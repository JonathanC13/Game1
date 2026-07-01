using NUnit;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using static UnityEngine.Rendering.HableCurve;

public class LinkPairManager : MonoBehaviour
{
    InputSystem_Actions input;

    public CameraStateMachine cameraStateMachine;

    public PuzzleManager puzzleManager;

    public Camera playerCamera;

    public Transform puzzleArea;

    public LinkLine linePrefab;

    public LinkLine previewLinePrefab;

    public Transform lineParent;

    public DestroyLinkButton destroyButtonPrefab;

    private DestroyLinkButton activeDestroyButton;

    LinkRouter linkRouter = new();

    private readonly List<LinkPair> connectedPairs = new();

    public IReadOnlyList<LinkPair> ConnectedPairs => connectedPairs;

    private HashSet<LinkableItem> usedItems = new();

    private LinkableItem firstSelection = null;

    private LinkPair selectedPair = null;

    private PendingLink pendingLink = null;

    private readonly float previewHeight = 10.0f;

    private readonly float linkDistance = 0.2f;


    // EvidenceView -> Links affected by this view
    private readonly Dictionary<EvidenceView, HashSet<LinkPair>> viewToPairs = new();

    // dirty queues
    private readonly HashSet<EvidenceView> dirtyViews = new();
    private readonly HashSet<LinkPair> dirtyPairs = new();

    void Awake()
    {
        input = new InputSystem_Actions();
    }

    // Unity lifecycle method that runs once per frame after all Update() calls have finished.
    private void LateUpdate()
    {
        PropagateDirtyViews();

        UpdateDirtyPairs();


        // for pending link
        if (pendingLink != null && pendingLink.linkLine != null)
        {
            Vector3 mouseWorld = GetMouseWorldPosition();
            //Vector3 endPos = new Vector3(mouseWorld.x, pendingLink.startItem.transform.position.y, mouseWorld.z); // do not use, causes offset.
            //Debug.Log(endPos);
            //Debug.Log("mouse" + mouseWorld);
            //Vector3 start = GetPreviewPoint(pendingLink.startItem.transform.position);
            //Vector3 end = GetPreviewPoint(mouseWorld);
            pendingLink.linkLine.SetPosition(pendingLink.startItem.transform.position, mouseWorld);
        }
        
    }

    private void OnEnable()
    {
        input.Enable();

        input.Player.CancelLink.started += cancelPendingLink;
    }

    private void OnDisable()
    {
        input.Player.CancelLink.started -= cancelPendingLink;

        input.Disable();
    }

    public void Register(LinkableItem item)
    {
        item.OnSelected += ItemSelected;  // subscribe to Linkable.OnSelected to method to handle.
    }

    void ItemSelected(LinkableItem item)
    {
        selectedPair = null;

        if (activeDestroyButton != null)
            activeDestroyButton.Hide();

        if (usedItems.Contains(item))
        {
            //Debug.Log("One of the facts exists in an active pair.");

            // find the LinkPair the selected item is associated with.
            foreach (LinkPair pair in connectedPairs)
            {
                if (pair.linkItemA == item || pair.linkItemB == item)
                {
                    selectedPair = pair;

                    // manage destroy button
                    if (activeDestroyButton == null)
                    {
                        activeDestroyButton = Instantiate(destroyButtonPrefab);
                        activeDestroyButton.Setup(this);
                    }
                    
                    activeDestroyButton.transform.SetParent(item.transform, false);
                    activeDestroyButton.SetPosition(item.GetDestroyButtonPos());
                    activeDestroyButton.Show();

                    break;
                }
            }
            return;
        } else if (firstSelection == null) 
        {
            firstSelection = item;
            //Debug.Log("first selection: " + item.linkableId);

            startPendingLink(item);

            return;
        }
        else if (firstSelection == item)
        {
            // self
            return;
        }

        CreatePair(firstSelection, item);

        firstSelection = null;
    }

    private void startPendingLink(LinkableItem item)
    {
        Vector3 mouseWorld = GetMouseWorldPosition();
        Vector3 endPos = new Vector3(mouseWorld.x, item.transform.position.y, mouseWorld.z);   // mouse x,y to x,z plane of puzzle surface.

        pendingLink = new PendingLink(item);

        pendingLink.linkLine = Instantiate(previewLinePrefab, lineParent);

        pendingLink.linkLine.line.sortingLayerName = "UI";
        pendingLink.linkLine.line.sortingOrder = 100;
    }

    void RemovePendingLink()
    {       
        /* listener for cancel pending pair with player interaction.
        Call when:
            right click
            escape
        */
        if (pendingLink != null)
        {
            firstSelection = null;

            if (pendingLink.linkLine != null)
            {
                pendingLink.linkLine.RemoveLine();
            }

            pendingLink = null;
        }    
        
    }

    void CreatePair(LinkableItem a, LinkableItem b)
    {
        if (usedItems.Contains(a) || usedItems.Contains(b))
        {
            //Debug.Log("One of the facts exists in an active pair.");
            return;
        }

        usedItems.Add(a);
        usedItems.Add(b);

        a.ShowLinkedBox();
        b.ShowLinkedBox();

        
        List <(Vector3 start, Vector3 end)> segments =
            linkRouter.CalculateRoute(
                a,
                b
            );
        
        LinkVisual visual = new LinkVisual();
        foreach (var segment in segments)
        {
            LinkLine line = Instantiate(linePrefab, lineParent);
            line.Setup(segment.start, segment.end);

            visual.segments.Add(line);

        }

        LinkPair pair = new LinkPair(a, b, visual); 
        pair.Setup(a, b, visual);

        connectedPairs.Add(pair);

        RegisterViewPair(pair.viewA, pair);

        RegisterViewPair(pair.viewB, pair);

        SubscribeToViews(pair);

        //Debug.Log("connected: " + a.linkableId + " <-> " + b.linkableId);

        RemovePendingLink();
    }

    private void RegisterViewPair(
       EvidenceView view,
       LinkPair pair)
    {
        if (view == null)
            return;

        if (!viewToPairs.TryGetValue(view, out var set))
        {
            set = new HashSet<LinkPair>();
            viewToPairs.Add(view, set);
        }

        set.Add(pair);
    }

    private void SubscribeToViews(LinkPair pair)
    {
        if (pair.viewA != null)
            pair.viewA.OnMoved += HandleViewMoved;


        if (pair.viewB != null)
            pair.viewB.OnMoved += HandleViewMoved;
    }

    private void unSubscribeToViews(LinkPair pair)
    {
        if (pair.viewA != null)
            pair.viewA.OnMoved -= HandleViewMoved;


        if (pair.viewB != null)
            pair.viewB.OnMoved -= HandleViewMoved;
    }

    // Dragging pair update link
    private void HandleViewMoved(EvidenceView view)
    {
        dirtyViews.Add(view);
    }

    // for first mouseDown for elevation change, must update all pair links.
    public void HandleViewInteracted()
    {
        foreach (LinkPair pair in connectedPairs)
        {
            dirtyPairs.Add(pair);
        }
    }

    private void PropagateDirtyViews()
    {
        foreach (EvidenceView view in dirtyViews)
        {
            if (viewToPairs.TryGetValue(
                view,
                out var affectedPairs))
            {
                foreach (LinkPair pair in affectedPairs)
                {
                    dirtyPairs.Add(pair);
                }
            }
        }

        dirtyViews.Clear();
    }

    private void UpdateDirtyPairs()
    {
        //if (dirtyPairs.Count > 0)
        //{
        //    Debug.Log("dirtyPairs: " + dirtyPairs.Count);
        //}
        foreach (LinkPair pair in dirtyPairs)
        {
            UpdatePair(pair);
        }

        dirtyPairs.Clear();
    }

    private void UpdatePair(LinkPair pair)
    {
        List<(Vector3 start, Vector3 end)> segments =
            linkRouter.CalculateRoute(
                pair.linkItemA,
                pair.linkItemB
            );
        
        List<LinkLine> linkLines = new();
        foreach (var segment in segments)
        {
            LinkLine line = Instantiate(linePrefab, lineParent);
            line.Setup(segment.start, segment.end);

            linkLines.Add(line);

        }

        pair.linkVisual.SetNewSegments(linkLines);
    }

    public void RemoveSelectedPair()
    {
        if (selectedPair != null)
        {
            RemovePair(selectedPair);
            selectedPair = null;
        }

        if (activeDestroyButton != null)
        {
            activeDestroyButton.Hide();
        }
    }

    public void RemovePair(LinkPair pair)
    {
        connectedPairs.Remove(pair);

        usedItems.Remove(pair.linkItemA);
        usedItems.Remove(pair.linkItemB);

        pair.RemoveLinkVisual();

        if (viewToPairs.TryGetValue(pair.viewA, out var pairsA))
        {
            pairsA.Remove(pair);
        }
        if (viewToPairs.TryGetValue(pair.viewB, out var pairsB))
        {
            pairsB.Remove(pair);
        }

        unSubscribeToViews(pair);
    }

    private Vector3 GetMouseWorldPosition()
    {
        // mouse position on screen
        Vector2 mouseScreen =
            Mouse.current.position.ReadValue();

        // create ray going to mouse position
        Ray ray =
            playerCamera.ScreenPointToRay(mouseScreen);

        // debug 
        //Debug.DrawRay(
        //    playerCamera.transform.position,
        //    ray.direction * 100,
        //    Color.red
        //);

        // create plane to hit on x,z
        // (normal (perpendicular), point on the surface)
        //Plane plane =
        //    new Plane(Vector3.up, puzzleArea.position);

        //// where on the plane the ray hit.
        //if (plane.Raycast(ray, out float distance))
        //{
        //    // convert to world position
        //    return ray.GetPoint(distance);
        //}

        // colliding with puzzle pieces
        //if (Physics.Raycast(ray, out RaycastHit hit))
        //{
        //    return hit.point;
        //}

        // player camera plane
        Plane plane = GetLinkPlane();

        if (plane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance);
        }


        return Vector3.zero;
    }
    private Vector3 GetPreviewPoint(Vector3 point)
    {
        return new Vector3(
            point.x,
            previewHeight,
            point.z
        );
    }

    private Plane GetLinkPlane()
    {
        // 0.1 < link distance
        // playerCamera.transform.forward * linkDistance < 0.4 (from puzzle camera anchor to puzzle surface
        //Debug.Log(linkDistance);
        return new Plane(
            playerCamera.transform.forward,
            playerCamera.transform.position + playerCamera.transform.forward * linkDistance
        );
    }

    private void cancelPendingLink(InputAction.CallbackContext ctx)
    {
        if (cameraStateMachine == null || cameraStateMachine.state == CameraState.FPS)
        {
            // CameraState.FPS because esc key during Inspecting transitions player out of CameraState.Inspecting, therefore OK to cancel link if in transition state.
            return;
        }

        RemovePendingLink();
    }
}

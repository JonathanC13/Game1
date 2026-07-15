using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DialogueGraphEditorWindow : EditorWindow
{
    private DialogueGraphView graphView;

    private DialogueGraph graph;

    [MenuItem("Tools/Dialogue Graph")]
    public static void Open()
    {
        GetWindow<DialogueGraphEditorWindow>("Dialogue Graph");
    }

    private void OnEnable()
    {
        ConstructGraphView();

        Selection.selectionChanged += OnSelectionChanged;
    }

    private void OnDisable()
    {
        Selection.selectionChanged -= OnSelectionChanged;

        rootVisualElement.Remove(graphView);
    }

    private void ConstructGraphView()
    {
        graphView = new DialogueGraphView(this);

        //graphView.StretchToParentSize();

        rootVisualElement.Add(graphView);
    }

    private void OnSelectionChanged()
    {
        if (Selection.activeObject is DialogueGraph selected)
        {
            graph = selected;

            graphView.Populate(graph);
        }
    }
}

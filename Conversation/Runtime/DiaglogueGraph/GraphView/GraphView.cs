using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

// place in Folder Editor so Unity knows how to run it.
public class GraphView : GraphView
{
    public MyCustomGraphView()
    {
        // Add basic grid background
        Insert(0, new GridBackground());

        // Add standard manipulators for navigation
        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        // Match standard visual elements styling
        this.StretchToParentSize();
    }
}

// get all edges to draw.
//foreach (var edge in graph.Edges)
//{
//    DrawConnection(
//        edge.From,
//        edge.To);
//}

Graph editor window
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MyGraphEditorWindow : EditorWindow
{
    [MenuItem("Window/Custom Node Graph")]
    public static void OpenWindow()
    {
        MyGraphEditorWindow window = GetWindow<MyGraphEditorWindow>();
        window.titleContent = new GUIContent("Node Graph");
    }

    private void CreateInitialize()
    {
        // Instantiate your custom GraphView
        MyCustomGraphView graphView = new MyCustomGraphView();
        
        // Add it to the window's root container
        rootVisualElement.Add(graphView);
    }

    private void OnEnable()
    {
        CreateInitialize();
    }
}

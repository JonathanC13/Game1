using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

// To dock the GraphView
public class DialogueGraphEditorWindow : EditorWindow
{
    private DialogueGraphView graphView;

    private DialogueGraph graph;

    private List<ValidationResult> validationResults;


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

        graphView.GraphStructureChanged += ValidateGraph;

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


    private void ValidateGraph()
    {
        //run after;
        //    edge created
        //    edge removed
        //    node created
        //    node removed
        DialogueGraphValidator validator =
            new();

        validationResults =
            validator.Validate(graph);
    }
}

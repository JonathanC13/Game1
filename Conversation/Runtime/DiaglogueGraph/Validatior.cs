using UnityEngine;

public class Validatior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

// children
//foreach (var edge in graph.GetOutgoingEdges(node))
//{
//    Debug.Log(edge.To.EditorName);
//}

//parents
//foreach(var edge in graph.GetIncomingEdges(node))
//{
//    Debug.Log(edge.From.EditorName);
//}

//speeh node more than one next
//    int count =
//    graph.GetOutgoingEdges(node)
//         .Count(e =>
//            e.EdgeType ==
//            DialogueEdgeType.Next);

//result node has any next
//    bool hasChildren =
//    graph.GetOutgoingEdges(node)
//         .Any();

//Any node is orphaned?
//    bool hasParents =
//    graph.GetIncomingEdges(node)
//         .Any();

//get all choice destinations
//    var destinations =
//    graph.GetOutgoingEdges(node)
//         .Where(e =>
//            e.EdgeType ==
//            DialogueEdgeType.Choice)
//         .Select(e =>
//            graph.GetDestination(e));

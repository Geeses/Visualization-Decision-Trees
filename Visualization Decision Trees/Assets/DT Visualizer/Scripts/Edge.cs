// To Debug the children of the tree in the Unity Inspector, remove the comment so the Edge class is serializable
//using System;

//[Serializable]
public class Edge
{
    public Node targetNode;
    public string split;

    public Edge()
    {
        targetNode = new Node();
        split = "Empty";
    }

    public Edge(Node _targetNode, string _split)
    {
        targetNode = _targetNode;
        split = _split;
    }
}

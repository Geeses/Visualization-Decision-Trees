using System;

[Serializable]
public class Tree
{
    public Node headNode;

    public Tree()
    {
        headNode = new Node();
    }

    public Tree(Node _headNode)
    {
        headNode = _headNode;
    }
}

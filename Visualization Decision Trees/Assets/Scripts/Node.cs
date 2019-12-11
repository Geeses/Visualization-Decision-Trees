using System;

[Serializable]
public class Node
{
    public string splitRule;
    public string attribute;
    public Edge[] children;

    public Node()
    {
        splitRule = "NumericalSplit";
        attribute = "Empty";
        children = new Edge[2];
    }

    public Node(string _splitrule, string _attribute, Edge[] _children)
    {
        splitRule = _splitrule;
        attribute = _attribute;
        _children = children;
    }

}

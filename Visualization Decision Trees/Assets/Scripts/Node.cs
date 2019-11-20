using System;

[Serializable]
public class Node
{
    public ISplitrule splitRule;
    public string attribute;
    public Edge[] children;
}

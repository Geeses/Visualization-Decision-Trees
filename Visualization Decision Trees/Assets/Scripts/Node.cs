using System;

[Serializable]
public class Node
{
    public Node[] children;
    public ISplitrule splitRule;
    public string branch;
    public string content;
}

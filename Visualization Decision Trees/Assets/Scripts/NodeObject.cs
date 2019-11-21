using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using ReadWriteCsv;

public class NodeObject : MonoBehaviour
{
    public Node node;
    public NodeObject parent;
    public List<NodeObject> childrenObjects;
    public List<EdgeObject> edges;

    // the number of of all children of the node, used to calculate space between the nodes
    public int childCount;

    public TextMeshPro samplesText;

    public TextMeshPro attributeText;

    public List<CsvRow> samples;

    void Awake()
    {
        samples = new List<CsvRow>();
    }

    void Update()
    {
        samplesText.text = "Samples: " + samples.Count.ToString();
        attributeText.text = "Attribute: " + node.attribute;
    }
}

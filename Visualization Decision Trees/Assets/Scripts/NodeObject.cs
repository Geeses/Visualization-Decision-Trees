using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using ReadWriteCsv;
using System.Data;

public class NodeObject : MonoBehaviour
{
    public Node node;
    public NodeObject parent;
    public ISplitrule splitRule;

    public List<NodeObject> childrenObjects;
    public List<EdgeObject> edges;

    // the number of of all children of the node, used to calculate space between the nodes
    public int childCount;

    public TextMeshPro samplesText;

    public TextMeshPro attributeText;

    public DataTable samples;

    void Awake()
    {
        samples = new DataTable();
    }

    void Update()
    {
        samplesText.text = "Samples: " + samples.Rows.Count;
        attributeText.text = "Attribute: " + node.attribute;
    }

    public void DetectSplitrule()
    {
        ISplitrule split = new UndefinedSplit();

        if (node.splitRule.ToLower().Equals("numericalsplit"))
        {
            split = new NumericalSplit();
        }
        //else if (_splitRule.ToLower().Equals("categoricalsplit"))
        //{
        //    split = new CategoricalSplit();
        //}

        splitRule = split;
        
    }
}

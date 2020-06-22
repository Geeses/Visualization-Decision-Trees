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
    public int layer;

    public TextMeshPro samplesText;

    public TextMeshPro attributeText;

    public DataTable samples;

    public MeshRenderer renderer;

    public bool traversed;

    void Awake()
    {
        samples = new DataTable();
    }

    void Start()
    {
        renderer = GetComponentInChildren<MeshRenderer>();
    }

    void Update()
    {
        samplesText.text = "Samples: " + samples.Rows.Count;
        attributeText.text = "Attribute: " + node.attribute;

        // if there are no children, then it is a leaf, and we just want the attribute displayed there
        if(childrenObjects.Count == 0)
        {
            attributeText.text = node.attribute;
        }
    }

    public void DetectSplitrule()
    {
        ISplitrule split = new UndefinedSplit();

        if (node.splitRule.ToLower().Equals("numericalsplit"))
        {
            split = new NumericalSplit();
        }

        // Uncomment if CategoricalSplit is implemented

        //else if (_splitRule.ToLower().Equals("categoricalsplit"))
        //{
        //    split = new CategoricalSplit();
        //}

        splitRule = split;        
    }

    public void NodeObjectInteractableExampleHoverBegin()
    {
        renderer.material.color = Color.red;
    }
    public void NodeObjectInteractableExampleHoverEnd()
    {
        renderer.material.color = Color.white;
    }
}

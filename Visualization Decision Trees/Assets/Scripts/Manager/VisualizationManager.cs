using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizationManager : MonoBehaviour
{
    public Transform startPoint;

    public GameObject nodeObject;

    public float edgeLength = 1f;
    public float nodeDistance = 2f;

    [SerializeField]
    private Node tree;
    
    void Start()
    {
        tree = JsonManager.instance.tree;

        VisualizeTree(tree);
    }

    public void VisualizeTree(Node _tree)
    {
        if (tree != null)
        {
            CreateNode(tree, startPoint.position, edgeLength, nodeDistance);
        }
        else
        {
            Debug.LogError("No tree found, or empty tree");
        }
    }

    void CreateNode(Node node, Vector3 position, float _edgeLength, float _nodeDistance)
    {
        GameObject tmp = Instantiate(nodeObject, position + new Vector3(_nodeDistance, -_edgeLength, 0), Quaternion.identity);
        NodeObject tmpNode = tmp.GetComponent<NodeObject>();
        tmpNode.node = node;

        for (int i = 0; i < node.children.Length; i++)
        {
            if (node.children[i] != null)
            {
                CreateNode(node.children[i], tmp.transform.position, _edgeLength, _nodeDistance);
            }
        }
    }
}

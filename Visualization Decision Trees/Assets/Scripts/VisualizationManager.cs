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
    
    IEnumerator Start()
    {
        // wait until tree is assignet to cache the value
        yield return new WaitUntil(() => tree != null);

        tree = JsonManager.instance.tree;

        VisualizeTree();
    }

    public void VisualizeTree()
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

        foreach(Node child in node.children)
        {
            if (child != null)
            {
                CreateNode(child, tmp.transform.position, _edgeLength, _nodeDistance);
            }
        }
    }
}

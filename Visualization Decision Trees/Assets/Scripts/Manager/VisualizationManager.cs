using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReadWriteCsv;
using System.Text.RegularExpressions;
using System.Data;

public class VisualizationManager : MonoBehaviour
{
    public Transform startPoint;

    public GameObject nodeObject;

    public EdgeObject edgeObject;

    public float edgeLength = 1f;
    public float nodeDistance = 2f;

    public bool flowData;

    [SerializeField]
    private Tree tree;

    public List<NodeObject> nodes;

    public List<NodeObject> leafs;

    public int highestNodeLayer;
    
    void Start()
    {
        nodes = new List<NodeObject>();
        leafs = new List<NodeObject>();
        tree = JsonManager.instance.tree;
                
        VisualizeTree(tree);

        StartCoroutine(Flow());
    }

    IEnumerator Flow()
    {
        yield return new WaitUntil(() => flowData);

        DataTable data = new DataTable();
        data = JsonManager.instance.data.Copy();

        Evaluate(data, nodes[0]);
    }

    void Evaluate(DataTable data, NodeObject root)
    {
        // we want to give the node all the data, which will not be touched, so we can display what went through
        root.samples = data.Copy();

        for (int i = 0; i < root.childrenObjects.Count; i++)
        {
            DataTable newData = data.Copy();

            for (int j = newData.Rows.Count - 1; j >= 0; j--)
            {
                if (!root.splitRule.Execute(newData.Rows[j], root.node.attribute, root.node.children[i].split))
                {
                    newData.Rows[j].Delete();
                    newData.AcceptChanges();
                }
                root.childrenObjects[i].samples = newData;
            }

            Evaluate(newData, root.childrenObjects[i]);
        }
    }

    public void VisualizeTree(Tree _tree)
    {
        if (_tree != null)
        {
            CreateNodes(_tree.headNode, null, 0);
            //PlaceNodes(nodes[0], startPoint.position);
            PlaceNodes(nodes);
            PlaceEdges(nodes);
        }
        else
        {
            Debug.LogError("No tree found");
        }
    }

    void CreateNodes(Node node, NodeObject parent, int layer)
    {
        GameObject go = Instantiate(nodeObject);
        NodeObject nodeObj = go.GetComponent<NodeObject>();
        nodes.Add(nodeObj);
        nodeObj.node = node;
        // In the Json, the splitrule is only a string, we want to know which actual class the rule belongs to
        nodeObj.DetectSplitrule();

        nodeObj.parent = parent;

        nodeObj.layer = layer;

        if (layer > highestNodeLayer)
            highestNodeLayer = layer;

        //if the parent is not null, set the previous node as the parent of this node
        if(parent != null)
        {
            //go.transform.SetParent(parent.transform);
            parent.childrenObjects.Add(nodeObj);
        }

        // if children are null, then it is a leaf
        if(node.children.Length == 0)
        {
            Debug.Log("Null children", nodeObj);
            return;
        } 

        for (int i = 0; i < node.children.Length; i++)
        {
            if (node.children[i] != null)
            {
                // set up the edge
                EdgeObject edge = Instantiate(edgeObject);
                edge.transform.SetParent(go.transform);

                nodeObj.edges.Add(edge);
                edge.splitText.text = node.children[i].split;
                
                CreateNodes(node.children[i].targetNode, nodeObj, nodeObj.layer + 1);
            }
        }
    }

    void PlaceNodes(List<NodeObject> _nodes)
    {
        // get every leaf node, from left to right into a list
        NodeObject bottomLeftLeaf = SearchForBottomLeftLeaf(_nodes[0]);
        FindLeafNodes(bottomLeftLeaf);


        // position all leafs
        for (int i = 0; i < leafs.Count; i++)
        {
            Vector3 newPosition = startPoint.position + new Vector3((i * nodeDistance) - ((leafs.Count * nodeDistance) / 2), -(edgeLength * leafs[i].layer), 0);
            leafs[i].transform.position = newPosition;
        }

        // position every node in relation to its child (the leafs are correctly positioned so every node can be now aswell) from highest layer to headNode
        int currentLayer = highestNodeLayer;
        //j represents the layer we are positioning right now
        for (int j = currentLayer; j >= 0; j--)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                // ignore current node if its a leaf
                if (nodes[i].childrenObjects.Count != 0)
                {
                    if (nodes[i].layer == j)
                    {
                        Debug.Log("Positioning " + nodes[i] + "!", nodes[i]);
                        // position it in between its children
                        // sorry for long equation, but its just the middle of the first and the last child as x
                        Vector3 newPosition = new Vector3(
                                             (nodes[i].childrenObjects[0].transform.position.x + nodes[i].childrenObjects[nodes[i].childrenObjects.Count - 1].transform.position.x) / 2,
                                              nodes[i].childrenObjects[0].transform.position.y + edgeLength,
                                              0);
                        nodes[i].transform.position = newPosition;
                    }
                }
                else
                {
                    Debug.Log("Ignore", nodes[i]);
                }
            }
        }
    }


    // go deeper into the tree until our tmp variable bottomLeftLeaf has no children anymore
    // this assumes that the first child of the childrenObjects list is always the leftest child
    NodeObject SearchForBottomLeftLeaf(NodeObject node)
    {
        NodeObject bottomLeftLeaf = node;

        while (bottomLeftLeaf.childrenObjects.Count != 0)
        {
            bottomLeftLeaf = bottomLeftLeaf.childrenObjects[0];
        }

        return bottomLeftLeaf;
    }

    void FindLeafNodes(NodeObject node)
    {
        for (int i = 0; i < node.childrenObjects.Count; i++)
        {
            if(node.childrenObjects[i].childrenObjects.Count == 0)
            {
                leafs.Add(node.childrenObjects[i]);
            } else if(!node.childrenObjects[i].traversed)
            {
                FindLeafNodes(node.childrenObjects[i]); 
            }
        }

        if (node.parent)
        {
            node.traversed = true;
            FindLeafNodes(node.parent);
        }
    }

    void PlaceEdges(List<NodeObject> _nodes)
    {
        for (int i = 0; i < _nodes.Count; i++)
        {
            for (int j = 0; j < _nodes[i].childrenObjects.Count; j++)
            {
                _nodes[i].edges[j].edgeRenderer.SetPosition(0, _nodes[i].transform.position);
                _nodes[i].edges[j].edgeRenderer.SetPosition(1, _nodes[i].childrenObjects[j].transform.position);
                _nodes[i].edges[j].splitText.gameObject.transform.position = 
                    new Vector3((_nodes[i].transform.position.x + _nodes[i].childrenObjects[j].transform.position.x) / 2,
                                (_nodes[i].transform.position.y + _nodes[i].childrenObjects[j].transform.position.y) / 2,
                                 0);
            }
        }
    }
}

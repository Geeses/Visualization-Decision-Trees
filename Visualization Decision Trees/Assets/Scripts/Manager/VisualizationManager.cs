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
    private Node tree;

    private List<NodeObject> nodes;
    
    void Start()
    {
        nodes = new List<NodeObject>();
        tree = JsonManager.instance.tree;
                
        VisualizeTree(tree);

        StartCoroutine(Flow());
    }

    IEnumerator Flow()
    {
        yield return new WaitUntil(() => flowData);

        DataTable data = new DataTable();
        data = JsonManager.instance.dataRows;

        //StartCoroutine(Evaluate(data, nodes[0]));

    }   

    IEnumerator Evaluate(List<CsvRow> data, NodeObject root)
    {
        root.samples = new List<CsvRow>(data);
        for (int i = 0; i < root.childrenObjects.Count; i++)
        {
            List<CsvRow> newData = new List<CsvRow>(data);

            for (int j = 0; j < newData.Count; j++)
            {
                //if (!root.node.splitRule.Execute(newData[j], root.node.attribute, root.node.children[i].split))
                //{
                //    newData.RemoveAt(j);
                //    j -= 1;
                //}
                root.childrenObjects[i].samples = newData;
                yield return new WaitForEndOfFrame();
            }

            StartCoroutine(Evaluate(newData, root.childrenObjects[i]));
        }
    }

    public void VisualizeTree(Node _tree)
    {
        if (_tree != null)
        {
            CreateNodes(_tree, null);
            PlaceNodes(nodes[0], startPoint.position);
        }
        else
        {
            Debug.LogError("No tree found");
        }
    }

    void CreateNodes(Node node, NodeObject parent)
    {
        GameObject go = Instantiate(nodeObject);
        NodeObject nodeObj = go.GetComponent<NodeObject>();
        nodes.Add(nodeObj);
        nodeObj.node = node;

        nodeObj.parent = parent;

        //if the parent is not null, set the previous node as the parent of this node
        if(parent != null)
        {
            go.transform.SetParent(parent.transform);
            parent.childrenObjects.Add(nodeObj);
        }

        // if children are null, then it is a leaf
        if(node.children == null)
        {
            return;
        } 
        else
        {
            // this is for getting the correct childCount for each Node
            // childCount is the sum of all children of the node, so if the child has children it gets count together
            // first we assign the count of the own child Array to its own counter
            // then, if we have a parent, then take the same number and add it to its parent,
            // do this as long as there are parents

            nodeObj.childCount += node.children.Length;
            NodeObject tmpObj = nodeObj;

            while(tmpObj.parent != null)
            {
                tmpObj.parent.childCount += node.children.Length;
                tmpObj = tmpObj.parent;
            }
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
                
                CreateNodes(node.children[i].targetNode, nodeObj);
            }
        }
    }

    // Function to place the nodes into their positions
    void PlaceNodes(NodeObject tmpNode, Vector3 position)
    {
        // place the node to its position
        tmpNode.transform.localPosition = position;
        
        // do the same for each child
        for (int i = 0; i < tmpNode.childrenObjects.Count; i++)
        {
            // This is the formula to place the children of this current node into positions, where there is enough space for their children aswell and its neighbours
            // Explanation: First of all, nodeDistance is the whole distance between first child, to last child, and the position we give it is in relation to its parent.
            //              To give everyone an evenly distributed space on the x axis, we take i + 1 (number of current child (and + 1 because the for loop starts at 0)),
            //              and divide if by the count of all children times the nodeDistance.
            //              " (i+1 / tmpNode.childrenObjects.Count) * nodeDistance)"
            //              With 3 children, this would look like: 1: 1/3 * nodeDistance, 2: 2/3 * nodeDistance, 3: 3/3 nodeDistance.
            //              While this is evenly distributed on a nodeDistance distance, it doesnt look like a tree, but like a cliff, because we have no negative value.
            //              Which means the new nodes will only appear on the right side of the parent, and no one on the left.
            //              So we shift everything 1/2 * nodedistance to the negative with : "- nodeDistance/2".
            //              If we want to have equal space for every node, we will eventually have a problem if we have alot of children, thats why we have a childCount saved.
            //              Childcount represents the number of ALL children the node has, so even the children of the child.
            //              Thats why we multiply it with the childCount, to give a node additional space if it has alot of children. (it needs the +1 because of leaf nodes that have no children)
            Vector3 newPosition = new Vector3((((i+1 / tmpNode.childrenObjects.Count) * nodeDistance) - nodeDistance/2) * (tmpNode.childrenObjects[i].childCount + 1), -edgeLength, 0);

            // Place the edges
            // Place its text into the middle of the edge
            tmpNode.edges[i].splitText.gameObject.transform.localPosition = newPosition / 2;
            tmpNode.edges[i].edgeRenderer.SetPosition(1, newPosition);

            PlaceNodes(tmpNode.childrenObjects[i], newPosition);
        }
    }

}

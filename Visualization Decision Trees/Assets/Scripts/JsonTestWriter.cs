using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonTestWriter : MonoBehaviour
{
    public bool WriteFile;

    void Start()
    {
        // stop start function if writefile is false
        if (!WriteFile)
            return;

        Node node = new Node();
        Node node1 = new Node();
        Node node2 = new Node();
        node.content = "Größe > 40";
        node.children = new Node[2] { node1, node2 };
        node1.content = "Alter > 40";
        node2.content = "Gewicht < 100kg";
        string json = JsonUtility.ToJson(node);
        System.IO.File.WriteAllText(@"E:\Unity Workspace\Visualization Decision Trees\Visualization Decision Trees\Assets\text.json", json);
    }
}


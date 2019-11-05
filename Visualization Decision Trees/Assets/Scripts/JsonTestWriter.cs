using System.Collections;
using System.Collections.Generic;
using Valve.Newtonsoft.Json;
using UnityEngine;

public class JsonTestWriter : MonoBehaviour
{
    public bool WriteFile;

    void Start()
    {
        // stop start function if writefile is false
        if (!WriteFile)
            return;

        WriteTestNodes();
        WriteTestData(1000);
    }

    void WriteTestData(int count)
    {
        Dictionary<string, float>[] data = new Dictionary<string, float>[count];

        for (int i = 0; i < count; i++)
        {
            data[i] = new Dictionary<string, float>();
            data[i].Add("age", Random.Range(5, 80));
            data[i].Add("size", Random.Range(1.10f, 2.10f));
            data[i].Add("IQ", Random.Range(50, 140));
            data[i].Add("weight", Random.Range(50, 200));
        }

        //string json = JsonUtility.ToJson(data[0], true);
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        Debug.Log(json);
        System.IO.File.WriteAllText(@"E:\Unity Workspace\Visualization Decision Trees\Visualization Decision Trees\Assets\data.json", json);
    }

    void WriteTestNodes()
    {
        Node node = new Node();
        Node node1 = new Node();
        Node node2 = new Node();
        node.content = "Größe > 40";
        node.children = new Node[2] { node1, node2 };
        node1.content = "Alter > 40";
        node2.content = "Gewicht < 100kg";
        string json = JsonUtility.ToJson(node, true);
        System.IO.File.WriteAllText(@"E:\Unity Workspace\Visualization Decision Trees\Visualization Decision Trees\Assets\text.json", json);
    }
}


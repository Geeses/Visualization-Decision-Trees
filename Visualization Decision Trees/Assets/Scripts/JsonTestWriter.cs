using System.Collections;
using System.Collections.Generic;
using Valve.Newtonsoft.Json;
using UnityEngine;
using CsvHelper;
using System.IO;
using System.Data;
using System.Dynamic;

public class JsonTestWriter : MonoBehaviour
{
    public bool WriteModel;
    public bool WriteData;

    void Awake()
    {
        // stop start function if writefile is false
        if (WriteModel)
        {
            Tree tree = new Tree();
            WriteTestNodes(5, tree.headNode, 2, 5);
            //WriteTestNodes(tree);

            string json = JsonConvert.SerializeObject(tree, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented
            });

            File.WriteAllText(@"E:\Unity Workspace\Visualization Decision Trees\Visualization Decision Trees\Assets\text.json", json);
        }
        if (WriteData)
        {
            WriteTestData(1000);
        }

    }

    void WriteTestData(int count)
    {
        var records = new List<dynamic>();

        dynamic record;

        for (int i = 0; i < count; i++)
        {
            record = new ExpandoObject();
            record.age = (Random.Range(18, 70).ToString());
            record.size = (Random.Range(1.20f, 2.10f).ToString());
            record.IQ = (Random.Range(50, 120).ToString());
            record.weight = (Random.Range(50, 120).ToString());
            records.Add(record);
        }

        using (var writer = new StreamWriter(JsonManager.instance.dataPath))
        using (var csv = new CsvWriter(writer))
        {
            csv.WriteRecords(records);            
        }
    }

    void WriteTestNodes(int layerCount, Node node, int minChildCount, int maxChildCount)
    {
        layerCount--;

        int currentChildCount = Random.Range(minChildCount, maxChildCount);
        node.children = new Edge[currentChildCount];

        if (layerCount != 0)
        {
            for(int i = 0; i < currentChildCount; i++)
            {
                node.children[i] = new Edge();
                node.children[i].targetNode = new Node();
                WriteTestNodes(layerCount, node.children[i].targetNode, minChildCount, maxChildCount);
            }
        }
    }

    void WriteTestNodes(Tree tree)
    {
        Node node = new Node();
        tree.headNode = node;

        Node node1 = new Node();
        Node node2 = new Node();
        Node node3 = new Node();
        Node node4 = new Node();
        Node node5 = new Node();
        Node node6 = new Node();
        Node node7 = new Node();
        Node node8 = new Node();
        Node node9 = new Node();
        Node node10 = new Node();

        node.attribute = "size";

        node.children = new Edge[] { new Edge(), new Edge()};
        node.children[0].targetNode = node1;
        node.children[0].split = ">1,60";
        node.children[1].targetNode = node2;
        node.children[1].split = "<=1,60";

        node1.attribute = "age";
        node2.attribute = "age";

        node1.children = new Edge[] { new Edge(), new Edge() };
        node1.children[0].targetNode = node3;
        node1.children[0].split = "<50";
        node1.children[1].targetNode = node4;
        node1.children[1].split = ">=50";

        node2.children = new Edge[] { new Edge(), new Edge() };
        node2.children[0].targetNode = node5;
        node2.children[0].split = ">30";
        node2.children[1].targetNode = node6;
        node2.children[1].split = "<=30";

        node3.attribute = "weight";
        node4.attribute = "IQ";
        node5.attribute = "Healthy";
        node6.attribute = "Sick";

        node3.children = new Edge[] { new Edge(), new Edge() };
        node3.children[0].targetNode = node7;
        node3.children[0].split = "<70";
        node3.children[1].targetNode = node8;
        node3.children[1].split = ">=70";

        node4.children = new Edge[] { new Edge(), new Edge() };
        node4.children[0].targetNode = node9;
        node4.children[0].split = "==80";
        node4.children[1].targetNode = node10;
        node4.children[1].split = "!=80";

        node7.attribute = "Healthy";
        node8.attribute = "Sick";
        node9.attribute = "Sick";
        node10.attribute = "Healthy";
    }
}


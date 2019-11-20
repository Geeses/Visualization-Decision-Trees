using System.Collections;
using System.Collections.Generic;
using Valve.Newtonsoft.Json;
using UnityEngine;
using ReadWriteCsv;

public class JsonTestWriter : MonoBehaviour
{
    public bool WriteFile;

    void Awake()
    {
        // stop start function if writefile is false
        if (!WriteFile)
            return;

        WriteTestNodes();
        WriteTestData(1000);
    }

    void WriteTestData(int count)
    {
        
        using (CsvFileWriter writer = new CsvFileWriter(JsonManager.instance.dataPath))
        {
            for (int i = 0; i < count; i++)
            {                
                CsvRow row = new CsvRow();
                row.Add("age");
                row.Add(Random.Range(18, 70).ToString());
                row.Add("size");
                row.Add(Random.Range(1.20f, 2.10f).ToString());
                row.Add("IQ");
                row.Add(Random.Range(50, 120).ToString());
                row.Add("weight");
                row.Add(Random.Range(50, 120).ToString());
                writer.WriteRow(row);                
            }
        }
    }

    void WriteTestNodes()
    {
        Node node = new Node();
        node.splitRule = new NumericalSplit();
        Node node1 = new Node();
        node1.splitRule = new NumericalSplit();
        Node node2 = new Node();
        node2.splitRule = new NumericalSplit();
        Node node3 = new Node();
        node3.splitRule = new NumericalSplit();
        Node node4 = new Node();
        node4.splitRule = new NumericalSplit();
        Node node5 = new Node();
        node5.splitRule = new NumericalSplit();
        Node node6 = new Node();
        node6.splitRule = new NumericalSplit();
        Node node7 = new Node();
        node7.splitRule = new NumericalSplit();
        Node node8 = new Node();
        node8.splitRule = new NumericalSplit();
        Node node9 = new Node();
        node9.splitRule = new NumericalSplit();
        Node node10 = new Node();
        node10.splitRule = new NumericalSplit();

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

        string json = JsonConvert.SerializeObject(node, new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Formatting.Indented
        });

        System.IO.File.WriteAllText(@"E:\Unity Workspace\Visualization Decision Trees\Visualization Decision Trees\Assets\text.json", json);
    }
}


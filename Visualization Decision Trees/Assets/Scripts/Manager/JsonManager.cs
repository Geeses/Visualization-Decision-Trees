using System.Collections;
using System.Collections.Generic;
using Valve.Newtonsoft.Json;
using UnityEngine;
using System.IO;
using ReadWriteCsv;
using System;

public class JsonManager : MonoBehaviour
{
    public string treePath = @"E:\Unity Workspace\Visualization Decision Trees\Visualization Decision Trees\Assets\text.json";
    public string dataPath = @"E:\Unity Workspace\Visualization Decision Trees\Visualization Decision Trees\Assets\data.csv";

    public static JsonManager instance;

    // this is the whole tree
    public Node tree;
    public List<CsvRow> dataRows;

    void Awake()
    {
        dataRows = new List<CsvRow>();

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        string tmp = File.ReadAllText(treePath);
        tree = JsonConvert.DeserializeObject<Node>(tmp, new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.Auto
        });
        //tmp = File.ReadAllText(dataPath);
        //data = tmp;
        using (CsvFileReader reader = new CsvFileReader(dataPath))
        {
            CsvRow row = new CsvRow();
            while (reader.ReadRow(row))
            {
                dataRows.Add(row);
                row = new CsvRow();
            }
        }
    }

    void Start()
    {
        /*foreach (var row in dataRows)
        {
            Debug.Log(String.Join(", ", row.ToArray()));
        } */
    }

}

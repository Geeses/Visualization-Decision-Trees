using System.Collections;
using System.Collections.Generic;
using Valve.Newtonsoft.Json;
using UnityEngine;
using System.IO;
using ReadWriteCsv;
using System;
using System.Data;
using CsvHelper;

public class JsonManager : MonoBehaviour
{
    public string treePath = @"E:\Unity Workspace\Visualization Decision Trees\Visualization Decision Trees\Assets\text.json";
    public string dataPath = @"E:\Unity Workspace\Visualization Decision Trees\Visualization Decision Trees\Assets\data.csv";

    public static JsonManager instance;

    // this is the whole tree
    public Tree tree;
    public DataTable data;

    void Awake()
    {
        data = new DataTable();

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        using (var reader = new StreamReader(dataPath))
        using (var csv = new CsvReader(reader))
        {
            using (var dr = new CsvDataReader(csv))
            {
                data.Load(dr);
            }
        }
        //TODO: Read it in manually to not rely on the deserializer of the node
        tree = JsonConvert.DeserializeObject<Tree>(File.ReadAllText(treePath), new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.Auto
        });
    }

}

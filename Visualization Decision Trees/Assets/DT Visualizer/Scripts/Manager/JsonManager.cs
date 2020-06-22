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
    public string treeName = @"model.json";
    public string dataName = @"data.csv";

    public static JsonManager instance;

    // this is the whole tree
    public Tree tree;
    public DataTable data;

    void Awake()
    {
        treeName = Application.dataPath + @"\" + treeName;
        dataName = Application.dataPath + @"\" + dataName;
        
        data = new DataTable();

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }        
    }

    void Start()
    {
        ReadData();
        ReadTree();
    }

    void ReadTree()
    {
        //TODO: Read it in manually to not rely on the deserializer of the node
        tree = JsonConvert.DeserializeObject<Tree>(File.ReadAllText(treeName), new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.Auto
        });
    }

    void ReadData()
    {
        using (var reader = new StreamReader(dataName))
        using (var csv = new CsvReader(reader))
        {
            using (var dr = new CsvDataReader(csv))
            {
                data.Load(dr);
            }
        }
    }

}

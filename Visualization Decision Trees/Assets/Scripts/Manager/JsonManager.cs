using System.Collections;
using System.Collections.Generic;
using Valve.Newtonsoft.Json;
using UnityEngine;
using System.IO;


public class JsonManager : MonoBehaviour
{
    public string treePath = @"E:\Unity Workspace\Visualization Decision Trees\Visualization Decision Trees\Assets\text.json";
    public string dataPath = @"E:\Unity Workspace\Visualization Decision Trees\Visualization Decision Trees\Assets\data.json";

    public static JsonManager instance;

    // this is the whole tree
    public Node tree;
    public Dictionary<string, float>[] data;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        string tmp = File.ReadAllText(treePath);
        tree = JsonUtility.FromJson<Node>(tmp);

        tmp = File.ReadAllText(dataPath);
        data = JsonConvert.DeserializeObject<Dictionary<string, float>[]>(tmp);

    }

    void Start()
    {

    }

}

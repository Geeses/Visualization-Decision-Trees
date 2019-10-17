using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class JsonManager : MonoBehaviour
{
    public string path;

    public static JsonManager instance;

    // this is the whole tree
    public Node tree;

    void Awake()
    {
        if (instance != null)
            instance = this;
        else if (instance != this) ;
            //Destroy(gameObject);
    }

    void Start()
    {
        string tmp = File.ReadAllText(path);
        tree = JsonUtility.FromJson<Node>(tmp);
    }

}

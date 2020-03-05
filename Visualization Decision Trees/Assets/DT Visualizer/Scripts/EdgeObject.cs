using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EdgeObject : MonoBehaviour
{
    public LineRenderer edgeRenderer;
    public TextMeshPro splitText;
    void Start()
    {
        edgeRenderer = GetComponentInChildren<LineRenderer>();
        splitText = GetComponentInChildren<TextMeshPro>();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stageCanvas : MonoBehaviour
{

    public GameObject canvasText;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setText(string text)
    {
        canvasText.GetComponent<Text>().text = text;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupScore : MonoBehaviour
{

    public GameObject score;
    public GameObject stage;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setData(string scoreData, string stageData)
    {
        score.GetComponent<Text>().text = scoreData;
        stage.GetComponent<Text>().text = stageData;
    }
}

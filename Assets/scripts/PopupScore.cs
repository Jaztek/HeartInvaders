using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupScore : MonoBehaviour
{

    public GameObject score;
    public GameObject stage;
    public progresBar progres;

    public void setData(long scoreData, int stageData)
    {
        score.GetComponent<Text>().text = scoreData.ToString("0000000");
        stage.GetComponent<Text>().text = stageData.ToString();
        progres.setProgress(stageData, scoreData);
    }
}

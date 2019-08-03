using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupScore : MonoBehaviour
{

    public GameObject score;
    public GameObject stage;
    public progresBar progres;

    public GameObject UInextStage;

    public GameObject txCheckpoint;
    public GameObject txNewRecordUI;

    int currentStage;
    int nextStage = 0;

    public void setData(long scoreData, int stageData, int nextStageUI, bool newRecord)
    {
        txCheckpoint.gameObject.SetActive(false);
        txNewRecordUI.gameObject.SetActive(false); 

        score.GetComponent<Text>().text = scoreData.ToString("0000000");
        stage.GetComponent<Text>().text = stageData.ToString();
        UInextStage.GetComponent<Text>().text = stageData.ToString();
        progres.setProgress(stageData, scoreData);

        currentStage = stageData;
        nextStage = nextStageUI;
        StartCoroutine("nextStageCoroutine");

        if(newRecord){ txNewRecordUI.gameObject.SetActive(true); };
    }

    IEnumerator nextStageCoroutine()
    {
        for (;currentStage != nextStage;)
        {
            currentStage--;
            UInextStage.GetComponent<Text>().text = currentStage.ToString();
            yield return new WaitForSeconds(0.1f);
        }
        txCheckpoint.gameObject.SetActive(true);
    }
}

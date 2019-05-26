using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class progresBar : MonoBehaviour
{

    public Image cooldown;
    public Text[] stageNumbers;
    private float targetProgress = 0;
    
    void Start()
    {
        cooldown.fillAmount = 0.0f;
        //setProgress(4, 110);
    }

    public void setProgress(int stage, long score)
    {
        targetProgress = 0.0f;
        cooldown.fillAmount = 0.0f;
        int intervalIni = UtilsStage.getStageRange(stage);

        printStageNumbers(intervalIni);
        targetProgress = (stage - intervalIni) / 5f;
        print(targetProgress);
        targetProgress += UtilsStage.getStageProgres(stage, score);
        StartCoroutine("progress");
    }

    private void printStageNumbers(int intervalIni)
    {
        foreach (Text stage in stageNumbers)
        {
            stage.text = intervalIni.ToString();
            intervalIni++;
        }
    }

    IEnumerator progress()
    {
        float amount = 0.01f;
        for (; cooldown.fillAmount < targetProgress;)
        {
            cooldown.fillAmount += amount;
            if(targetProgress - cooldown.fillAmount < 0.2f){
                    amount = 0.003f;
            }
            else if(targetProgress - cooldown.fillAmount < 0.4f){
                    amount = 0.007f;
            }
            yield return new WaitForSeconds( 0.001f);
        }
    }

}

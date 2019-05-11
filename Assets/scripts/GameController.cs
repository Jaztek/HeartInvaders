﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameController : MonoBehaviour
{
    public GameObject score;
    public GameObject heart;
    public GameObject log;
    public GameObject popup;

    public float nextFire = 2;
    public float fireRate = 2;

    private ScoreController scoreController;
    private bool gameOver = true;

    private StageModel currentStage;
    private StagesList stages;


    void Start()
    {
        scoreController = score.GetComponentInChildren<ScoreController>();
        getStagesPlattform();
        //getStages();
    }


    void Update()
    {
        if (gameOver || currentStage == null) { return; }

        if (Time.time > nextFire)
        {
            nextFire = Time.time + currentStage.fireRate;

            Vector3 position = randomPosition();
            var heartPosition = heart.transform.position + new Vector3(0, 1, 0);
            Vector3 targetDir = heartPosition - position;
            var angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            GameObject variableForPrefab = (GameObject)Resources.Load("Prefabs/" + getBullet(), typeof(GameObject));

            GameObject shotClone = Instantiate(variableForPrefab, position, rotation);
        }
    }
    private string getBullet()
    {
        string path = "";
        var rand = Random.Range(1, 100);
        foreach (BulletModel bull in currentStage.bullets)
        {
            rand = rand - bull.probabilidad;
            if (rand <= 0)
            {
                path = bull.path;
                break;
            }
        }
        return path;
    }
    
    private Vector3 randomPosition()
    {
        var x = randomPositionX();
        var y = randomPositionY(x);
        //new Vector3(x, y)
        return new Vector3(x, y);
    }
    private int randomPositionY(int x) {
        if ((x >= -6 && x <= 6))
        {
            var y = Random.Range(0, 2);
            if (y == 0)
            {
                y = y - 4;
            }
            else if (y == 1)
            {
                y = y + 3;
            }
            return y;
        }
        else
        {
            return Random.Range(-4, 5);
        }
    }
    private int randomPositionX()
    {
        var x = Random.Range(-5, 6);
        if (x < 0)
        {
            x = x - 2;
        }
        if (x >= 0)
        {
            x = x + 2;
        }
        return x;
    }
    ///////////////////////////// ---- Functions ----///////////////////////////////////////////



    public void scored()
    {
        if (gameOver) { return; }
        scoreController.scored();

        checkScore();
    }

    private void checkScore()
    {
        if (scoreController.getScore() >= currentStage.maxScore && stages.stagesList.Length >= currentStage.stage + 1)
        {
            currentStage = stages.stagesList[currentStage.stage + 1];
        }
    }

    public void damage()
    {
        heart.GetComponent<Heart>().damage();
        if (heart.GetComponent<Heart>().checklife() == 3)
        {
            setGameOver();
        }
    }

    public void setGameOver()
    {
        gameOver = true;
        popup.SetActive(true);
        popup.GetComponent<PopupScore>().setData(scoreController.getScore().ToString("00000000"), currentStage.stage.ToString());
    }
    public void restart()
    {
        loadStage();
        heart.GetComponent<Heart>().restart();
        scoreController.restart();
        gameOver = false;
    }

    public void getStagesPlattform()
    {
        loadStage();
    }

    public void loadStage()
    {
        stages = StageService.getStages();
        currentStage = stages.stagesList[0];

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameController : MonoBehaviour
{
    [Header("Controllers")]
    public PlayerController player;
    public MainController main;
    public MusicController music;

    [Header("Game Objects")]
    public GameObject heart;
    public GameObject popup;
    public GameObject kaboom;
    public Barrera barreras;

    [Header("Canvas")]
    public GameObject tutoCanvas;
    public GameObject stageCanvas;
    public GameObject score;
    private ScoreController scoreController;
    public GameObject log;
    public boomsController bombs;

    [Header("Variables")]
    public float nextFire = 2;
    public float fireRate = 2;

    private bool gameOver = true;
    private StageModel currentStage;
    private StagesList stages;


    void Start()
    {
        scoreController = score.GetComponentInChildren<ScoreController>();
        getStagesPlattform();
    }


    void Update()
    {
        if (gameOver || currentStage == null) { return; }

        if (Time.time > nextFire)
        {
            nextFire = Time.time + currentStage.fireRate;

            Vector3 position = RandomPosition.randomPosition();
            var heartPosition = heart.transform.position + new Vector3(0, 0, 0);
            Vector3 targetDir = heartPosition - position;
            var angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            GameObject variableForPrefab = (GameObject)Resources.Load("Prefabs/Bullets/" + getBullet(), typeof(GameObject));

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
            changeStage(currentStage);

            // al pasar de stage, eliminamos el tutorial.
            tutoCanvas.SetActive(false);
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
        destroyAllBullets();
        music.stopMusic();
        popup.SetActive(true);
        tutoCanvas.SetActive(false);
        popup.GetComponent<PopupScore>().setData(scoreController.getScore(), currentStage.stage);

        if (player.getGameModel() != null && scoreController.getScore() > player.getGameModel().playerModel.maxScore) {
            music.playSong("victoria");
        } else if (player.getGameModel() != null) {
            music.playSong("gameOver");
        }
    }
    public void restart()
    {
        loadStage();
        heart.GetComponent<Heart>().restart();
        scoreController.restartScore();
        bombs.setBombs(player.getBombs());

        StartCoroutine("delayStartGame");
        if (currentStage.stage == 0)
        {
            tutoCanvas.SetActive(true);
        }
    }

    IEnumerator delayStartGame()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(4f);
            gameOver = false;
            changeStage(currentStage);
            break;
        }

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

    public void backToMenu()
    {
        heart.GetComponent<Heart>().restart();
        main.backToMenu(scoreController.getScore(), currentStage.stage);
    }

    public void changeStage(StageModel stage)
    {
        GameObject stageUI = Instantiate(stageCanvas, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        stageUI.transform.SetParent(GameObject.FindGameObjectWithTag("stageCanvas").transform, false);
        stageUI.GetComponent<stageCanvas>().setText(stage.stage.ToString());

        if (stage.music != null)
        {
            music.playSong(stage.music);
        }
    }

    public void boom()
    {
        if (player.getBombs() > 0)
        {
            GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
            camera.GetComponent<CameraController>().triggerShake();
            GameObject kaboomInst = Instantiate(kaboom, new Vector3(0, 0, 0), Quaternion.identity);
            kaboomInst.transform.SetParent(heart.transform.parent);

            player.setBooms(player.getBombs() - 1);
            bombs.setBombs(player.getBombs());
        }
    }

    public void activeBarreras(bool active)
    {
        barreras.activeBarreras(active);
    }

    public void destroyAllBullets()
    {
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("bullet");
        foreach (var bull in bullets)
        {
            Destroy(bull);
        }
    }
}


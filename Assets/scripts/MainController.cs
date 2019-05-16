using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{

    public GameController game;

    public GameObject gameCanvas;
    public GameObject mainCanvas;

    public GameObject lifeHeartContainer;
    public GameObject popupScore;
    public GameObject textLifes;
    public GameObject textScore;

    public CameraController cameraController;

    private GameModel gameModel;

    // private GameController game;
    //private Home
    // Use this for initialization
    void Start()
    {
        gameModel = LoadSaveService.Load();
        generateHeartsLifes();
        updateCanvas();

    }

    private void generateHeartsLifes()
    {
        foreach (Transform child in lifeHeartContainer.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        float currentAngle = 0;

        for (int i = 0; i < gameModel.lifes; i++)
        {

            GameObject variableForPrefab = (GameObject)Resources.Load("Prefabs/LifeHeart", typeof(GameObject));

            GameObject lifeHeart = Instantiate(variableForPrefab, Vector2.zero, this.transform.rotation);

            lifeHeart.GetComponent<RotatingHeart>().currentAngle = currentAngle;
            currentAngle = currentAngle + 0.3f;

            lifeHeart.transform.parent = lifeHeartContainer.transform;
        }
    }

    public void startGame()
    {
        mainCanvas.SetActive(false);
        gameCanvas.SetActive(true);
        lifeHeartContainer.SetActive(false);

        game.restart();
    }

    public void restart()
    {
        game.setGameOver();
        mainCanvas.SetActive(true);
        gameCanvas.SetActive(false);
        popupScore.SetActive(false);
        lifeHeartContainer.SetActive(true);
    }

    public void updateCanvas()
    {
        textLifes.GetComponent<Text>().text = gameModel.lifes.ToString();
        textScore.GetComponent<Text>().text = gameModel.maxScore.ToString("0000000");
    }

    public void backToMenu(long score, int stage)
    {
        gameModel = LoadSaveService.saveCurrentGame(1, score, stage);
        generateHeartsLifes();
        updateCanvas();
        restart();
    }

    public void setLifesTo(int lifes)
    {
        gameModel = LoadSaveService.addLifes(10);
        generateHeartsLifes();
        updateCanvas();
    }
}
   

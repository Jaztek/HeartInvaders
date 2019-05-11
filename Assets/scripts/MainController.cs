using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{

    public GameController game;

    public GameObject gameCanvas;
    public GameObject mainCanvas;
    public GameObject lifeHeartContainer;
    public GameObject popupScore;

    public CameraController cameraController;

    // private GameController game;
    //private Home
    // Use this for initialization
    void Start()
    {
        // game = gameController.GetComponent<GameController>();
        float currentAngle = 0;
        for (int i = 0; i < 9; i++)
        {

            GameObject variableForPrefab = (GameObject)Resources.Load("Prefabs/LifeHeart", typeof(GameObject));

            GameObject lifeHeart = Instantiate(variableForPrefab, Vector2.zero, this.transform.rotation);

            lifeHeart.GetComponent<RotatingHeart>().currentAngle = currentAngle;
            currentAngle = currentAngle + 0.2f;

            lifeHeart.transform.parent = lifeHeartContainer.transform;
        }
    }

    public void startGame()
    {
        mainCanvas.SetActive(false);
        gameCanvas.SetActive(true);
        lifeHeartContainer.SetActive(false);
        cameraController.goToGame();
        game.restart();
    }

    public void restart()
    {
        game.setGameOver();
        mainCanvas.SetActive(true);
        gameCanvas.SetActive(false);
        popupScore.SetActive(false);
        lifeHeartContainer.SetActive(true);
        cameraController.goToMenu();
    }
}

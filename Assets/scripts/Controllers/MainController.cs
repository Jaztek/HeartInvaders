using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{

    [Header("Canvas")]
    public GameObject gameCanvas;
    public GameObject mainCanvas;
    public GameObject popupScore;
    public GameObject textLifes;
    public GameObject textScore;

    [Header("Models")]
    private GameModel gameModel;

    [Header("Controllers")]
    public GameController game;
    public PlayerController player;
    public CameraController cameraController;
    public MusicController music;

    [Header("Game Objects")]
    public GameObject lifeHeartContainer;

    // private GameController game;
    //private Home
    // Use this for initialization
    void Start()
    {
        gameModel = LoadSaveService.Load();
        generateHeartsLifes();
        updateCanvas();
        restartMenu();
        music.playClipMain();

         FirebaseController firebase = new FirebaseController();

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
        music.stopMusic();
        player.setGameModel(gameModel);
        mainCanvas.SetActive(false);
        gameCanvas.SetActive(true);
        lifeHeartContainer.SetActive(false);
        game.activeBarreras(true);

        game.restart();

       
    }

    public void restartMenu()
    {
        music.playClipMain();
        game.setGameOver();
        mainCanvas.SetActive(true);
        gameCanvas.SetActive(false);
        popupScore.SetActive(false);
        game.activeBarreras(false);
        lifeHeartContainer.SetActive(true);
    }

    public void updateCanvas()
    {
        textLifes.GetComponent<Text>().text = gameModel.lifes.ToString();
        textScore.GetComponent<Text>().text = gameModel.maxScore.ToString("0000000");
    }

    public void backToMenu(long score, int stage)
    {
        gameModel = LoadSaveService.saveCurrentGame(1, score, stage, player.getBombs());
        generateHeartsLifes();
        updateCanvas();
        restartMenu();
         music.playClipMain();
    }

    public void setLifesTo(int lifes)
    {
        gameModel = LoadSaveService.addLifes(lifes);
        generateHeartsLifes();
        updateCanvas();
    }

    public void addBoms(int boms)
    {
        gameModel = LoadSaveService.addBoms(boms);
        // generateHeartsLifes();
        // updateCanvas();
    }
}


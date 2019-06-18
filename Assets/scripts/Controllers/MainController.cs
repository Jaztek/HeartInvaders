using GoogleMobileAds.Api;
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
    public GameObject createNick;

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
        LoadSaveService.Load();

        //si el jugador no tiene nombre, es porque es nuevo y debe crearselo.
        if (LoadSaveService.game.playerModel.name == null)
        {
            createNick.SetActive(true);
        }
        generateHeartsLifes();
        updateCanvas();
        restartMenu();
        music.playSong("casaAsteroide");

        FirebaseController.start();
        AdmobController.start();
    }

    private void generateHeartsLifes()
    {
        foreach (Transform child in lifeHeartContainer.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        float currentAngle = 0;

        for (int i = 0; i < LoadSaveService.game.lifes; i++)
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
        player.setGameModel(LoadSaveService.game);
        mainCanvas.SetActive(false);
        gameCanvas.SetActive(true);
        lifeHeartContainer.SetActive(false);
        game.activeBarreras(true);

        game.restart();
    }

    public void restartMenu()
    {
        game.setGameOver();
        music.playSong("casaAsteroide");
        mainCanvas.SetActive(true);
        gameCanvas.SetActive(false);
        popupScore.SetActive(false);
        game.activeBarreras(false);
        lifeHeartContainer.SetActive(true);
    }

    public void updateCanvas()
    {
        textLifes.GetComponent<Text>().text = LoadSaveService.game.lifes.ToString();
        textScore.GetComponent<Text>().text = LoadSaveService.game.playerModel.maxScore.ToString("0000000");
    }

    public void backToMenu(long score, int stage)
    {
        LoadSaveService.saveCurrentGame(1, score, stage, player.getBombs());
        generateHeartsLifes();
        updateCanvas();
        restartMenu();
        music.playSong("casaAsteroide");
    }

    public void setLifesTo(int lifes)
    {
        if (AdmobController.rewardedLifeAd.IsLoaded())
        {
            AdmobController.rewardedLifeAd.Show();
        }
        else
        {
            LoadSaveService.addLifes(lifes);
        }
        generateHeartsLifes();
        updateCanvas();
    }

    public void addBoms(int boms)
    {
        if (AdmobController.rewardedBombAd.IsLoaded())
        {
            AdmobController.rewardedBombAd.Show();
        }
        else
        {
            LoadSaveService.addBoms(boms);
        }
    }

    void OnEnable()
    {
        mainCanvas.GetComponent<CanvasGroup>().alpha = 0f;
        StartCoroutine("fadeInMenu");
    }

    IEnumerator fadeInMenu()
    {
        yield return new WaitForSeconds(3f);
        for (float alpha = 0f; alpha < 1f; alpha = alpha + 0.05f)
        {
            mainCanvas.GetComponent<CanvasGroup>().alpha = alpha;
            yield return new WaitForSeconds(0.1f);
        }

    }
}


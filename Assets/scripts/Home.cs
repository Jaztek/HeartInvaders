using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;


public class Home : MonoBehaviour
{
    public GameObject score;
    public GameObject bullet;
    public GameObject heart;

    public float nextFire = 2;
    public float fireRate = 2;

    private ScoreController scoreController;

    void Start() { scoreController = score.GetComponentInChildren<ScoreController>(); }

    public void startGame()
    {
        scoreController.start();
    }

    void Update()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            Vector3 position = randomPosition();

            print(heart.transform.position);
            var heartPosition = heart.transform.position + new Vector3(0, 1, 0);
            Vector3 targetDir = heartPosition - position;
            var angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            GameObject shotClone = Instantiate(bullet, position, rotation);
        }
    }
    ///////////////////////////// ---- Functions ----///////////////////////////////////////////

    private Vector3 randomPosition()
    {
        var x = Random.Range(-10, 10);
        var y = randomPositionY(x);

        return new Vector3(x, y);
    }

    private int randomPositionY(int x)
    {
        var y = Random.Range(-10, 10);
        if ((x >= -6 && x <= 6) && (y > -6 && y < 6))
        {
            y = randomPositionY(x);
        }
        return y;
    }

    public void scored()
    {
        scoreController.scored();

        checkScore();
    }

    private void checkScore()
    {
         if (scoreController.getScore() > 70)
        {
            fireRate = 0.01f;
        }
        else if (scoreController.getScore() > 40)
        {
            fireRate = 0.3f;
        }
        else if (scoreController.getScore() > 20)
        {
            fireRate = 0.5f;
        }
        else if (scoreController.getScore() > 10)
        {
            fireRate = 1f;
        }
        else if (scoreController.getScore() > 5)
        {
            fireRate = 1.5f;
        }

        print(fireRate);
    }

        public void exitPresed(bool presed)
        {
            if (presed)
            {
            }
            else
            {
            }
        }
    }

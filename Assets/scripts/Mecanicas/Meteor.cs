using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    private GameController gameController;

    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("barrera"))
        {
            Destroy(this.gameObject);
            gameController.scored();
        }
        if (other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            gameController.damage();
        }
    }

     void OnDestroy()
    {
        GameObject explosionPrefab = (GameObject)Resources.Load("Prefabs/Expl/Explosion", typeof(GameObject));
        explosionPrefab.GetComponent<Explosion>().setExplParams(0.5f, EnumExplColor.Orange);
        GameObject explosion = Instantiate(explosionPrefab, this.transform.position, this.transform.rotation);
    }
}

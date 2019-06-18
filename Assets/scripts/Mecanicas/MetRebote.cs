using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetRebote : MonoBehaviour, BulletInterface
{
    public float timeWithoutGrav = 0.5f;
    public int lifes = 3;
    public Color32[] shotColor;

    private GameController gameController;
    private Rigidbody2D rb;
    private Gravedad gravity;
    private SpriteRenderer sr;


    // Use this for initialization
    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        gravity = GetComponent<Gravedad>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        onTriggerEnter(other);
    }

    private void rebote()
    {
        sr.color = shotColor[Mathf.Abs(lifes - 3)];

        gravity.isEnabled = false;
        // rb.velocity = transform.TransformDirection(Vector3.right * 2);
        rb.AddForce(transform.TransformDirection(Vector3.left * 280));
        StartCoroutine("gravityEnabled");
    }

    IEnumerator gravityEnabled()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(timeWithoutGrav);
            gravity.isEnabled = true;
            break;
        }
    }

    public virtual void onTriggerEnter(Collider2D other)
    {
        if (other.CompareTag("barrera"))
        {
            lifes--;
            gameController.scored();
            if (lifes != 0)
            {
                rebote();
            }
            else
            {
                destroy();
            }
        }
        if (other.CompareTag("Player"))
        {
            destroy();
            gameController.damage();
        }
    }

    public virtual void destroy()
    {
        GameObject explosionPrefab = (GameObject)Resources.Load("Prefabs/Expl/Explosion", typeof(GameObject));
        explosionPrefab.GetComponent<Explosion>().setExplParams(1, EnumExplColor.Orange);
        GameObject explosion = Instantiate(explosionPrefab, this.transform.position, this.transform.rotation);

        Destroy(this.gameObject);
    }
}

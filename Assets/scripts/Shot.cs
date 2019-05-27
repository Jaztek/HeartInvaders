using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{

    private Rigidbody2D rigidbodyShot;
    public int speed = 50;
    private GameController gameController;

    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;


    // Use this for initialization
    void Start()
    {
        rigidbodyShot = GetComponent<Rigidbody2D>();
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[0];
        StartCoroutine("spriteChange");
    }

    // Update is called once per frame
    void Update()
    {
        rigidbodyShot.velocity = transform.TransformDirection(Vector3.right * speed);
        rigidbodyShot.AddForce(Vector3.right * speed);
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


    public void start()
    {
        StartCoroutine("counter");
    }

    IEnumerator spriteChange()
    {
        for (; ; )
        {
            if (spriteRenderer.sprite == sprites[0])
            {
                spriteRenderer.sprite = sprites[1];

            }
            else
            {
                spriteRenderer.sprite = sprites[0];
            }
            yield return new WaitForSeconds(0.4f);
        }
    }

    void OnDestroy()
    {
         GameObject explosion = (GameObject)Resources.Load("Prefabs/Expl/Explosion", typeof(GameObject));
         Instantiate(explosion, this.transform.position, this.transform.rotation);
    }
}

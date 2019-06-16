using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{

    public int speed = 7;
    public Sprite[] sprites;
    public EnumExplColor explColor;

    private Rigidbody2D rigidbodyShot;
    private GameController gameController;
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
        if(transform.parent != null && transform.parent.childCount == 0){
             Destroy(transform.parent.gameObject);
        }
        GameObject explosionPrefab = (GameObject)Resources.Load("Prefabs/Expl/Explosion", typeof(GameObject));
        explosionPrefab.GetComponent<Explosion>().setExplParams(this.transform.localScale.x, explColor);
        GameObject explosion = Instantiate(explosionPrefab, this.transform.position, this.transform.rotation);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine("spriteChange");
    }


    IEnumerator spriteChange()
    {
        for (int count = 0; count != sprites.Length; count++)
        {
           spriteRenderer.sprite = sprites[count];
            yield return new WaitForSeconds(0.05f);
        }
        Destroy(this.gameObject);
    }
}

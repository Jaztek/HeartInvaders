using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{

    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    private int lifeCount = 0;
    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[lifeCount];
    }

    public void restart()
    {
        lifeCount = 0;
        spriteRenderer.sprite = sprites[lifeCount];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void damage()
    {
        lifeCount++;
        if (sprites.Length > lifeCount)
        {
            spriteRenderer.sprite = sprites[lifeCount];
        }
    }
    public int checklife()
    {
        return lifeCount;
    }
}

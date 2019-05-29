using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;


    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine("spriteChange");
    }

    public void setExplParams(float scale, EnumExplColor color)
    {
        this.transform.localScale = new Vector2(scale, scale);
        GetComponent<SpriteRenderer>().color = getColor(color);
         print( GetComponent<SpriteRenderer>().color);

    }

    public Color getColor(EnumExplColor color)
    {
        Color colorExpl = new Color(0, 0, 255);
        switch (color)
        {
            case EnumExplColor.Blue:
                colorExpl = new Color(0, 0, 255);
                return colorExpl;

            case EnumExplColor.Red:
                colorExpl = new Color(197, 0, 0);
                return colorExpl;

            case EnumExplColor.Orange:
                colorExpl = new Color(191, 136, 0);
                return colorExpl;

            case EnumExplColor.White:
                colorExpl = new Color(255, 255, 255);
                return colorExpl;

            default:
                colorExpl =  new Color(0, 0, 255);
                return colorExpl;
        }
    }


    IEnumerator spriteChange()
    {
        for (int count = 0; count != sprites.Length; count++)
        {
            spriteRenderer.sprite = sprites[count];
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(this.gameObject);
    }
}

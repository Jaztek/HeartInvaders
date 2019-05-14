using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScoreController : MonoBehaviour, IPointerClickHandler
{
   
    public int cant = 1;
    public int multi = 1;
    
    private long count = 0;

    public void start()
    {
        StartCoroutine("counter");
    }

    IEnumerator counter()
    {
        for (; ; )
        {
            float vel = 0.2f;
            yield return new WaitForSeconds(vel);
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
    }

    public void scored(){
            count = count + (cant * multi);
            this.GetComponent<Text>().text = (count).ToString("0000000");
    }

    public long getScore(){
        return count;
    }

    public void restartScore(){
        count = 0;
        scored();
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class TextHome : MonoBehaviour, IPointerClickHandler
{

    public Home home;

    //Detect if a click occurs
    public void OnPointerClick(PointerEventData pointerEventData)
    {
       Debug.Log("tralari");
    }
}

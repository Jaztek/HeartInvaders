using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActiveGO : MonoBehaviour, IPointerDownHandler
{
 public GameObject GOtoActive;

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        GOtoActive.SetActive(true);
    }
}

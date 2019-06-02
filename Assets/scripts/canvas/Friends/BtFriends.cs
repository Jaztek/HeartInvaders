using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtFriends : MonoBehaviour, IPointerDownHandler
{
    public GameObject panelFriends;

    public void OnPointerDown(PointerEventData pointerEventData)
    {
       panelFriends.SetActive(true);
    }
}

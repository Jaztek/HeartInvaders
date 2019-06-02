using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtGoToSerachFriend : MonoBehaviour, IPointerDownHandler
{
   public GameObject searchFrienPanel;

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        searchFrienPanel.SetActive(true);
    }
}

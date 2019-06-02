using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtSearchFriend : MonoBehaviour, IPointerDownHandler
{
   public SearchFriend popupSearchFriend;

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        popupSearchFriend.findFriend();
    }
}

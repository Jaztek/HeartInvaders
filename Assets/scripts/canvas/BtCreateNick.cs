using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BtCreateNick : MonoBehaviour, IPointerDownHandler
{
    public CreateNick popupCreateNick;

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        popupCreateNick.checkNick();
    }
}

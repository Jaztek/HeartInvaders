using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AddLifes : MonoBehaviour, IPointerDownHandler
{
    public MainController mainController;

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        mainController.setLifesTo(10);
    }

}


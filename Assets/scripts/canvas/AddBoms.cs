using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AddBoms : MonoBehaviour, IPointerDownHandler {

  public MainController mainController;

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        mainController.addBoms(3);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartGame : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public GameObject main;

    private MainController mainController;
    private Button butt;

    void Start()
    {
        //butt = this.gameObject.GetComponent<Button>();
        mainController = main.GetComponent<MainController>();
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        mainController.startGame();
    }

    //Detect if clicks are no longer registering
    public void OnPointerUp(PointerEventData pointerEventData)
    {
    }

}


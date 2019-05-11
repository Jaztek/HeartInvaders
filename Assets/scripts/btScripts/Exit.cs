using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Exit : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public MainController mainController;

    private Button butt;

    void Start()
    {
        butt = this.gameObject.GetComponent<Button>();
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        mainController.restart();
    }

    //Detect if clicks are no longer registering
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        //Debug.Log(name + "No longer being clicked");
    }
    
}

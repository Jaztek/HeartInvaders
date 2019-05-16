using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class btBoom : MonoBehaviour, IPointerDownHandler {

	public GameController gameController;


    void Start()
    {
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        gameController.boom();
    }

	public void destroySelf(){
		
	}

}

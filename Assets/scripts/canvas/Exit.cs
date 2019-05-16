using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Exit : MonoBehaviour, IPointerDownHandler
{
    public GameController gameController;

    private Button butt;

    void Start()
    {
        butt = this.gameObject.GetComponent<Button>();
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        gameController.backToMenu();
    }

}

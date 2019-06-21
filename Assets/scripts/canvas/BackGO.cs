using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackGO : MonoBehaviour, IPointerDownHandler
{
     public GameObject GOtoInactive;

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if(GOtoInactive == null){
            this.transform.parent.gameObject.SetActive(false);
        } else {
            GOtoInactive.gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Mute : MonoBehaviour, IPointerDownHandler
{
    public MusicController music;
    public Sprite[] imagenes;

    private bool isMute = false;

    public void init()
    {
        if (LoadSaveService.game.isMute) {
            muting();
        }
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        muting();
    }

    public void muting()
    {
        isMute = !isMute;
        music.muteChange(isMute);

        GetComponent<Button>().image.sprite = isMute ? imagenes[1] : imagenes[0];
    }

}

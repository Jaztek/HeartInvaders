using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioClip casaAsteroide;
    public AudioClip enemigos;
    public AudioClip victoria;
    public AudioClip gameOver;
    public AudioClip enemigosMisterioso;
    public AudioClip enemigosFuerte;
    public AudioClip enemigosBoss;

    private float fadeTime = 2.5f;
    private AudioSource audioSource;
    private bool fadingOut = false;
    private float startVolume;


    void Awake()
    {
        audioSource = this.GetComponent<AudioSource>();
        startVolume = audioSource.volume;
    }

    public void stopMusic()
    {
        fadingOut = true;
        StartCoroutine("FadeOut");

    }

    public void muteChange( bool mute){
        audioSource.mute = mute;
        
        LoadSaveService.game.isMute = mute;
        LoadSaveService.savePlayerLocal();
    }

    public void playSong(string song)
    {
        audioSource.loop = true;
        fadingOut = false;
        switch (song)
        {
            case "casaAsteroide":
                audioSource.clip = casaAsteroide;
                break;
            case "enemigos":
                audioSource.clip = enemigos;
                break;
            case "victoria":
                audioSource.clip = victoria;
                audioSource.loop = false;
                break;
            case "gameOver":
                audioSource.clip = gameOver;
                 audioSource.loop = false;
                break;
            case "enemigosMisterioso":
                audioSource.clip = enemigosMisterioso;
                break;
            case "enemigosFuerte":
                audioSource.clip = enemigosFuerte;
                break;
            case "enemigosBoss":
                audioSource.clip = enemigosBoss;
                break;

        }
        audioSource.Play();

       

    }

    public IEnumerator FadeOut()
    {
        while (audioSource.volume > 0)
        {
            if (fadingOut)
            {
                audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
                yield return null;
            }
            else
            {
                audioSource.volume = startVolume;
                break;
            }

        }
        audioSource.volume = startVolume;
        if (fadingOut)
        {
            audioSource.Stop();
            fadingOut = false;
        }
    }


}

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
    public float fadeTime = 2f;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    public void playClipMain()
    {
        audioSource.clip = casaAsteroide;
        audioSource.Play();
    }

    public void playClipEnemies()
    {
        audioSource.clip = enemigos;
        audioSource.Play();
    }

    public void stopMusic()
    {
        StartCoroutine("FadeOut");

    }

    public void mute()
    {
        audioSource.mute = true;
    }

    public void playSong(string song)
    {
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
                break;
            case "gameOver":
                audioSource.clip = gameOver;
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

    public IEnumerator FadeOut () {
        float startVolume = audioSource.volume;
 
        while (audioSource.volume > 0) {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
 
            yield return null;
        }
 
        audioSource.Stop ();
        audioSource.volume = startVolume;
    }


}

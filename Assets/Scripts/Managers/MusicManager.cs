using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(instance);
    }


    public AudioClip mainMenuMusic;
    public AudioClip inGameMusic;

    public string mainMenuScene = "StartMenu";
    public string gameScene = "TwoGuysGameplay";

    public AudioMixer globalMixer;
    public AudioSource globalAudioSource;

    string currentScene;

    [Header("Mixer parameters")]
    public string globalMixerVolume = "MainVolume";
    public void SetSceneMusic(string sceneName)
    {
        currentScene = sceneName;
        if (currentScene == mainMenuScene)
            FadeToMusic(mainMenuMusic);
        if (currentScene == gameScene)
            FadeToMusic(inGameMusic);
    }

    public void FadeToMusic(AudioClip newMusic)
    {
        globalAudioSource.DOFade(0, 0.3f).OnComplete(
            ()=>
            {
                globalAudioSource.clip = newMusic;
                globalAudioSource.Play();
                globalAudioSource.DOFade(1, 0.3f);

            }
            
            );
    }
}

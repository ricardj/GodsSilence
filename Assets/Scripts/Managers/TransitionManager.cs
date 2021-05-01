using DG.Tweening;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager instance = null;

    public GameObject fadeCanvas;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ChangeToScene(string sceneName)
    {
        fadeCanvas.SetActive(true);
        Image imageTransition = fadeCanvas.GetComponentInChildren<Image>();
        Color previousColor = imageTransition.color;
        Color fromColor = new Color(0, 0, 0, 0);
        imageTransition.DOColor(fromColor, 0.3f).From().OnComplete(
            ()=>
            {
                UnityAction<Scene, LoadSceneMode> sceneLoadedMethod = null;
                sceneLoadedMethod = (scene, mode) =>
                {
                    imageTransition.DOColor(fromColor, 0.3f).OnComplete(() => fadeCanvas.SetActive(false));
                    MusicManager.instance.SetSceneMusic(scene.name);
                    SceneManager.sceneLoaded -= sceneLoadedMethod;
                };
                SceneManager.sceneLoaded += sceneLoadedMethod;
                SceneManager.LoadScene(sceneName);
            }
            );

        
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

}

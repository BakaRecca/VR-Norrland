using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    private Color imageColorBlank;
    public string levelName;

    public static LoadLevel instance;

    [SerializeField, Range(1f, 3f)] private float transitionDelayTime;

    [SerializeField] private Image blankImage;

    private void Reset()
    {
        transitionDelayTime = 1f;
    }


    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != null)
        {
            Destroy(this);
        }

        imageColorBlank = blankImage.color;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartLoadingScene();
        }
    }

    private void StartLoadingScene()
    {
        StartCoroutine(LoadLevelAsync());
    }

    private IEnumerator FadeIn()
    {
        float alpha = 1f;
        float timer = 0f;

        blankImage.color = new Color(imageColorBlank.r, imageColorBlank.g, imageColorBlank.b, alpha);

        while (alpha > 0)
        {
            yield return null;

            timer += Time.deltaTime;
            alpha = Mathf.Lerp(1f, 0f, timer / transitionDelayTime);

            blankImage.color = new Color(imageColorBlank.r, imageColorBlank.g, imageColorBlank.b, alpha);
        }
    }


    private IEnumerator FadeOut()
    {
        float alpha = 0f;
        float timer = 0f;

        blankImage.color = new Color(imageColorBlank.r, imageColorBlank.g, imageColorBlank.b, alpha);

        while (alpha < 1)
        {
            yield return null;

            timer += Time.deltaTime;
            alpha = Mathf.Lerp(0f, 1f, timer / transitionDelayTime);

            blankImage.color = new Color(imageColorBlank.r, imageColorBlank.g, imageColorBlank.b, alpha);
        }
    }


    private IEnumerator LoadLevelAsync()
    {
        StartCoroutine(FadeOut());

        yield return new WaitForSeconds(transitionDelayTime);

        var oldScene = SceneManager.GetActiveScene();

        var progress = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive); // Starting the process to load

        //When while is done - level has loaded.
        while (!progress.isDone)
        {
            yield return null; // No object returns. Wait.
        }

        progress = SceneManager.UnloadSceneAsync(oldScene, UnloadSceneOptions.None);

        while (!progress.isDone)
        {
            yield return null; // No object returns. Wait.
        }

        StartCoroutine(FadeIn());
    }

}

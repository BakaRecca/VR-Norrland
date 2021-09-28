using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    public static LoadLevel Instance;
    
    [SerializeField] private Image blankImage;
    [SerializeField, Range(1f, 3f)] private float transitionDelayTime;
    
    private Color imageColorBlank;
    private string levelName;
    
    private void Reset()
    {
        transitionDelayTime = 1f;
    }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }
        
        transform.GetChild(0).gameObject.SetActive(true);

        imageColorBlank = blankImage.color;
        DontDestroyOnLoad(gameObject);
    }
    
    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    private void Update()
    {
        ReadDebugInput();
    }

    public void StartLoadingScene(string sceneName)
    {
        levelName = sceneName; 
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

        if (PlayerController.Instance != null)
            PlayerController.Instance.transform.parent = null;

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

    #region DEBUG

    private void ReadDebugInput()
    {
        if (Input.GetKey(KeyCode.S))
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                StartLoadingScene("Menu");
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                StartLoadingScene("CabinScene");
            else if (Input.GetKeyDown(KeyCode.Alpha3))
                StartLoadingScene("Lake");
            else if (Input.GetKeyDown(KeyCode.R))
                StartLoadingScene(SceneManager.GetActiveScene().name);
        }
    }

    #endregion
}

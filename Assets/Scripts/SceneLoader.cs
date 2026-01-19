using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    private int LoadingSceneIndex;
    public int CreditSceneIndex;

    public List<int> UnlockedScenes = new List<int>();
    public int currentSceneIndex { private set; get; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnLevelWasLoaded(int level)
    {
        
    }
    void Start()
    {
        CreditSceneIndex = SceneManager.sceneCountInBuildSettings - 1;
        LoadingSceneIndex = SceneManager.sceneCountInBuildSettings - 2;
        Debug.Log("Credit Index: " + CreditSceneIndex);
    }

    public void LoadNextScene(int sceneIndex)
    {
        if (sceneIndex < 0) return;
        UnlockedScenes.Add(sceneIndex);
        SceneManager.LoadScene(LoadingSceneIndex);
        currentSceneIndex = sceneIndex;
    }

    public void LoadCreditScene()
    {
        SceneManager.LoadScene(CreditSceneIndex);
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene(0);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void LoadCurrentScene()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }
}

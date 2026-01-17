using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    public int SceneIndexToLoad;
    private int LoadingSceneIndex;
    private int CreditSceneIndex;

    public List<int> UnlockedScenes = new List<int>();

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
    void Start()
    {
        CreditSceneIndex = SceneManager.sceneCount - 1;
        LoadingSceneIndex = SceneManager.sceneCountInBuildSettings - 2;
    }

    public void LoadNextScene(int sceneIndex)
    {
        if (sceneIndex < 0) return;
        UnlockedScenes.Add(sceneIndex);
        // Load Loading Scene
        SceneManager.LoadScene(LoadingSceneIndex);
        // Load Actual Scene
        SceneManager.LoadScene(sceneIndex);
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
}

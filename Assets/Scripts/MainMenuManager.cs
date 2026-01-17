using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject MainMenu_GO;
    public GameObject LevelSelectMenu_GO;

    private void Start()
    {
        Screen.SetResolution(1920, 1080, false);
    }

    public void OpenMainMenu()
    {
        MainMenu_GO.SetActive(true);
        LevelSelectMenu_GO.SetActive(false);
    }

    public void OpenLevelSelect()
    {
        MainMenu_GO.SetActive(false);
        LevelSelectMenu_GO.SetActive(true);
    }

    public void LoadCredits()
    {
        SceneLoader.Instance.LoadCreditScene();
    }

    public void LoadNextScene()
    {
        SceneLoader.Instance.LoadNextScene(1);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}

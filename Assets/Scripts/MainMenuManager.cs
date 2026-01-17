using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject MainMenu_GO;
    public GameObject LevelSelectMenu_GO;
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
}

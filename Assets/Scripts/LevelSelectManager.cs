using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelSelectManager : MonoBehaviour
{
    public TMP_Text LevelName_TXT;
    public Button StartGame_BTN;
    public List<Button> LevelSelection_BTNs = new List<Button>();

    private int selectedLevel = -1;

    public void Start()
    {
        for( int i = 0; i < LevelSelection_BTNs.Count; i++)
        {
            if (i < SceneLoader.Instance.UnlockedScenes.Count) LevelSelection_BTNs[i].interactable = true;
            else LevelSelection_BTNs[i].interactable = false;
        }
    }

    public void SelectLevel(int level)
    {
        selectedLevel = level;
        StartGame_BTN.interactable = true;
        LevelName_TXT.text = "Level: " + selectedLevel;
    }

    public void StartGame()
    {
        SceneLoader.Instance.LoadNextScene(selectedLevel);
    }

    public void OpenAllLevels()
    {
        foreach(var button in LevelSelection_BTNs)
        {
            button.interactable = true;
        }
    }
}

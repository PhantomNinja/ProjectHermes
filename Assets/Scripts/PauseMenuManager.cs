using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject PauseMenuCanvas_GO;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    public void UpdateManager()
    {
        
    }

    public void SwitchPauseMenu(bool gamePaused)
    {
        PauseMenuCanvas_GO.SetActive(gamePaused);
    }
}

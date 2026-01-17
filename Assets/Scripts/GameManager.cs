using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static bool GamePaused;
    public ObjectsManager ObjectsManager;
    public PlayerHUDManager PlayerHUDManager;
    public PauseMenuManager PauseMenuManager;
    
    // X = Seconds | Y = Minutes
    private Vector2 MainTimer = new Vector2();

    private InputAction pauseAction;
    private void Awake()
    {
        Instance = this;
        ObjectsManager.WakeUpManager();
        PlayerHUDManager.WakeUpManager();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseAction = InputSystem.actions.FindAction("Pause");
        ObjectsManager.StartManager();
        PlayerHUDManager.StartManager();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (pauseAction.WasCompletedThisFrame())
        {
            GamePaused = !GamePaused;
            PauseMenuManager.SwitchPauseMenu(GamePaused);
        }
        if (GamePaused)
        {
            PauseMenuManager.UpdateManager();
            return;
        }
        */
        MainTimer.x += Time.deltaTime;
        if(MainTimer.x >= 60)
        {
            MainTimer.y++;
            MainTimer.x = 0;
        }
        ObjectsManager.UpdateManager();
    }

    private void LateUpdate()
    {
        if(GamePaused) return;
        PlayerHUDManager.UpdateManager(MainTimer);
    }

    private void FixedUpdate()
    {
        if (GamePaused) return;
        ObjectsManager.FixedUpdateManager();
    }

    public void ResetGame()
    {
        ObjectsManager.ResetObjects();
    }
}

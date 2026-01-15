using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public ObjectsManager ObjectsManager;
    public PlayerHUDManager PlayerHUDManager;
    // X = Seconds | Y = Minutes
    private Vector2 MainTimer = new Vector2();

    private void Awake()
    {
        Instance = this;
        ObjectsManager.WakeUpManager();
        PlayerHUDManager.WakeUpManager();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ObjectsManager.StartManager();
        PlayerHUDManager.StartManager();
    }

    // Update is called once per frame
    void Update()
    {
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
        Debug.Log(MainTimer.ToString());
        PlayerHUDManager.UpdateManager(MainTimer);
    }

    private void FixedUpdate()
    {
        ObjectsManager.FixedUpdateManager();
    }

    public void ResetGame()
    {
        ObjectsManager.ResetObjects();
    }
}

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ObjectsManager ObjectsManager;

    private void Awake()
    {
        ObjectsManager.WakeUpManager();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ObjectsManager.StartManager();
    }

    // Update is called once per frame
    void Update()
    {
        ObjectsManager.UpdateManager();
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

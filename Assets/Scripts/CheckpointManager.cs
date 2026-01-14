using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public GameObject Player;
    public static CheckpointManager Instance;
    public Checkpoint currentCheckpoint;

    private void Awake()
    {
        Instance = this;
    }

    public void LoadCurrentCheckpoint()
    {
        Player.transform.position = currentCheckpoint.transform.position;
    }
}

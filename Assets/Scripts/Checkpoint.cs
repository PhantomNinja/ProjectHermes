using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int CheckArea;
    private CheckpointManager CheckpointManager;
    void Start()
    {
        CheckpointManager = CheckpointManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            CheckpointManager.currentCheckpoint = this;
            CheckpointManager.CheckArea = CheckArea;
        }
    }
}

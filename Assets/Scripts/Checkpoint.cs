using UnityEngine;

public class Checkpoint : MonoBehaviour
{
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
        }
    }
}

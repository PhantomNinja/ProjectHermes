using UnityEngine;

public class HarmfulObject : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        CheckpointManager.Instance.LoadCurrentCheckpoint();
    }
}

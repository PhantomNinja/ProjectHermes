using UnityEngine;

public class HarmfulObject : HermesObject
{
    private void OnCollisionEnter(Collision collision)
    {
        CheckpointManager.Instance.LoadCurrentCheckpoint();
    }
}

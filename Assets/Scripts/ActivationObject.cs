using UnityEngine;

public class ActivationObject : MonoBehaviour
{
    [Tooltip("This is the object to activate")]
    public TimedObject TimedObject;

    public void ActivateObject()
    {
        TimedObject.ActivateObject();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (TimedObject.IsRunning) return;
        if(other.tag == "Player")
        {
            ActivateObject();
        }
    }
}

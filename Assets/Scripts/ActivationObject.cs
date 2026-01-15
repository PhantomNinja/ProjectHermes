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
        Debug.Log("Hit");
        if(other.tag == "Player")
        {
            Debug.Log("Active");
            ActivateObject();
        }
    }
}

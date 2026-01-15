using UnityEngine;

public class ActivationObject : HermesObject
{
    [Tooltip("This is the object to activate")]
    public TimedObject TimedObject;
    public FadingPlatform FadingPlatform;

    public void ActivateObject()
    {
        TimedObject.ActivateObject();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (TimedObject)
        {
            if (TimedObject.IsRunning) return;
            if (other.tag == "Player")
            {
                ActivateObject();
            }
        }
        else
        {
            FadingPlatform.Toggle();
        }
    }

    public override void ResetObject()
    {
        base.ResetObject();
        if(FadingPlatform)
            FadingPlatform.ResetObject();
    }
}

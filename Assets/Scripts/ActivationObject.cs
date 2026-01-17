using UnityEngine;
using UnityEngine.TextCore;

public class ActivationObject : HermesObject
{
    [Tooltip("This is the object to activate")]
    public TimedObject TimedObject;
    public FadingPlatform FadingPlatform;
    public bool OneTimeUse = false;
    private bool wasUsed = false;
    public bool OnByDefault = false;

    public override void StartObject()
    {
        base.StartObject();
        if (FadingPlatform)
        {
            FadingPlatform.StartObject();
            if(OnByDefault)
                FadingPlatform.FadeIn();
            else
                FadingPlatform.FadeOut();

            if(FadingPlatform.GetComponent<MovingObject>() != null)
                FadingPlatform.GetComponent<MovingObject>().ObjectIsOn = OnByDefault;
        }
        wasUsed = !OneTimeUse;
    }

    public void ActivateObject()
    {
        if (wasUsed && OneTimeUse) return;
        TimedObject.ActivateObject();
        wasUsed = true;
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

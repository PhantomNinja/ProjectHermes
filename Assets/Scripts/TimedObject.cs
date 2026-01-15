using System.Collections;
using UnityEngine;

public class TimedObject : HermesObject
{
    [Tooltip("How long is the object on for")]
    public float ActiveTime;
    public bool IsRunning { private set; get; }
    public float activeTime { private set; get; } 
    private MovingObject mObject;
    private FadingPlatform fPlat;

    public override void StartObject()
    {
        // Update Manager with Active Timer
        mObject = GetComponent<MovingObject>();
        if (mObject)
        {
            mObject.ObjectIsOn = false;
            mObject.objectIsOn = false;
            mObject.CheckArea = CheckArea;
        }
        fPlat = GetComponent<FadingPlatform>();
        if (fPlat)
        {
            fPlat.StartObject();
            fPlat.CheckArea = CheckArea;
        }

        base.StartObject();
    }

    public override void UpdateObject()
    {
        if (activeTime <= 0) return;

        activeTime -= Time.deltaTime;
        if(activeTime <= 0)
        {
            ToggleObject();
        }
    }

    public void ActivateObject()
    {
        PlayerHUDManager.Instance.AddTimer(this);
        activeTime = ActiveTime;
        ToggleObject();
    }

    public override void ResetObject()
    {
        if(fPlat)
            fPlat.ResetObject();
        else if(mObject == null)
            base.ResetObject();
    }

    void ToggleObject()
    {
        IsRunning = !IsRunning;
        if (mObject)
        {
            mObject.objectIsOn = IsRunning;
            mObject.MoveTime = ActiveTime;
        }
        if (fPlat)
        {
            if(IsRunning) fPlat.FadeIn();
            else fPlat.FadeOut();
        }
    }
}

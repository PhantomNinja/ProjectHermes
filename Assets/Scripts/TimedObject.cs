using System.Collections;
using UnityEngine;

public class TimedObject : HermesObject
{
    [Tooltip("How long is the object on for")]
    public float OnTime;
    public bool IsRunning { private set; get; }
    private float onTime;
    private MovingObject mObject;
    private FadingPlatform fPlat;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void StartObject()
    {
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

    // Update is called once per frame
    public override void UpdateObject()
    {
        if (onTime <= 0) return;

        onTime -= Time.deltaTime;
        if(onTime <= 0)
        {
            ToggleObject();
        }
    }

    public void ActivateObject()
    {
        onTime = OnTime;
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
            mObject.MoveTime = OnTime;
        }
        if (fPlat)
        {
            if(IsRunning) fPlat.FadeIn();
            else fPlat.FadeOut();
        }
    }
}

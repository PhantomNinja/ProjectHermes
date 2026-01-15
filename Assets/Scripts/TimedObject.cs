using System.Collections;
using UnityEngine;

public class TimedObject : MonoBehaviour
{
    [Tooltip("How long is the object on for")]
    public float OnTime;
    public bool IsRunning { private set; get; }
    private float onTime;
    private MovingObject mObject;
    private FadingPlatform fPlat;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mObject = GetComponent<MovingObject>();
        if(mObject)
            mObject.ObjectIsOn = false;
        fPlat = GetComponent<FadingPlatform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (onTime <= 0) return;

        onTime -= Time.deltaTime;
        Debug.Log(onTime);
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

    void ToggleObject()
    {
        IsRunning = !IsRunning;
        if (mObject)
        {
            mObject.ObjectIsOn = IsRunning;
            mObject.MoveTime = OnTime;
        }
        if (fPlat)
        {
            if(IsRunning) fPlat.FadeIn();
            else fPlat.FadeOut();
        }
    }
}

using System.Collections;
using UnityEngine;

public class TimedObject : MonoBehaviour
{
    [Tooltip("How long is the object on for")]
    public float OnTime;
    public bool IsRunning { private set; get; }
    private float onTime;
    private MovingObject mObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mObject = GetComponent<MovingObject>();
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
        if (mObject)
        {
            mObject.ObjectIsOn = !mObject.ObjectIsOn;
        }
        IsRunning = !IsRunning;
    }
}

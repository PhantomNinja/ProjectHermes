using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectsManager : MonoBehaviour
{
    public TimedObject[] TimedObjects;
    public MovingObject[] MovingObjects;
    public HarmfulObject[] HarmfulObjects;

    public void WakeUpManager()
    {
        TimedObjects = FindObjectsByType<TimedObject>(FindObjectsSortMode.None);
        MovingObjects = FindObjectsByType<MovingObject>(FindObjectsSortMode.None);
        HarmfulObjects = FindObjectsByType<HarmfulObject>(FindObjectsSortMode.None);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartManager()
    {
        foreach (var obj in TimedObjects)
        {
            obj.StartObject();
        }
        foreach (var obj in MovingObjects)
        {
            obj.StartObject();
        }
        foreach (var obj in HarmfulObjects)
        {
            obj.StartObject();
        }
    }

    // Update is called once per frame
    public void UpdateManager()
    {

        foreach (var obj in TimedObjects)
        {
            obj.UpdateObject();
        }
        foreach (var obj in HarmfulObjects)
        {
            obj.UpdateObject();
        }
    }

    public void FixedUpdateManager()
    {
        foreach (var obj in MovingObjects)
        {
            obj.FixedUpdateObject();
        }
    }

    public void ResetObjects()
    {

        foreach (var obj in TimedObjects)
        {
            obj.ResetObject();
        }
        foreach (var obj in MovingObjects)
        {
            obj.ResetObject();
        }
        foreach (var obj in HarmfulObjects)
        {
            obj.ResetObject();
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

public class ObjectsManager : MonoBehaviour
{
    public static ObjectsManager Instance;

    public TimedObject[] TimedObjects;
    public MovingObject[] MovingObjects;
    public HarmfulObject[] HarmfulObjects;

    public void WakeUpManager()
    {
        Instance = this;
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
        int currArea = CheckpointManager.Instance.CheckArea;
        foreach (var obj in TimedObjects)
        {
            if(obj.CheckArea == currArea)
                obj.ResetObject();
        }
        foreach (var obj in MovingObjects)
        {
            if (obj.CheckArea == currArea)
                obj.ResetObject();
        }
        foreach (var obj in HarmfulObjects)
        {
            if (obj.CheckArea == currArea)
                obj.ResetObject();
        }
    }
}

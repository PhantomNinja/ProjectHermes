using UnityEngine;
using UnityEngine.UI;

public class ActiveTimerHolder : MonoBehaviour
{
    private TimedObject TimedObject;
    public Image Foreground_IMG;

    public void StartTimerHolder(TimedObject timedObject)
    {
        TimedObject = timedObject;
    }

    public void UpdateHolder()
    {
        Foreground_IMG.fillAmount = TimedObject.activeTime / TimedObject.ActiveTime;
    }
}

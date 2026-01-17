using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerHUDManager : MonoBehaviour
{
    public static PlayerHUDManager Instance;
    [SerializeField]
    private TMP_Text MainTimer_TXT;
    private List<ActiveTimerHolder> ActiveTimerHolders = new List<ActiveTimerHolder>();
    [SerializeField]
    private Transform ActiveTimerHolders_PRNT;
    [SerializeField]
    private ActiveTimerHolder ActiveTimerHolder_PRFB;

    public void WakeUpManager()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartManager()
    {
        
    }

    // Update is called once per frame
    public void UpdateManager(Vector2 timer)
    {
        UpdateMainTimer(timer);
        UpdateActiveTimers();
    }

    public void AddTimer(TimedObject timedObject)
    {
        ActiveTimerHolder holder = Instantiate(ActiveTimerHolder_PRFB, ActiveTimerHolders_PRNT);
        holder.StartTimerHolder(timedObject);
        ActiveTimerHolders.Add(holder);
    }

    private void UpdateMainTimer(Vector2 _timer)
    {
        int secondsI = (int)_timer.x;
        string seconds = secondsI < 10 ? "0" + secondsI : secondsI.ToString();
        string minutes = _timer.y < 10 ? "0" + _timer.y : _timer.y.ToString();
        string timer = minutes + ":" + seconds;
        MainTimer_TXT.text = timer;
    }

    private void UpdateActiveTimers()
    {
        List<ActiveTimerHolder> removeList = new List<ActiveTimerHolder>();
        foreach (ActiveTimerHolder holder in ActiveTimerHolders)
        {
            holder.UpdateHolder();
            if (holder.Foreground_IMG.fillAmount <= 0)
                removeList.Add(holder);
        }
        foreach (ActiveTimerHolder holder in removeList)
        {
            Destroy(holder.gameObject);
            ActiveTimerHolders.Remove(holder);
        }
    }

    public void RemoveTimers()
    {
        foreach(var holder in ActiveTimerHolders)
        {
            Destroy(holder.gameObject);
        }
        ActiveTimerHolders.Clear();
    }
}

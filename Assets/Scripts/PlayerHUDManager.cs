using System.Collections.Generic;
using UnityEngine;

public class PlayerHUDManager : MonoBehaviour
{
    public static PlayerHUDManager Instance;
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
    public void UpdateManager()
    {
        List<ActiveTimerHolder> removeList = new List<ActiveTimerHolder>();
        foreach(ActiveTimerHolder holder in ActiveTimerHolders)
        {
            holder.UpdateHolder();
            if(holder.Foreground_IMG.fillAmount <= 0)
                removeList.Add(holder);
        }
        foreach(ActiveTimerHolder holder in removeList)
        {
            Destroy(holder.gameObject);
            ActiveTimerHolders.Remove(holder);
        }
    }

    public void AddTimer(TimedObject timedObject)
    {
        ActiveTimerHolder holder = Instantiate(ActiveTimerHolder_PRFB, ActiveTimerHolders_PRNT);
        holder.StartTimerHolder(timedObject);
        ActiveTimerHolders.Add(holder);
    }
}

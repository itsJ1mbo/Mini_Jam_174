using System;
using UnityEngine;

public class StateHandler : MonoBehaviour
{
    void Start()
    {
        DungeonMaster.Instance.AddEntity(this.gameObject);
    }
    
    public void UpdateState(TimePeriod timePeriod, Flags flags)
    {
        switch (timePeriod)
        {
            case TimePeriod.Morning:
                break;
            case TimePeriod.Afternoon:
                break;
            case TimePeriod.Evening:
                break;
            case TimePeriod.EndGame:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(timePeriod), timePeriod, null);
        }
    }
}

using System;
using UnityEngine;

public class TechnicianStateHandler : StateHandler
{ 
    public override void UpdateState()
    {
        switch (DungeonMaster.Instance.GetCurrentTimePeriod())
        {
            case TimePeriod.Morning:
                transform.position = positions[0];
                break;
            case TimePeriod.Afternoon:
                if(DungeonMaster.Instance.GetFlag().HasFlag(Flags.TechnicianDead))
                   transform.position = positions[3];
                else
                    transform.position = positions[1];
                break;
            case TimePeriod.Evening:
                if(DungeonMaster.Instance.GetFlag().HasFlag(Flags.TechnicianDead))
                    transform.position = positions[3];
                else
                    transform.position = positions[2];
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

using System;
using Unity.VisualScripting;
using UnityEngine;

public class GuardStateHandler : StateHandler
{
    public override void UpdateState()
    {
        _script = GetComponent<Guion>();
        switch (DungeonMaster.Instance.GetCurrentTimePeriod())
        {
            case TimePeriod.Morning:
                transform.position = positions[0];
                _script.ActiveStory = _script.MorningStories[0];
                break;
            case TimePeriod.Afternoon:
                transform.position = positions[1];
                _script.ActiveStory = _script.AfternoonStories[0];
                break;
            case TimePeriod.Evening:
                Flags drugged = Flags.TeaPosioned | Flags.TeaPrepared;
                Flags drank = Flags.TeaPrepared;
                if(DungeonMaster.Instance.GetFlags().HasFlag(drugged))
                {
                    transform.position = positions[3];
                    _script.ActiveStory = _script.NightStories[2];
                    DungeonMaster.Instance.SetFlag(Flags.DrankTea);
                }
                else if(DungeonMaster.Instance.GetFlags().HasFlag(drank))
                {
                    transform.position = positions[2];
                    _script.ActiveStory = _script.NightStories[1];
                    DungeonMaster.Instance.SetFlag(Flags.DrankTea);
                }
                else
                {
                    transform.position = positions[2];
                    _script.ActiveStory = _script.NightStories[0];
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

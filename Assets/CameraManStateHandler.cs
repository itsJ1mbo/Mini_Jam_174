using System;
using UnityEngine;

public class CameraManStateHandler : StateHandler
{
    private Guion _script;

    // Update is called once per frame
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
                if(DungeonMaster.Instance.GetFlags().HasFlag(Flags.CamerasSabotaged))
                {
                    transform.position = positions[2];
                    _script.ActiveStory = _script.NightStories[1];
                    transform.rotation = rotations[2];
                }
                else
                {
                    transform.position = positions[1];
                    _script.ActiveStory = _script.NightStories[0];
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

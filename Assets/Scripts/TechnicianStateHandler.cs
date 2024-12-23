using System;
using Unity.VisualScripting;
using UnityEngine;

public class TechnicianStateHandler : StateHandler
{ 
    public override void UpdateState()
    {
        _script = GetComponent<Guion>();
        
        switch (DungeonMaster.Instance.GetCurrentTimePeriod())
        {
            case TimePeriod.Morning:
                transform.position = positions[0];
                transform.rotation = rotations[0];
                _script.ActiveStory = _script.MorningStories[0];
                break;
            case TimePeriod.Afternoon:
                if(DungeonMaster.Instance.GetFlags().HasFlag(Flags.TechnicianDead))
                {
                    transform.position = positions[1];
                    _script.ActiveStory = _script.AfternoonStories[1];
                    transform.rotation = rotations[1];
                }
                else
                {
                    transform.position = positions[1];
                    _script.ActiveStory = _script.AfternoonStories[0];
                    transform.rotation = rotations[1];
                    foreach (GameObject cam in DungeonMaster.Instance.GetCams())
                    {
                        cam.GetComponent<SpriteRenderer>().color = Color.white;
                    }
                    
                }
                break;
            case TimePeriod.Evening:
                if(DungeonMaster.Instance.GetFlags().HasFlag(Flags.TechnicianDead))
                {
                    transform.position = positions[1];
                    _script.ActiveStory = _script.NightStories[1];
                    transform.rotation = rotations[1];
                }
                else
                {
                    transform.position = positions[2];
                    transform.rotation = rotations[2];
                    _script.ActiveStory = _script.NightStories[0];
                    foreach (GameObject cam in DungeonMaster.Instance.GetCams())
                    {
                        cam.GetComponent<SpriteRenderer>().color = Color.white;
                    }
                    
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

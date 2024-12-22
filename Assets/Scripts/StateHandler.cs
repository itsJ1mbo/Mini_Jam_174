using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class StateHandler : MonoBehaviour
{
    [SerializeField] 
    protected Vector3[] positions = new Vector3[3]; 
    [SerializeField] 
    protected Quaternion[] rotations = new Quaternion[3];

    protected Guion _script;
    
    void Start()
    {
        DungeonMaster.Instance.RegisterEntity(this.gameObject);
    }
    
    public virtual void UpdateState()
    {
        _script = GetComponent<Guion>();
        
        switch (DungeonMaster.Instance.GetCurrentTimePeriod())
        {
            case TimePeriod.Morning:
                if(((DungeonMaster.Instance.GetFlags().HasFlag(Flags.TeaPrepared) &&
                       DungeonMaster.Instance.GetFlags().HasFlag(Flags.TeaPosioned) &&
                       DungeonMaster.Instance.GetFlags().HasFlag(Flags.DrankTea) &&
                       gameObject.name == "Laura" || gameObject.name == "Mark") &&
                    (!DungeonMaster.Instance.GetFlags().HasFlag(Flags.CamerasSabotaged) &&
                     DungeonMaster.Instance.GetFlags().HasFlag(Flags.TechnicianDead))) ||
                    (DungeonMaster.Instance.GetFlags().HasFlag(Flags.CamerasSabotaged) &&
                    gameObject.name == "Raymond") ||
                    (DungeonMaster.Instance.GetFlags().HasFlag(Flags.TechnicianDead) &&
                     gameObject.name == "Joy") ||
                    (DungeonMaster.Instance.GetFlags().HasFlag(Flags.LightsOut) &&
                     gameObject.name == "Jacob"))
                {
                    transform.position = positions[3];
                    transform.rotation = rotations[3];
                    _script.ActiveStory = _script.MorningStories[1];
                }
                else
                {
                    transform.position = positions[0];
                    transform.rotation = rotations[0];
                    _script.ActiveStory = _script.MorningStories[0];
                }
                break;
            case TimePeriod.Afternoon:
                if(((DungeonMaster.Instance.GetFlags().HasFlag(Flags.TeaPrepared) &&
                        DungeonMaster.Instance.GetFlags().HasFlag(Flags.TeaPosioned) &&
                        DungeonMaster.Instance.GetFlags().HasFlag(Flags.DrankTea) &&
                        gameObject.name == "Laura" || gameObject.name == "Mark") &&
                    (!DungeonMaster.Instance.GetFlags().HasFlag(Flags.CamerasSabotaged) &&
                     DungeonMaster.Instance.GetFlags().HasFlag(Flags.TechnicianDead))) ||
                   (DungeonMaster.Instance.GetFlags().HasFlag(Flags.CamerasSabotaged) &&
                    gameObject.name == "Raymond") ||
                   (DungeonMaster.Instance.GetFlags().HasFlag(Flags.TechnicianDead) &&
                    gameObject.name == "Joy") ||
                   (DungeonMaster.Instance.GetFlags().HasFlag(Flags.LightsOut) &&
                    gameObject.name == "Jacob"))
                {
                    transform.position = positions[3];
                    transform.rotation = rotations[3];
                    _script.ActiveStory = _script.AfternoonStories[1];
                }
                else
                {
                    transform.position = positions[1];
                    transform.rotation = rotations[1];
                    _script.ActiveStory = _script.AfternoonStories[0];
                }
                break;
            case TimePeriod.Evening:
                if(((DungeonMaster.Instance.GetFlags().HasFlag(Flags.TeaPrepared) &&
                        DungeonMaster.Instance.GetFlags().HasFlag(Flags.TeaPosioned) &&
                        DungeonMaster.Instance.GetFlags().HasFlag(Flags.DrankTea) &&
                        gameObject.name == "Laura" || gameObject.name == "Mark") &&
                    (!DungeonMaster.Instance.GetFlags().HasFlag(Flags.CamerasSabotaged) &&
                     DungeonMaster.Instance.GetFlags().HasFlag(Flags.TechnicianDead))) ||
                   (DungeonMaster.Instance.GetFlags().HasFlag(Flags.CamerasSabotaged) &&
                    gameObject.name == "Raymond") ||
                   (DungeonMaster.Instance.GetFlags().HasFlag(Flags.TechnicianDead) &&
                    gameObject.name == "Joy") ||
                   (DungeonMaster.Instance.GetFlags().HasFlag(Flags.LightsOut) &&
                    gameObject.name == "Jacob"))
                {
                    transform.position = positions[3];
                    transform.rotation = rotations[3];
                    _script.ActiveStory = _script.NightStories[1];
                }
                else
                {
                    transform.position = positions[2];
                    transform.rotation = rotations[2];
                    _script.ActiveStory = _script.NightStories[0];
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

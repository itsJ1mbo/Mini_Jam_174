using System;
using UnityEngine;

public class ElecStateHandler : StateHandler
{
    [SerializeField] private Sprite _normalSprite;
    
    public override void UpdateState()
    {
        switch (DungeonMaster.Instance.GetCurrentTimePeriod())
        {
            case TimePeriod.Morning:
                if (DungeonMaster.Instance.GetFlags().HasFlag(Flags.LightsOut))
                    GetComponent<SpriteRenderer>().sprite = _normalSprite;
                break;
            case TimePeriod.Afternoon:
                if (DungeonMaster.Instance.GetFlags().HasFlag(Flags.LightsOut))
                    GetComponent<SpriteRenderer>().sprite = _normalSprite;
                break;
            case TimePeriod.Evening:
                if (DungeonMaster.Instance.GetFlags().HasFlag(Flags.LightsOut))
                    GetComponent<SpriteRenderer>().sprite = _normalSprite;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

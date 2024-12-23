using UnityEngine;

public class KillInteraction : FlagInteraction
{
    private bool _killed;
    
    public override void activateFlag()
    {
        if (!_killed &&
            DungeonMaster.Instance.GetCurrentTimePeriod() == TimePeriod.Afternoon &&
            DungeonMaster.Instance.GetFlags().HasFlag(Flags.HasWeapon))
        {
            _killed = true;
            DungeonMaster.Instance.SetFlag(_flag);
        }
    }
}

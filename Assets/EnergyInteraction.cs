using UnityEngine;

public class EnergyInteraction : FlagInteraction
{
    private bool _cut = false;
    
    public override void activateFlag()
    {
        if(DungeonMaster.Instance.GetFlags().HasFlag(Flags.HasScissors) && !_cut)
        {
            DungeonMaster.Instance.SetFlag(_flag);
            _cut = true;
        }
    }
}

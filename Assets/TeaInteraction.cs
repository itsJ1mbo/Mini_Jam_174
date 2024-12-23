using UnityEngine;

public class TeaInteraction : FlagInteraction
{
    [SerializeField] private GameObject _cup;
    
    public override void activateFlag()
    {
        Debug.Log("ENTRO");
        Debug.Log(!DungeonMaster.Instance.GetFlags().HasFlag(Flags.TeaPrepared));
        if (DungeonMaster.Instance.GetFlags().HasFlag(Flags.TeaPrepared) &&
            DungeonMaster.Instance.GetFlags().HasFlag(Flags.HasDrugs))
        {
            DungeonMaster.Instance.SetFlag(Flags.TeaPosioned);
        }
        else if(!DungeonMaster.Instance.GetFlags().HasFlag(Flags.TeaPrepared))
        {
            DungeonMaster.Instance.SetFlag(Flags.TeaPrepared);
            _cup.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}

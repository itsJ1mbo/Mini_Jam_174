using System;
using UnityEngine;

public class FlagInteraction : MonoBehaviour
{
    [SerializeField]
    protected Flags _flag = Flags.None;

    [SerializeField] protected bool _disappearOnInteraction = false;
    public virtual void activateFlag()
    {
        DungeonMaster.Instance.SetFlag(_flag);
        if (_disappearOnInteraction)
        {
            DungeonMaster.Instance.DeregisterEntity(this.gameObject);
            Destroy(this.gameObject);
        }
    }
}

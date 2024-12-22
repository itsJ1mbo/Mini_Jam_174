using System;
using UnityEngine;

public class FlagInteraction : MonoBehaviour
{
    [SerializeField]
    private Flags _flag = Flags.None;

    [SerializeField] private bool _disappearOnInteraction = false;
    public void activateFlag()
    {
        DungeonMaster.Instance.SetFlag(_flag);
        if (_disappearOnInteraction)
        {
            DungeonMaster.Instance.DeregisterEntity(this.gameObject);
            Destroy(this.gameObject);
        }
    }
}

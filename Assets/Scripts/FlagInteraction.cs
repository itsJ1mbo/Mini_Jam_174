using System;
using UnityEngine;

public class FlagInteraction : MonoBehaviour
{
    [SerializeField]
    private Flags _flag = Flags.None;

    public void activateFlag()
    {
        Debug.Log(DungeonMaster.Instance);
        DungeonMaster.Instance.SetFlag(_flag);
    }
}

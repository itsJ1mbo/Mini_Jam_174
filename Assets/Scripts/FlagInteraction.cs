using System;
using UnityEngine;

public class FlagInteraction : MonoBehaviour
{
    [SerializeField]
    private Flags _flag = Flags.None;

    private void Start()
    {
        
    }

    public void activateFlag()
    {
        DungeonMaster.Instance.SetFlag(_flag);
    }
}

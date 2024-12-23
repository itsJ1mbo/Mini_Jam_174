using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class FogComponent : MonoBehaviour
{
    void Start()
    {
        DungeonMaster.Instance.RegisterFog(this.gameObject);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.GetComponent<PlayerInput>())
            DungeonMaster.Instance.updateFog(this.gameObject);
    }
}

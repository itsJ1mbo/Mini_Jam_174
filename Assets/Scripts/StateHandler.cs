using System;
using UnityEngine;
using UnityEngine.Events;

public class StateHandler : MonoBehaviour
{
    [SerializeField] Vector3[] positions = new Vector3[3]; 
    void Start()
    {
        DungeonMaster.Instance.AddEntity(this.gameObject);
    }
    
    public void UpdateState()
    {
        transform.position = positions[(int)DungeonMaster.Instance.GetCurrentTimePeriod()];
    }
}

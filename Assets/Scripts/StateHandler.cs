using System;
using UnityEngine;
using UnityEngine.Events;

public class StateHandler : MonoBehaviour
{
    [SerializeField] 
    protected Vector3[] positions = new Vector3[3]; 
    void Start()
    {
        DungeonMaster.Instance.RegisterEntity(this.gameObject);
    }
    
    public virtual void UpdateState()
    {
        transform.position = positions[(int)DungeonMaster.Instance.GetCurrentTimePeriod()];
    }
}

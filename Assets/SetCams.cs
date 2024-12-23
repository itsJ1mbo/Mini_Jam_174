using UnityEngine;

public class SetCams : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DungeonMaster.Instance.SetCam(gameObject);
    }
}

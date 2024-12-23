using UnityEngine;

public class CamInteraction : FlagInteraction
{
    public override void activateFlag()
    {
        DungeonMaster.Instance.SetFlag(_flag);

        foreach (GameObject cam in DungeonMaster.Instance.GetCams())
        {
            cam.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
}

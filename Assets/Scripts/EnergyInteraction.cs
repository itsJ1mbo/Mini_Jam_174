using UnityEngine;

public class EnergyInteraction : FlagInteraction
{
    [SerializeField] private Sprite _cutSprite;
    
    private bool _cut = false;
    [SerializeField] CameraManStateHandler _cameraManStateHandler;
    public override void activateFlag()
    {
        if(DungeonMaster.Instance.GetFlags().HasFlag(Flags.HasScissors) && !_cut)
        {
            DungeonMaster.Instance.SetFlag(_flag);
            _cameraManStateHandler.LightsOut();
            GetComponent<SpriteRenderer>().sprite = _cutSprite;
            _cut = true;
        }
    }
}

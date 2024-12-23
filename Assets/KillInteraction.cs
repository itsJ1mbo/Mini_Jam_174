using UnityEngine;

public class KillInteraction : FlagInteraction
{
    private bool _killed;
    [SerializeField] private Sprite _killedSprite;
    
    public override void activateFlag()
    {
        if (!_killed &&
            DungeonMaster.Instance.GetCurrentTimePeriod() == TimePeriod.Afternoon &&
            DungeonMaster.Instance.GetFlags().HasFlag(Flags.HasWeapon))
        {
            _killed = true;
            GetComponent<SpriteRenderer>().sprite = _killedSprite;
            GetComponent<SpriteRenderer>().color = Color.white;
            GetComponent<Guion>().ActiveStory = GetComponent<Guion>().AfternoonStories[1];
            DungeonMaster.Instance.SetFlag(_flag);
        }
    }
}

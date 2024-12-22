using System.Collections.Generic;
using System.Linq;
using Ink.Runtime;
using UnityEngine;

public class Guion : MonoBehaviour
{
    [SerializeField] private PlayerMovement _player;
    
    // Set this file to your compiled json asset
    [SerializeField] private List<TextAsset> _morningAssets;
    [SerializeField] private List<TextAsset> _afternoonAssets;
    [SerializeField] private List<TextAsset> _nightAssets;
    
    [HideInInspector] public List<Story> MorningStories { get; private set; }
    [HideInInspector] public List<Story> AfternoonStories { get; private set; }
    [HideInInspector] public List<Story> NightStories { get; private set; }
    
    private UIManager _ui;
    public bool Talking { get; private set; }

    [HideInInspector] public Story ActiveStory { get; set; }
    
    public void StartDialogue()
    {
    //     TimePeriod time = DungeonMaster.Instance.GetCurrentTimePeriod();
    //     if (DungeonMaster.Instance.GetFlags().HasFlag(Flags.TeaPrepared) && time == TimePeriod.Evening && gameObject.name == "Gilly")
    //     {
    //         _activeStory = _nightStories[1];
    //     }
    //     else if (DungeonMaster.Instance.GetFlags().HasFlag(Flags.CamerasSabotaged) && time == TimePeriod.Evening &&
    //         gameObject.name == "Willy")
    //     {
    //         _activeStory = _nightStories[1];
    //     }
    //     else if (DungeonMaster.Instance.GetFlags().HasFlag(Flags.LightsOut) && gameObject.name == "Willy")
    //     {
    //         _activeStory = _afternoonStories[1];
    //     }
    //     else if(time == TimePeriod.Morning)
    //     {
    //         _activeStory = _morningStories[0];
    //     }
    //     else if(time == TimePeriod.Afternoon)
    //     {
    //         _activeStory = _afternoonStories[0];
    //     }
    //     else if(time == TimePeriod.Evening)
    //     {
    //         _activeStory = _nightStories[0];
    //     }
        
        if (!ActiveStory.canContinue) return;
        
        Talking = true;

        _ui.Script = this;
        _ui.ChangeDialogue(ActiveStory.Continue(), SpeakerName(ActiveStory.currentTags));
    }
    
    public void NextLine()
    {
        if (ActiveStory.canContinue)
        {
            _ui.ChangeDialogue(ActiveStory.Continue(), SpeakerName(ActiveStory.currentTags));
        }
        else
        {
            Talking = false;
            ActiveStory.ResetState();
            _player.ToggleMove();
            DungeonMaster.Instance.ToggleTimer();
            _ui.OnDialogueEnd();
        }
    }

    private string SpeakerName(List<string> tags)
    {
        foreach(string t in tags)
        {
            string[] splitTag = t.Split(":");
            if (splitTag[0].Trim() == "speaker")
                return splitTag[1].Trim();
        }

        return "";
    }

    private void SetStories()
    {
        foreach (TextAsset t in _morningAssets)
        {
            Story s = new Story(t.text);
            if (gameObject.GetComponent<FlagInteraction>())
            {
                s.BindExternalFunction("ActivateFlag", () =>
                {
                    gameObject.GetComponent<FlagInteraction>().activateFlag();
                });
            }
            
            MorningStories.Add(s);
        }
        foreach (TextAsset t in _afternoonAssets)
        {
            Story s = new Story(t.text);
            if (gameObject.GetComponent<FlagInteraction>())
            {
                s.BindExternalFunction("ActivateFlag", () =>
                {
                    gameObject.GetComponent<FlagInteraction>().activateFlag();
                });
            }
            
            AfternoonStories.Add(s);
        }
        foreach (TextAsset t in _nightAssets)
        {
            Story s = new Story(t.text);
            if (gameObject.GetComponent<FlagInteraction>())
            {
                s.BindExternalFunction("ActivateFlag", () =>
                {
                    gameObject.GetComponent<FlagInteraction>().activateFlag();
                });
            }
            
            NightStories.Add(s);
        }
    }

    private void Start()
    {
        _ui = DungeonMaster.Instance.GetUIManager();
        MorningStories = new List<Story>();
        AfternoonStories = new List<Story>();
        NightStories = new List<Story>();
        SetStories();
    }
}

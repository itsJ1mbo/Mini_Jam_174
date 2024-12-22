using System.Collections.Generic;
using System.Linq;
using Ink.Runtime;
using UnityEngine;

public class Guion : MonoBehaviour
{
    [SerializeField] private PlayerMovement _player;
    
    // Set this file to your compiled json asset
    [SerializeField] private List<TextAsset> _textAssets;
    private List<Story> _inkStories;
    
    private UIManager _ui;
    public bool Talking { get; private set; }

    public void StartDialogue()
    {
        Story activeStory = _inkStories[0];
        if (!activeStory.canContinue) return;
        
        Talking = true;

        _ui.Script = this;
        _ui.ChangeDialogue(activeStory.Continue(), SpeakerName(activeStory.currentTags));
    }
    
    public void NextLine()
    {
        //Vector2 point;
        Story activeStory = _inkStories[0];
        
        if (activeStory.canContinue)
        {
            _ui.ChangeDialogue(activeStory.Continue(), SpeakerName(activeStory.currentTags));
        }
        else
        {
            Talking = false;
            activeStory.ResetState();
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
        foreach (TextAsset t in _textAssets)
        {
            Story s = new Story(t.text);
            if (gameObject.GetComponent<FlagInteraction>())
            {
                s.BindExternalFunction("ActivateFlag", () =>
                {
                    gameObject.GetComponent<FlagInteraction>().activateFlag();
                });
            }
            
            _inkStories.Add(s);
        }
    }

    private void Start()
    {
        _ui = DungeonMaster.Instance.GetUIManager();
        _inkStories = new List<Story>();
        SetStories();
    }
}

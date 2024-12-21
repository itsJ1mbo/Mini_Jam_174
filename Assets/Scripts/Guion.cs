using System.Collections.Generic;
using System.Linq;
using Ink.Runtime;
using UnityEngine;

public class Guion : MonoBehaviour
{
    [SerializeField] private UIManager _ui;
    
    // Set this file to your compiled json asset
    [SerializeField] private List<TextAsset> _textAssets;
    private List<Story> _inkStories;
    public bool Talking { get; private set; }

    public void StartDialogue()
    {
        Talking = true;
        
        Story activeStory = _inkStories[0];
        
        if (activeStory.canContinue)
        {
            _ui.ChangeDialogue(activeStory.Continue());
        }
    }
    
    public void NextLine()
    {
        Vector2 point;
        Story activeStory = _inkStories[0];
        
        if (activeStory.canContinue)
        {
            _ui.ChangeDialogue(activeStory.Continue());
        }
        else
        {
            Talking = false;
            activeStory.ResetState();
            _ui.OnDialogueEnd();
        }
    }

    private void SetStories()
    {
        foreach (TextAsset t in _textAssets)
        {
            _inkStories.Add(new Story(t.text));
            if (gameObject.GetComponent<FlagInteraction>())
            {
                _inkStories.Last().BindExternalFunction("ActivateFlag", () =>
                {
                    gameObject.GetComponent<FlagInteraction>().activateFlag();
                });
            }
        }
    }

    private void Start()
    {
        _inkStories = new List<Story>();

        SetStories();
    }
}

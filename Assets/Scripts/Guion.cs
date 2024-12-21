using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

public class Guion : MonoBehaviour
{
    // Set this file to your compiled json asset
    [SerializeField] private List<TextAsset> _textAssets;
    private List<Story> _inkStories;

    public void NextLine()
    {
        // Vector2 point;
        // Story activeStory = _inkStories[(int)point.x - 1][(int)point.y];
        //
        // if (activeStory.canContinue)
        // {
        //     Debug.Log("LINEA");
        //     GameManager.Instance.NextLine(activeStory.Continue());
        // }
        // else
        // {
        //     activeStory.ResetState();
        //     gameObject.GetComponent<NPC>().ResumeMovement();
        //     GameManager.Instance.DialogueEnded();
        // }
    }

    private void SetStories()
    {
        foreach (TextAsset t in _textAssets)
        {
            _inkStories.Add(new Story(t.text));
        }
    }

    private void Start()
    {
        _textAssets = new List<TextAsset>();
        _inkStories = new List<Story>();

        SetStories();
    }
}

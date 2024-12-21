using TMPro;
using System.Collections;
using NUnit.Framework.Internal.Commands;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _dialogue;
    [SerializeField] private TMP_Text _message;
    private string _sentence;

    public bool Typing { get; private set; }

    public void ChangeDialogue(string text)
    {
        Debug.Log(text + " " + Typing);
        _dialogue.SetActive(true);
        _sentence = text;
        StartCoroutine(TypeSentence());
    }

    public void OnDialogueEnd()
    {
        _dialogue.SetActive(false);
    }

    private IEnumerator TypeSentence()
    {
        Typing = true;
        
        _message.text = "";
        foreach (char letter in _sentence)
        {
            _message.text += letter;
            yield return new WaitForSeconds(0.1f);
        }

        Typing = false;
    }

    public void Skip()
    {
        if (!Typing) return;

        StopAllCoroutines();
        _message.text = _sentence;
        Typing = false;
    }
}

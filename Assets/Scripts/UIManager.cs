using TMPro;
using System.Collections;
using NUnit.Framework.Internal.Commands;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _dialogue;
    [SerializeField] private TMP_Text _message;
    private string _sentence;

    private bool _typing;

    public void ChangeDialogue(string text)
    {
        Debug.Log(text);
        switch (_typing)
        {
            case false when text.Length != 0:
                Debug.Log("UI LINEA");
                _dialogue.SetActive(true);
                _sentence = text;
                StopAllCoroutines();
                StartCoroutine(TypeSentence());
                break;
            case true:
                Skip();
                break;
        }
    }

    public void OnDialogueEnd()
    {
        _dialogue.SetActive(false);
    }

    private IEnumerator TypeSentence()
    {
        _typing = true;
        _message.text = "";
        foreach (char letter in _sentence.ToCharArray())
        {
            _message.text += letter;
            yield return new WaitForSeconds(0.1f);
        }

        _typing = false;
    }

    private void Skip()
    {
        if (!_typing) return;
        
        StopAllCoroutines();
        _message.text = _sentence;
        _typing = false;
    }
}

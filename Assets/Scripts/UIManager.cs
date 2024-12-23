using TMPro;
using System.Collections;
using NUnit.Framework.Internal.Commands;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public Guion Script { get; set; }
    [SerializeField] private GameObject _dialogue;
    [SerializeField] private TMP_Text _message;
    [SerializeField] private TMP_Text _name;
    private string _sentence;

    [FormerlySerializedAs("_clock1")] [SerializeField] private RectTransform _clockBig;
    [FormerlySerializedAs("_clock2")] [SerializeField] private RectTransform _clockLittle;
    public bool Typing { get; private set; }

    public void ChangeDialogue(string text, string speakerName)
    {
        if (text.Length <= 0)
        { 
            Script.NextLine();
            return;
        }
        
        
        _dialogue.SetActive(true);
        _sentence = text;
        _name.text = speakerName;
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

    public void UpdateClock(float speed)
    {
        _clockBig.Rotate(-Vector3.forward, speed * Time.deltaTime);
        _clockLittle.Rotate(-Vector3.forward, speed * 60 * Time.deltaTime);
    }
}

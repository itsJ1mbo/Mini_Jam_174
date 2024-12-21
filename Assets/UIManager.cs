using TMPro;
using System.Collections;
using NUnit.Framework.Internal.Commands;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject intro1;
    public GameObject intro2;
    public GameObject intro3;
    public GameObject intro4;

    public GameObject _furbo;
    [SerializeField] private GameObject _dialogue;
    [SerializeField] private TMP_Text _message;
    private string _sentence;
    public bool Typing { get; private set; }

    public void ChangeDialogue(string text)
    {
        Debug.Log(text);
        if (!Typing && text.Length != 0) 
        {
            Debug.Log("UI LINEA");
            _dialogue.SetActive(true);
            _sentence = text;
            StopAllCoroutines();
            StartCoroutine(TypeSentence());
        }
    }

    public void OnDialogueEnd()
    {
        _dialogue.SetActive(false);
    }

    private IEnumerator TypeSentence()
    {
        Typing = true;
        _message.text = "";
        foreach (char letter in _sentence.ToCharArray())
        {
            _message.text += letter;
            yield return new WaitForSeconds(0.1f);
        }

        Typing = false;
    }

    public void Skip()
    {
        if (Typing) {
            StopAllCoroutines();
            _message.text = _sentence;
            Typing = false;
        }
    }
    
    public void DissapearObject(GameObject myObj)
    {
        myObj.SetActive(false);
    }

    public void AppearObject(GameObject myObj)
    {
        myObj.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

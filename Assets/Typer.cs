using System.Collections;
using TMPro;
using UnityEngine;

public class Typer : MonoBehaviour
{
    private TMP_Text _message;
    private string _sentence;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _message = GetComponent<TMP_Text>();
        _sentence = _message.text;
        StartCoroutine(TypeSentence());
    }
    
    private IEnumerator TypeSentence()
    {
        _message.text = "";
        foreach (char letter in _sentence)
        {
            _message.text += letter;
            yield return new WaitForSeconds(0.1f);
        }
    }
}

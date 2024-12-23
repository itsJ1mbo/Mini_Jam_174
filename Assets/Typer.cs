using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Typer : MonoBehaviour
{
    private TMP_Text _message;
    private string _sentence;
    [SerializeField] UnityEvent _onTyperEnd;
    
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
        _onTyperEnd.Invoke();
    }
}

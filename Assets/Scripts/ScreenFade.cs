using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class ScreenFade : MonoBehaviour
{
    private bool _playAnim = false;
    [SerializeField] private float _animDuration = 1;
    [SerializeField] private float _fadeSpeed = 7.5f;
    private float _animTimer = 0.0f;
    private CanvasGroup _blackScreen;

    private bool _eventsExecuted = false;
    [SerializeField] private List<UnityEvent> _midFadeEvents;

    private void Start()
    {
        _blackScreen = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if (_playAnim)
        {
            //Debug.Log(Time.timeScale);

            if (_animTimer < _animDuration * 0.5f)
            {
                _blackScreen.alpha += _fadeSpeed * Time.deltaTime;
            }
            else
            {
                _blackScreen.alpha -= _fadeSpeed * Time.deltaTime;
            }

            if (_blackScreen.alpha == 1 && !_eventsExecuted)
            {
                foreach (UnityEvent callback in _midFadeEvents)
                {
                    callback.Invoke();
                }
                _eventsExecuted = true;
            }
            
            _animTimer += Time.deltaTime;
         
            if (_animTimer >= _animDuration)
            {
                _playAnim = false;
                _eventsExecuted = false;
                _animTimer = 0;
                _blackScreen.alpha = 0;
            }
        }
    }
    
    public void PlayFade()
    {
        _playAnim = true;
    }
}

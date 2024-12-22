using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class ScreenFade : MonoBehaviour
{
    private bool _playAnim = false;
    [SerializeField] private float _animDuration = 5;
    [SerializeField] private float _fadeSpeed = 5;
    private float _animTimer = 0.0f;
    private CanvasGroup _blackScreen;

    private bool _eventsExecuted;
    [SerializeField] private List<UnityEvent> _midFadeEvents;

    private void Start()
    {
        _blackScreen = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if (_playAnim)
        {
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
            }
            
            _animTimer += Time.deltaTime;
        }
        if (_animTimer >= _animDuration)
        {
            _playAnim = false;
            _animTimer = 0;
            _blackScreen.alpha = 0;
        }
    }
    
    public void PlayFade()
    {
        _playAnim = true;
    }
}

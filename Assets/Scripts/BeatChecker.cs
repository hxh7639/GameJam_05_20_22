using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class BeatChecker : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip = null;
    [SerializeField] private AudioSource _audioSource = null;

    [Header("Settings")]    
    [SerializeField] private float _beatsPerMin = 140;    
    [SerializeField] private Image _tempImage;
    [SerializeField] private int _beatScale = 4;
    [SerializeField] private double _hitGraceTimer = 0.3f;
    [SerializeField] private float _first4thBeatTimer = 0;

    [Header("info only")] 
    [SerializeField] private float _secPerBeat = 0;
    [SerializeField] private double _graceTimer;
    [SerializeField] private double _currentTime;
    [SerializeField] private double _songStartedTime = 0;
    [SerializeField] private double _currentSongPosition = 0;
    [SerializeField] private bool _isBeatStarted = false;
    [SerializeField] private double _lastBeatTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource.clip = _audioClip;
        _secPerBeat = 60 / _beatsPerMin;
        _secPerBeat = _secPerBeat * _beatScale;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey)
        {
            StartGame();
        }
        if(!_audioSource.isPlaying){return;}

        TempCountBeat3();

    }

    private void StartGame()
    {
        _audioSource.Play();
        _songStartedTime = Time.timeAsDouble;
    }

    private void TempCountBeat3()
    {
        _currentTime = Time.timeAsDouble;

        _currentSongPosition = _currentTime - _songStartedTime;

        if (!_isBeatStarted && (_first4thBeatTimer) <= _currentSongPosition)
        {
            Debug.Log("beat counted, 1st beat");
            _lastBeatTimer = Time.timeAsDouble;
            _isBeatStarted = true;
        }

        if(!_isBeatStarted)
        {
            return;
        }

        if(_currentTime > _graceTimer) 
        {
            _tempImage.color = Color.black;
        }


        if( (_currentTime - _lastBeatTimer) > (_secPerBeat))
        {
            Debug.Log("record new beat");
            _tempImage.color = Color.green;
            _lastBeatTimer = Time.timeAsDouble;
            _graceTimer = Time.timeAsDouble + _hitGraceTimer;
        }



    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class BeatChecker : MonoBehaviour
{
    [SerializeField] private Songs_SO _songs_SO = null;
    [SerializeField] private AudioSource _audioSource = null;
    [SerializeField] private ArrowCreator _arrowCreator = null;

    [Header("Settings")]    
    [SerializeField] private float _beatsPerMin = 140;    
    [SerializeField] private Image _tempImage;
    [SerializeField] private double _hitGraceTimer = 0.3f;
    [SerializeField] private float _first4thBeatTimer = 0;

    [Header("info only")] 
    [SerializeField] private float _secPerBeatScaled = 0;
    [SerializeField] private double _graceTimer;
    [SerializeField] private double _currentTime;
    [SerializeField] private double _songStartedTime = 0;
    [SerializeField] private double _currentSongPosition = 0;
    [SerializeField] private bool _isBeatStarted = false;
    [SerializeField] private double _lastBeatTimer = 0;
    [SerializeField] private int _beatIndex = 0;
    
    //public
    public double _nextBeatCheckTimer = 0;

    // Start is called before the first frame update
    void Start()
    {    

    } 

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Return))
        {
            StartGame();
        }
        if(!_audioSource.isPlaying){return;}

        TempCountBeat3();

    }

    private void StartGame()
    {
        _audioSource.clip = _songs_SO._audioClip;
        _beatsPerMin = _songs_SO._beatsPerMin;
        _first4thBeatTimer = _songs_SO._first4thBeatTimer;

        _secPerBeatScaled = _songs_SO._secPerBeatScaled;

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
            _tempImage.color = Color.green;            
            _lastBeatTimer = Time.timeAsDouble;
            _graceTimer = Time.timeAsDouble + _hitGraceTimer;
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


        if( (_currentTime - _lastBeatTimer) > (_secPerBeatScaled))
        {            
            if(_beatIndex == 4){
                _tempImage.color = Color.green;
                }
            else{
                _tempImage.color = Color.yellow;}
            if(_beatIndex == 1){
                //record the next beat checker
                //_secPerBeatScaled not actually scaled since i am now using a scale of 1
                _nextBeatCheckTimer = Time.timeAsDouble + (_secPerBeatScaled * 3);
                LeanTween.scale(_arrowCreator._stampParentPanel, new Vector3(0,0,0), 0.5f);
            }
            _beatIndex ++;
            if(_beatIndex > 4) {_beatIndex = 1;}
            _lastBeatTimer = Time.timeAsDouble;
            _graceTimer = Time.timeAsDouble + _hitGraceTimer;
        }
    }

}

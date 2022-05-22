using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class BeatChecker : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip = null;
    [SerializeField] private AudioSource _audioSource = null;
    [SerializeField] private float _time;
    [SerializeField] private float _beatTimer;

    [SerializeField] private float _firstBeatTimer = 0;
    [SerializeField] private float _beatsPerMin = 140;
    [SerializeField] private float _secPerBeat = 0;
    [SerializeField] private Image _tempImage;
    [SerializeField] private float _hitGraceTimer = 0.3f;
                     private float _scriptHitGraceTimer = 0.1f;
    [SerializeField] private bool _beatStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource.clip = _audioClip;
        _secPerBeat = 60 / _beatsPerMin;
        _secPerBeat = _secPerBeat * 4;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey)
        {
            StartGame();
        }
        if(!_audioSource.isPlaying){return;}
        if(!_beatStarted)
        {
            _firstBeatTimer -=Time.deltaTime;

            if(_firstBeatTimer <= 0)
            {
                _beatStarted = true;
                _beatTimer = -1;
                TempCountBeat();
            }
            return;
        }

        if(_scriptHitGraceTimer < 0 )
        {
            _tempImage.color = Color.black;
        }


        _beatTimer -= Time.deltaTime;  
        _scriptHitGraceTimer -= Time.deltaTime;
        

        if(_beatTimer < 0)
        {
            TempCountBeat();
        }
        



    }

    private void StartGame()
    {
        _audioSource.Play();
        _beatTimer = _secPerBeat;
    }

    private void TempCountBeat()
    {
        Debug.Log("beat counted");
        _beatTimer = _secPerBeat;
        _tempImage.color = Color.green;
        _scriptHitGraceTimer = _hitGraceTimer;
    }
}

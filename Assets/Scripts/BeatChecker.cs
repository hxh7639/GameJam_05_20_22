using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BeatChecker : MonoBehaviour
{
    [SerializeField] private Songs_SO _songs_SO = null;
    [SerializeField] private AudioSource _audioSource = null;
    [SerializeField] private ArrowCreator _arrowCreator = null;
    [SerializeField] private ArrowChecker _arrowChecker = null;
    [SerializeField] private PersistGameManager _persistGameManager = null;


    [Header("Settings")]    
    [SerializeField] private float _beatsPerMin = 140;    
    [SerializeField] private Image _tempImage;
    [SerializeField] private double _hitGraceTimer = 0.3f;
    [SerializeField] private float _first4thBeatTimer = 0;
    [SerializeField] private float _gameStartAfterSeconds = 0;
    [SerializeField] private float _songLastBeatTimer;
    [SerializeField] private float _endOfTheSongTime;
    

    [Header("Other")]
    [SerializeField] private GameObject _startTextPanel;
    [SerializeField] private TMP_Text _startText;
    [SerializeField] private bool _isInPutEnabled = false;
                     private float _gameStartTimer = 0;
    [SerializeField] private bool _isLoadingNextScene = false; 


    [Header("Beat Correction")]
    [SerializeField] private bool _isCorrectionNeeded = false;   
    [SerializeField] private bool _isBeatCorrected = false;  
    [SerializeField] private float _timeSlightlyAfterCorrectionBeat;
    [SerializeField] private float _timeOfNextBeat;
    [SerializeField] private float _timeOfCorrectionBeat;
                

    [Header("info only")] 
    [SerializeField] private float _secPerBeatScaled = 0;
    [SerializeField] private double _graceTimer;
    [SerializeField] private double _currentTime;
    [SerializeField] private double _songStartedTime = 0;
    [SerializeField] private double _currentSongPosition = 0;
    [SerializeField] private bool _isBeatStarted = false;
    [SerializeField] private bool _isMusicStarted = false;
    [SerializeField] private double _lastBeatTimer = 0;
    [SerializeField] private int _beatIndex = 0;
    
    //public
    public double _nextBeatCheckTimer = 0;
    public bool _isSpacePressedBeforeForthBeat = false;

    void Awake()
    {
        _persistGameManager = FindObjectOfType<PersistGameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {    
        DisableInputs();
        _persistGameManager.FadeIn(1);
        _isLoadingNextScene = false;
        
        _songs_SO = _persistGameManager._availableSongs[_persistGameManager._currentSongIndex];
        _songLastBeatTimer = _songs_SO._songLastBeatTimer;
        _endOfTheSongTime = _songs_SO._endOfTheSongTime;

        
        _gameStartTimer = 0.5f;
        _isMusicStarted = false;
    } 

    // Update is called once per frame
    void Update()
    {
        if(!_isMusicStarted)
        {
            _gameStartTimer += Time.deltaTime;
            if(_gameStartTimer > _gameStartAfterSeconds)
            {
                StartGame();
                _isMusicStarted = true;
            }
        }
        /* if(Input.GetKey(KeyCode.Return))
        {
            StartGame();
        } */
        //if(!_audioSource.isPlaying){return;}

        TempCountBeat3();

    }

    private void StartGame()
    {
        _audioSource.clip = _songs_SO._audioClip;
        _beatsPerMin = _songs_SO._beatsPerMin;
        _first4thBeatTimer = _songs_SO._first4thBeatTimer;

        _secPerBeatScaled = _songs_SO._secPerBeatScaled;
        /* _isCorrectionNeeded = _songs_SO._isCorrectionNeeded;
        _timeSlightlyAfterCorrectionBeat = _songs_SO._timeSlightlyAfterCorrectionBeat;
        _timeOfNextBeat = _songs_SO._timeOfNextBeat;
        _timeOfCorrectionBeat = _songs_SO._timeOfCorrectionBeat; */



        _audioSource.Play();
        _songStartedTime = Time.timeAsDouble;
    }

    private void TempCountBeat3()
    {
        if(_isLoadingNextScene){return;}

        _currentTime = Time.timeAsDouble;

        _currentSongPosition = _currentTime - _songStartedTime;

        //end of song actions
        if(_currentSongPosition > _endOfTheSongTime)
        {
            

            _persistGameManager._currentSongIndex ++;
            Debug.Log("_persistGameManager._currentSongIndex: " + _persistGameManager._currentSongIndex);
            if(_persistGameManager._currentSongIndex <= 1)
            {
                Debug.Log("load StoryScene ");
                _isLoadingNextScene = true;
                LoadScene("StoryScene");
                return;
            }
            if(_persistGameManager._currentSongIndex >= _persistGameManager._availableSongs.Count - 1)
            {
                Debug.Log("load Menu ");
                _isLoadingNextScene = true;
                LoadScene("Menu");
            }else{
                Debug.Log("load GameScene ");
                _isLoadingNextScene = true;
                LoadScene("GameScene");
            }
            

        }
        if(_currentSongPosition > _songLastBeatTimer){return;}

        //beat correction
        if(_isCorrectionNeeded && !_isBeatCorrected && 
        _currentSongPosition > _timeSlightlyAfterCorrectionBeat  && _currentSongPosition < _timeOfNextBeat)
        {
            _lastBeatTimer = _timeOfCorrectionBeat;
            _isBeatCorrected = true;
        }

        if (!_isBeatStarted && (_first4thBeatTimer) <= _currentSongPosition)
        {
            //_tempImage.color = Color.green;            
            _lastBeatTimer = Time.timeAsDouble;
            _graceTimer = Time.timeAsDouble + _hitGraceTimer;
            _isBeatStarted = true;
            _startTextPanel.SetActive(true);
            LeanTween.scale(_startTextPanel, new Vector3(0,0,0), 0.5f);
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
            if(_beatIndex == 1){
                //ready Text on the first 2 music blocks
                if (!_isInPutEnabled){
                    //reset panel scale
                    LeanTween.scale(_startTextPanel, new Vector3(1,1,1), 0f);
                    _startText.text = "3";
                    LeanTween.scale(_startTextPanel, new Vector3(0f,0f,0f), 0.5f);
                }

                //record the next beat checker
                //_secPerBeatScaled not actually scaled since i am now using a scale of 1
                _nextBeatCheckTimer = Time.timeAsDouble + (_secPerBeatScaled * 3);
                LeanTween.scale(_arrowCreator._stampParentPanel, new Vector3(0,0,0), 0.5f);
            }

            if(_beatIndex == 2){
                //ready Text on the first 2 music blocks
                if (!_isInPutEnabled){
                    //reset panel scale
                    LeanTween.scale(_startTextPanel, new Vector3(1,1,1), 0f);
                    _startText.text = "2";
                    LeanTween.scale(_startTextPanel, new Vector3(0f,0f,0f), 0.5f);
                }
            }

            if(_beatIndex == 3){
                //ready Text on the first 2 music blocks
                if (!_isInPutEnabled){
                    //reset panel scale
                    LeanTween.scale(_startTextPanel, new Vector3(1,1,1), 0f);
                    _startText.text = "1";
                    LeanTween.scale(_startTextPanel, new Vector3(0f,0f,0f), 0.5f);
                }
            }

            if(_beatIndex == 4){
                //ready Text on the first 2 music blocks
                if (!_isInPutEnabled){
                    //reset panel scale
                    LeanTween.scale(_startTextPanel, new Vector3(1,1,1), 0f);
                    _startText.text = "Start!";
                    LeanTween.scale(_startTextPanel, new Vector3(0f,0f,0f), 0.5f);
                    EnableInputs();
                }


                _tempImage.color = Color.green;
                //if space is pressed before the forth beat, that'll be the determination, so skip this
                if(_isSpacePressedBeforeForthBeat) //hit but before the beat
                {
                    //do not kick in time's up
                    //reset for next loop
                    _isSpacePressedBeforeForthBeat = false;
                } else 
                {
                    //kick in time's up, to be checked by arrowCreator
                    _arrowCreator._isTimeUpForCurrentBeat = true;                    
                }                
            }
            else{
                _tempImage.color = Color.yellow;}





            _beatIndex ++;
            if(_beatIndex > 4) {_beatIndex = 1;}
            _lastBeatTimer = Time.timeAsDouble;
            _graceTimer = Time.timeAsDouble + _hitGraceTimer;
        }
    }

    private void DisableInputs()
    {
        _arrowCreator._isInPutEnabled = false;
        _arrowChecker._isInPutEnabled = false;
        _isInPutEnabled = false;
    }

    private void EnableInputs()
    {
        _arrowCreator._isInPutEnabled = true;
        _arrowChecker._isInPutEnabled = true;
        _isInPutEnabled = true;
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneIE(sceneName));
    }

    public IEnumerator LoadSceneIE(string sceneName)
    {        
        _persistGameManager.FadeOut(2);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(sceneName.ToString());
    }

}

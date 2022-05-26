using UnityEngine;


[CreateAssetMenu(fileName = "Song_", menuName = "Game/Add New Song", order = 0)]
public class Songs_SO : ScriptableObject
{
    [Header("setup")] 
    public AudioClip _audioClip = null;
    public float _beatsPerMin = 100;  
    public float _firstBeatTimer = 0;  

    [Header("Beat Correction")] 
    [Tooltip("if true, you need to fill out the rest")]
    public bool _isCorrectionNeeded = false;
    public float _timeSlightlyAfterCorrectionBeat = 0;
    public float _timeOfNextBeat = 0;
    public float _timeOfCorrectionBeat = 0;



    [Header("auto")] 
    public float _first4thBeatTimer = 0;
    public int _beatScale = 1;
    public float _secPerBeatScaled;
    public float _secPerBeat = 0;

    public void OnValidate()
    {
         _secPerBeat = 60 / _beatsPerMin;
        _secPerBeatScaled = _secPerBeat * _beatScale;

        _first4thBeatTimer = _firstBeatTimer + (_secPerBeat * 3);
    }
}

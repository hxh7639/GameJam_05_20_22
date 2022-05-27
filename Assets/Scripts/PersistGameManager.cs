using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PersistGameManager : MonoBehaviour
{
    public List<Songs_SO> _availableSongs = new List<Songs_SO>();
    
    public int _currentSongIndex = 0;

    public int _currentScore = 0;
    public int _combo = 0;
    public bool _firstTimeHit = true;

    public TMP_Text _comboText;
    public TMP_Text _ScoreText;

    public int _basePointPerClear = 500;
    public int _pointPerCombo = 100;
    public int _pointPerPerfectHit = 250;


    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void AddScore(bool isPerfectHit)
    {
        _currentScore += _basePointPerClear + (_combo * _pointPerCombo) + _pointPerPerfectHit;
        _ScoreText.text = _currentScore.ToString();
    }

    public void ResetScore()
    {
        _currentScore = 0;
        _ScoreText.text = _currentScore.ToString();
    }

    public void AddCombo()
    {   
        //do not add combo point if first time hit
        if(_firstTimeHit)
        {
            _firstTimeHit = false;
            return;
        }
        _combo++;
        _comboText.text = _combo.ToString();
    }

    public void ResetCombo()
    {
        _combo = 0;
        _comboText.text = _combo.ToString();
        _firstTimeHit = true;
    }

    public void RegisterComboText(TMP_Text comboText)
    {
        _comboText = comboText;
        Debug.Log("RegisterComboText");
    }

    public void RegisterScoreText(TMP_Text scoreText)
    {
        _ScoreText = scoreText;
        Debug.Log("RegisterScoreText");
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowCreator : MonoBehaviour
{
    [SerializeField] private BeatChecker _beatChecker;
    //0 up, 1 down, 2 left, 3 right
    public List<int> _currentListOfArrows;
    [SerializeField] private List<int> _nextListOfArrows;
    public int _currentArrowPos;
    [SerializeField] private int _currentLevel;
    [SerializeField] private int _nextLevel;
    [SerializeField] private int _arrowsToCreate;
    public List<Image> _lineOfArrowsImagesCurrent = new List<Image>();
    [SerializeField] private List<Image> _lineOfArrowsImagesNext = new List<Image>();
    [SerializeField] private List<Sprite> _arrowSprites = new List<Sprite>();
    [SerializeField] private bool _isTheFirstLine = true;
    [SerializeField] private float _beatCheckGraceTime = 0.1f;
    [SerializeField] private float _perfectHitRange = 0.05f;
    public bool _isTimeUpForCurrentBeat = false;
    public bool _isInPutEnabled = false;

    [Header("stamps")] 
    public GameObject _stampParentPanel;
    [SerializeField] private GameObject _perfectBorder;
    [SerializeField] private GameObject _goodBorder;
    [SerializeField] private GameObject _missedBorder;
    [SerializeField] private List<GameObject> _perfectStamps;
    [SerializeField] private List<GameObject> _goodStamps;
    [SerializeField] private List<GameObject> _badStamps;
    

    private int _startLevelForOneArrow = 1;
    private int _startLevelForTwoArrows = 3;
    private int _startLevelForThreeArrows = 5;
    private int _startLevelForFiveArrows = 9;
    private int _startLevelForSevenArrows = 17;

    public void CreateLine()
    {       
        if (_isTheFirstLine)
        {
            NumberOfArrowsToCreateNextLine(_currentLevel);
            GenerateCurrentLine();    

            _currentLevel++;      
            GenerateNextLine();
            _currentLevel--;
            _isTheFirstLine = false;
            return;
        } 
        //have to clone it, otherwise it will continue to reference the list when updated
        _currentListOfArrows = new List<int> (_nextListOfArrows);   
        UpdateCurrentLine();
        GenerateNextLine();      
    }

    void Update()
    {
        if(!_isInPutEnabled)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            
            //check if arrows have been cleared (hit correctly)
            //if all clear
            if (_currentArrowPos >= _currentListOfArrows.Count)
            {
                CheckBeatTimer();

            }
            else
            {
                Debug.Log("not all arrows are cleared");
                HandleNotAllArrowsAreCleared();
            }

            ResetCurrentLine();
            _isTimeUpForCurrentBeat = false;          
        }

        if(!_isTimeUpForCurrentBeat)
        { return;}
        
        if(_beatChecker._nextBeatCheckTimer + _beatCheckGraceTime + 0.05 < Time.timeAsDouble)
        {
            if(_currentArrowPos > 0) // do not need to reset if player didn't even start
            {
                HandleNotAllArrowsAreCleared();
                ResetCurrentLine();
                _isTimeUpForCurrentBeat = false;
            }            
        }

        //TODO handle line not finished but did not click space key 
    }

    

    private void CheckBeatTimer()
    {
        //check if space was hit on time
        //hit before the Check Timer
        if ((Time.timeAsDouble - _beatChecker._nextBeatCheckTimer) < 0 &&
            (Time.timeAsDouble - _beatChecker._nextBeatCheckTimer) > -_beatCheckGraceTime
            ||
            //(hit after the check timer)
            (Time.timeAsDouble - _beatChecker._nextBeatCheckTimer) > 0 &&
            (Time.timeAsDouble - _beatChecker._nextBeatCheckTimer) < _beatCheckGraceTime)

        {         
            if((Time.timeAsDouble - _beatChecker._nextBeatCheckTimer) < 0)    
            {
                //let beat checker know so it doesn't change _isTimeUpForCurrentBeat status
                //its a hit but it is before the beat
                _beatChecker._isSpacePressedBeforeForthBeat = true;
            }  
            float tempFloat = (float)(_beatChecker._nextBeatCheckTimer - Time.timeAsDouble);
            //test for perfect or good     
            if(Mathf.Abs(tempFloat) < _perfectHitRange)
            {
                HandlePerfectHit();
            } else
            {
                HandleGoodHit();
            }


            //next line/level
            CreateLine();
            _currentLevel++;
        }
        else
        {
            //missing the beat
            HandleMissingBeat();

        }
    }



    private void GenerateCurrentLine()
    {
        _currentListOfArrows.Clear();
        while (_currentListOfArrows.Count < _arrowsToCreate)
        {
            _currentListOfArrows.Add((int)Random.Range(0, 3));
        }

        UpdateCurrentLine();

    }

    private void UpdateCurrentLine()
    {
        //clear out the previous sprites
        foreach (Image img in _lineOfArrowsImagesCurrent)
        {
            img.sprite = null;
        }
        //set available sprites
        for (int i = 0; i < _currentListOfArrows.Count; i++)
        {
            _lineOfArrowsImagesCurrent[i].sprite = _arrowSprites[_currentListOfArrows[i]];
        }
        //disable the additional images
        foreach (Image image in _lineOfArrowsImagesCurrent)
        {
            image.transform.parent.gameObject.SetActive(image.sprite == null ? false : true);  
            if(_isTheFirstLine){return;}          
            LeanTween.moveLocalY(image.transform.parent.gameObject, 250, 0);
            LeanTween.moveLocalY(image.transform.parent.gameObject, 0, 0.1f);   
        }    

    }

    private void GenerateNextLine()
    {
        _nextListOfArrows.Clear();

        NumberOfArrowsToCreateNextLine(_currentLevel + 1);

        while (_nextListOfArrows.Count < _arrowsToCreate)
        {
            _nextListOfArrows.Add((int)Random.Range(0, 3));
        }        

        for (int i = 0; i < _nextListOfArrows.Count; i++)
        {
            _lineOfArrowsImagesNext[i].sprite = _arrowSprites[_nextListOfArrows[i]];
        }

        UpdateNextLine();

    }


    

    private void UpdateNextLine()
    {
        //clear out the previous sprites
        foreach (Image img in _lineOfArrowsImagesNext)
        {
            img.sprite = null;
        }
        //set available sprites
        for (int i = 0; i < _nextListOfArrows.Count; i++)
        {
            _lineOfArrowsImagesNext[i].sprite = _arrowSprites[_nextListOfArrows[i]];
        }
        //disable the additional images
        foreach (Image image in _lineOfArrowsImagesNext)
        {            
            image.transform.parent.gameObject.SetActive(image.sprite == null ? false : true);
            if(_isTheFirstLine){return;}
            LeanTween.moveLocalY(image.transform.parent.gameObject, 1000, 0);
            LeanTween.moveLocalY(image.transform.parent.gameObject, 0, Random.Range(.1f,.4f));
        }
    }

    private void NumberOfArrowsToCreateNextLine(int currentLevel)
    {
        if (currentLevel <= _startLevelForOneArrow)
        {
            _arrowsToCreate = 1;
        }
        else if (currentLevel >= _startLevelForTwoArrows && currentLevel < _startLevelForThreeArrows)
        {
            _arrowsToCreate = 2;
        }
        else if (currentLevel >= _startLevelForThreeArrows && currentLevel < _startLevelForFiveArrows)
        {
            _arrowsToCreate = 3;
        }
        else if (currentLevel >= _startLevelForFiveArrows && currentLevel < _startLevelForSevenArrows)
        {
            _arrowsToCreate = 5;
        }
        else if (currentLevel >= _startLevelForSevenArrows)
        {
            _arrowsToCreate = 7;
        }
    }

    private void ResetStamps()
    {
        _stampParentPanel.transform.localRotation = Quaternion.identity;
        _stampParentPanel.transform.localScale = new Vector3(1,1,1);
        _perfectBorder.SetActive(false);
        _goodBorder.SetActive(false);
        _missedBorder.SetActive(false);
        foreach (GameObject stamp in _perfectStamps)
        {
            stamp.SetActive(false);
        }
        foreach (GameObject stamp in _goodStamps)
        {
            stamp.SetActive(false);
        }
        foreach (GameObject stamp in _badStamps)
        {
            stamp.SetActive(false);
        }
    }

    private void HandleMissingBeat()
    {
        ResetStamps();
        LeanTween.rotateLocal(_stampParentPanel, new Vector3 (0,0,Random.Range(-45,45)), 0f);
        _missedBorder.SetActive(true);
        _badStamps[Random.Range(0,_badStamps.Count)].SetActive(true);        
    }

    private void HandlePerfectHit()
    {
        ResetStamps();
        LeanTween.rotateLocal(_stampParentPanel, new Vector3 (0,0,Random.Range(-45,45)), 0f);
        _perfectBorder.SetActive(true);
        _perfectStamps[Random.Range(0,_perfectStamps.Count)].SetActive(true);   
    }

    private void HandleGoodHit()
    {
        ResetStamps();
        LeanTween.rotateLocal(_stampParentPanel, new Vector3 (0,0,Random.Range(-45,45)), 0f);
        _goodBorder.SetActive(true);
        _goodStamps[Random.Range(0,_goodStamps.Count)].SetActive(true);   
    }

    public void HandleNotAllArrowsAreCleared()
    {
        foreach (Image img in _lineOfArrowsImagesCurrent)
        {
            LeanTween.rotateLocal(img.transform.parent.gameObject,new Vector3(Random.Range(-90,90),Random.Range(-90,90),Random.Range(-90,90)),0f);
            LeanTween.rotateLocal(img.transform.parent.gameObject,new Vector3(0,0,0), .3f);
        }
    }

    public void ResetCurrentLine()
    {
        //reset current line
        foreach (Image img in _lineOfArrowsImagesCurrent)
        {
            img.color = Color.white;
        }
        _currentArrowPos = 0;
    }


    void Start()
    {
        CreateLine();
    }

}

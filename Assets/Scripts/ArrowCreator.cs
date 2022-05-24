using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowCreator : MonoBehaviour
{
    //0 up, 1 down, 2 left, 3 right
    [SerializeField] private List<int> _currentListOfArrows;
    [SerializeField] private List<int> _nextListOfArrows;
    [SerializeField] private int _currentArrow;
    [SerializeField] private int _currentLevel;
    [SerializeField] private int _nextLevel;
    [SerializeField] private int _arrowsToCreate;
    [SerializeField] private List<Image> _lineOfArrowsImagesCurrent = new List<Image>();
    [SerializeField] private List<Image> _lineOfArrowsImagesNext = new List<Image>();
    [SerializeField] private List<Sprite> _arrowSprites = new List<Sprite>();
    [SerializeField] private bool _isTheFirstLine = true;
    [SerializeField] private GameObject _currentBeatsParent = null;
    [SerializeField] private GameObject _nextBeatsParent = null;

    //temp
    // The time at which the animation started.
    private float startTime;

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
        }
        
        _currentListOfArrows = _nextListOfArrows;
        UpdateCurrentLine();
        GenerateNextLine();        

    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            CreateLine();
            _currentLevel++;
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
        }

        /* _currentBeatsParent.transform.localPosition = new Vector3(-400, 250, 0);
        _currentBeatsParent.transform. = 
            Vector3.Slerp(_currentBeatsParent.transform.localPosition, new Vector3(-400, 1000, 0), (Time.time - startTime / 5f));
     */
    }

    private void GenerateNextLine()
    {        
        _nextListOfArrows.Clear();
        NumberOfArrowsToCreateNextLine(_currentLevel+1);
        while (_nextListOfArrows.Count < _arrowsToCreate)
        {
            _nextListOfArrows.Add((int)Random.Range(0, 3));
        }        

        for(int i = 0; i < _nextListOfArrows.Count; i++)
        {
            _lineOfArrowsImagesNext[i].sprite = _arrowSprites[_nextListOfArrows[i]];
        }

        //clear out the previous sprites
        foreach (Image img in _lineOfArrowsImagesNext)
        {
            img.sprite = null;
        }
        //set available sprites
        for(int i = 0; i < _nextListOfArrows.Count; i++)
        {
            _lineOfArrowsImagesNext[i].sprite = _arrowSprites[_nextListOfArrows[i]];
        }
        //disable the additional images
        foreach(Image image in _lineOfArrowsImagesNext)
        {
            image.transform.parent.gameObject.SetActive(image.sprite == null ? false : true);
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
            Debug.Log("this ran");
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

    void Start()
    {
        CreateLine();
        startTime = Time.time;
    }

}

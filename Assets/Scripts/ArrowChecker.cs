using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowChecker : MonoBehaviour
{
    [SerializeField] private ArrowCreator _arrowCreator = null;
    public bool _isInPutEnabled = false;

    //0 up, 1 down, 2 left, 3 right
    void Update()
    {
        if(!_isInPutEnabled)
        {
            return;
        }


        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(_arrowCreator._currentListOfArrows[_arrowCreator._currentArrowPos] == 0)
            {
                _arrowCreator._lineOfArrowsImagesCurrent[_arrowCreator._currentArrowPos].color = Color.green;
                _arrowCreator._currentArrowPos++;
            } else
            {
                //when player make mistake
                //effect on the current arrow
                LeanTween.rotate(
                    _arrowCreator._lineOfArrowsImagesCurrent[_arrowCreator._currentArrowPos].transform.parent.gameObject,
                    new Vector3(Random.Range(-90,90),Random.Range(-90,90),Random.Range(-90,90)),
                    0f);
                LeanTween.rotate(
                    _arrowCreator._lineOfArrowsImagesCurrent[_arrowCreator._currentArrowPos].transform.parent.gameObject,
                    new Vector3(0,0,0),
                    .3f);

                //reset line
                foreach (Image img in _arrowCreator._lineOfArrowsImagesCurrent)
                {
                    img.color = Color.white;
                }
                _arrowCreator._currentArrowPos = 0;
            }
        }

        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(_arrowCreator._currentListOfArrows[_arrowCreator._currentArrowPos] == 1)
            {
                _arrowCreator._lineOfArrowsImagesCurrent[_arrowCreator._currentArrowPos].color = Color.green;
                _arrowCreator._currentArrowPos++;
            } else
            {
                //when player make mistake
                //effect on the current arrow
                LeanTween.rotate(
                    _arrowCreator._lineOfArrowsImagesCurrent[_arrowCreator._currentArrowPos].transform.parent.gameObject,
                    new Vector3(Random.Range(-90,90),Random.Range(-90,90),Random.Range(-90,90)),
                    0f);
                LeanTween.rotate(
                    _arrowCreator._lineOfArrowsImagesCurrent[_arrowCreator._currentArrowPos].transform.parent.gameObject,
                    new Vector3(0,0,0),
                    .3f);

                //reset line
                foreach (Image img in _arrowCreator._lineOfArrowsImagesCurrent)
                {
                    img.color = Color.white;
                }
                _arrowCreator._currentArrowPos = 0;
            }
        }

        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(_arrowCreator._currentListOfArrows[_arrowCreator._currentArrowPos] == 2)
            {
                _arrowCreator._lineOfArrowsImagesCurrent[_arrowCreator._currentArrowPos].color = Color.green;
                _arrowCreator._currentArrowPos++;
            } else
            {
                //when player make mistake
                //effect on the current arrow
                LeanTween.rotate(
                    _arrowCreator._lineOfArrowsImagesCurrent[_arrowCreator._currentArrowPos].transform.parent.gameObject,
                    new Vector3(Random.Range(-90,90),Random.Range(-90,90),Random.Range(-90,90)),
                    0f);
                LeanTween.rotate(
                    _arrowCreator._lineOfArrowsImagesCurrent[_arrowCreator._currentArrowPos].transform.parent.gameObject,
                    new Vector3(0,0,0),
                    .3f);

                //reset line
                foreach (Image img in _arrowCreator._lineOfArrowsImagesCurrent)
                {
                    img.color = Color.white;
                }
                _arrowCreator._currentArrowPos = 0;
            }
        }

        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(_arrowCreator._currentListOfArrows[_arrowCreator._currentArrowPos] == 3)
            {
                _arrowCreator._lineOfArrowsImagesCurrent[_arrowCreator._currentArrowPos].color = Color.green;
                _arrowCreator._currentArrowPos++;
            } else
            {
                //when player make mistake
                //effect on the current arrow
                LeanTween.rotate(
                    _arrowCreator._lineOfArrowsImagesCurrent[_arrowCreator._currentArrowPos].transform.parent.gameObject,
                    new Vector3(Random.Range(-90,90),Random.Range(-90,90),Random.Range(-90,90)),
                    0f);
                LeanTween.rotate(
                    _arrowCreator._lineOfArrowsImagesCurrent[_arrowCreator._currentArrowPos].transform.parent.gameObject,
                    new Vector3(0,0,0),
                    .3f);

                //reset line
                foreach (Image img in _arrowCreator._lineOfArrowsImagesCurrent)
                {
                    img.color = Color.white;
                }
                _arrowCreator._currentArrowPos = 0;
            }
        }

    }
}

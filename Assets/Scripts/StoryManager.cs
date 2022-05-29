using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    public MenuSoundManager _menuSoundManager;
    public PersistGameManager _persistGameManager;
    public int _convoIndex = 0;
    public GameObject _deathChatBubble;
    public GameObject _deathStoryObj;
    public TMP_Text _deathChatText;
    public TMP_Text _playerChatText;
    public List<string> _chatTexts = new List<string>();
    public List<string> _storyTexts = new List<string>();
    //story part 1
    public GameObject _jobStoryObj;
    public TMP_Text _jobStoryText;
    public GameObject _howToPlayObj;
    public int _jobConvoIndex = 0;

    //story part 2
    public int _storyPartTwoIndex = 0;
    public bool _isStoryPartTwoOpeningFinished = false;


    void Awake()
    {
        _menuSoundManager = FindObjectOfType<MenuSoundManager>();
        _persistGameManager = FindObjectOfType<PersistGameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _convoIndex = 0;
        if(_persistGameManager._currentSongIndex == 0)
        {
            _jobStoryObj.SetActive(true);
            _jobStoryText.text = _storyTexts[0];  
            _jobConvoIndex ++;      
            _persistGameManager.FadeIn(1);
        }
        if(_persistGameManager._currentSongIndex == 1)
        {
            _jobStoryText.text = _storyTexts[4]; 
            _jobStoryObj.SetActive(true);
            _persistGameManager.FadeIn(1);            
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        //story part 1
        if(_persistGameManager._currentSongIndex == 0)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if(_jobConvoIndex == 1)
                {
                    _jobStoryText.text = _storyTexts[1];  
                    _jobConvoIndex ++;      
                    _menuSoundManager.PlayStorySFX(5);       
                } 
                else if(_jobConvoIndex == 2)
                {
                    _jobStoryText.text = _storyTexts[2];  
                    _jobConvoIndex ++;      
                    _menuSoundManager.PlayStorySFX(5);       
                } 

                else if(_jobConvoIndex == 3)
                {
                    _jobStoryText.text = _storyTexts[3];  
                    _jobConvoIndex ++;      
                    //_menuSoundManager.PlayStorySFX(3);       
                } 

                else if(_jobConvoIndex == 4)
                {
                    _jobStoryObj.SetActive(false);
                    _howToPlayObj.SetActive(true);
                    _jobConvoIndex ++;  
                         
                } 

                else if(_jobConvoIndex == 5)
                {
                    _menuSoundManager.StopStorySFX();   
                    LoadScene("GameScene");     
                         
                } 

                
            }
            
        }

        //story part 2

        if(_persistGameManager._currentSongIndex == 1)
        {            
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if (!_isStoryPartTwoOpeningFinished)
                {
                    //part 2 opening
                    if(_storyPartTwoIndex < 1)
                    {
                        _jobStoryText.text = _storyTexts[5];
                        _menuSoundManager.PlayStorySFX(5); 
                        _storyPartTwoIndex++;
                    } 
                    else if(_storyPartTwoIndex == 1)
                    {
                        _jobStoryText.text = _storyTexts[6];
                        _menuSoundManager.PlayStorySFX(5); 
                        _storyPartTwoIndex++;
                    }
                    else if(_storyPartTwoIndex == 2)
                    {
                        _jobStoryObj.SetActive(false);
                        _isStoryPartTwoOpeningFinished = true;
                        _persistGameManager.FadeOut(1); 
                        StartCoroutine(_menuSoundManager.PlayStorySound());
                    }
                    return;
                }

                
                //odd#'s player, even's death
                /* if(_convoIndex < 1)
                {
                    _menuSoundManager.StopStorySFX();
                    _deathChatText.text = _chatTexts[0];
                    _deathStoryObj.SetActive(true);
                    _deathChatBubble.SetActive(true);   
                    _convoIndex ++;      
                    _menuSoundManager.PlayStorySFX(3);       
                } 
                else  */
                
                if(_convoIndex == 1)
                {
                    _menuSoundManager.StopStorySFX();
                    _playerChatText.text = _chatTexts[1];
                    _playerChatText.gameObject.SetActive(true);   
                    _convoIndex ++;       
                    _menuSoundManager.PlayStorySFX(4);     
                } 
                else if(_convoIndex == 2)
                {
                    _deathChatText.text = _chatTexts[2];
                    _convoIndex ++; 
                    _menuSoundManager.PlayStorySFX(3);                
                }
                else if(_convoIndex == 3)
                {
                    _playerChatText.text = _chatTexts[3];
                    _convoIndex ++;         
                    _menuSoundManager.StopStorySFX();      
                }
                else if(_convoIndex == 4)
                {
                    _deathChatText.text = _chatTexts[4];
                    _convoIndex ++;    
                    _menuSoundManager.PlayStorySFX(3);             
                }
                else if(_convoIndex == 5)
                {
                    _playerChatText.text = _chatTexts[5];
                    _convoIndex ++;     
                    _menuSoundManager.StopStorySFX();          
                }
                else if(_convoIndex == 6)
                {
                    _deathChatText.text = _chatTexts[6];
                    _convoIndex ++;      
                    _menuSoundManager.PlayStorySFX(3);           
                }
                else if(_convoIndex == 7)
                {
                    _playerChatText.text = _chatTexts[7];
                    _convoIndex ++;       
                    _menuSoundManager.StopStorySFX();        
                }
                else if(_convoIndex == 8)
                {
                    _deathChatText.text = _chatTexts[8];
                    _convoIndex ++;    
                    _menuSoundManager.PlayStorySFX(3);             
                }
                else if(_convoIndex == 9)
                {
                    _playerChatText.text = _chatTexts[9];
                    _convoIndex ++;   
                    _menuSoundManager.StopStorySFX();            
                }
                else if(_convoIndex == 10)
                {
                    _deathChatText.text = _chatTexts[10];
                    _convoIndex ++;       
                    _menuSoundManager.PlayStorySFX(3);          
                }
                else if(_convoIndex == 11)
                {
                    _playerChatText.text = _chatTexts[11];
                    _convoIndex ++;  
                    _menuSoundManager.StopStorySFX();             
                }
                else if(_convoIndex == 12)
                {
                    _deathChatText.text = _chatTexts[12];
                    _convoIndex ++;   
                    _menuSoundManager.PlayStorySFX(3);              
                }
                else if(_convoIndex == 13)
                {
                    _playerChatText.text = _chatTexts[13];
                    _convoIndex ++;     
                    _menuSoundManager.StopStorySFX();          
                }
                else if(_convoIndex == 14)
                {
                    _deathChatText.text = _chatTexts[14];
                    _convoIndex ++;      
                    _menuSoundManager.PlayStorySFX(3);           
                }
                else if(_convoIndex > 14)
                {
                    _menuSoundManager.StopStorySFX();   
                    LoadScene("GameScene");              
                }
            }
        }
        
    }

    public void DeathStoryStart()
    {
        _menuSoundManager.StopStorySFX();
        _deathChatText.text = _chatTexts[0];
        _deathStoryObj.SetActive(true);
        _deathChatBubble.SetActive(true);   
        _convoIndex ++;      
        _menuSoundManager.PlayStorySFX(3);   
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

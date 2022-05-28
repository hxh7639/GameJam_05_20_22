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
    public TMP_Text _deathChatText;
    public TMP_Text _playerChatText;
    public List<string> _chatTexts = new List<string>();

    void Awake()
    {
        _menuSoundManager = FindObjectOfType<MenuSoundManager>();
        _persistGameManager = FindObjectOfType<PersistGameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _convoIndex = 0;
        StartCoroutine(_menuSoundManager.PlayStorySound());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //odd#'s player, even's death
            if(_convoIndex < 1)
            {
                _menuSoundManager.StopStorySFX();
                _deathChatText.text = _chatTexts[0];
                _deathChatBubble.SetActive(true);   
                _convoIndex ++;      
                _menuSoundManager.PlayStorySFX(3);       
            } 
            else if(_convoIndex == 1)
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

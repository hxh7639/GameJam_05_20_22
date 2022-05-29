using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public List<Button> _menuSelections;
    public int _menuSelectionIndex;
    public GameObject _selectionPanel;
    public GameObject _controlsPanel;
    public GameObject _optionsPanel;
    public GameObject _creditsPanel;
    public GameObject _screenOverlay;
    public GameObject _volumeReminder;
    public MenuSoundManager _menuSoundManager;
    public PersistGameManager _persistGameManager;

    void Awake()
    {
        Application.targetFrameRate = 400;
    }

    // Start is called before the first frame update
    void Start()
    {
        _menuSelectionIndex = 0;
        _menuSelections[_menuSelectionIndex].Select();
        _persistGameManager.FadeIn(0);
    }

    // Update is called once per frame
    void Update()
    {


        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            _menuSelectionIndex --;
            if(_menuSelectionIndex < 0) {_menuSelectionIndex = 4;}
            _menuSoundManager.PlayMenuSound(0);
        }

        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            _menuSelectionIndex ++;
            if(_menuSelectionIndex > 4) {_menuSelectionIndex = 0;}
            _menuSoundManager.PlayMenuSound(0);
        }


        if(Input.GetKeyDown(KeyCode.Space))
        {
            _menuSoundManager.PlayMenuSound(1);
            _menuSelections[_menuSelectionIndex].onClick.Invoke();
            
        }

        if(Input.GetKey(KeyCode.Return))
        {
            _menuSoundManager.PlayMenuSound(1);
        }

        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
        {
            ResetMenu();
        }

        _menuSelections[_menuSelectionIndex].Select();
        

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

    public void ShowControls()
    {
        _selectionPanel.SetActive(false);
        _controlsPanel.SetActive(true);
        _volumeReminder.SetActive(false);
    }

    public void ShowOptions()
    {
        _selectionPanel.SetActive(false);
        _optionsPanel.SetActive(true);
        _screenOverlay.SetActive(false);
        _volumeReminder.SetActive(false);
        _menuSoundManager.PlayVolumeAdjustmentSong();
    }

    public void ShowCredits()
    {
        _selectionPanel.SetActive(false);
        _creditsPanel.SetActive(true);
        _volumeReminder.SetActive(false);
    }

    public void ResetMenu()
    {
        if (_selectionPanel.activeInHierarchy)
        {
            return;
        }
        
        _selectionPanel.SetActive(true);
        _controlsPanel.SetActive(false);
        _optionsPanel.SetActive(false);
        _creditsPanel.SetActive(false);

        _volumeReminder.SetActive(true);
        _screenOverlay.SetActive(true);
        _screenOverlay.GetComponent<Image>().CrossFadeAlpha(0, 1, false);

        _menuSelectionIndex = 0;
        _menuSelections[_menuSelectionIndex].Select();
        _menuSoundManager.PlayMenuSound(2);

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

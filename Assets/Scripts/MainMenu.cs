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
    public MenuSoundManager _menuSoundManager;

    void Awake()
    {
        Application.targetFrameRate = 60;
    }

    // Start is called before the first frame update
    void Start()
    {
        _menuSelectionIndex = 0;
        _menuSelections[_menuSelectionIndex].Select();
    }

    // Update is called once per frame
    void Update()
    {


        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            _menuSelectionIndex --;
            if(_menuSelectionIndex < 0) {_menuSelectionIndex = 4;}
            _menuSoundManager.PlaySound(0);

        }

        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            _menuSelectionIndex ++;
            if(_menuSelectionIndex > 4) {_menuSelectionIndex = 0;}
            _menuSoundManager.PlaySound(0);
        }


        if(Input.GetKeyDown(KeyCode.Space))
        {
            _menuSelections[_menuSelectionIndex].onClick.Invoke();
            _menuSoundManager.PlaySound(1);
        }

        if(Input.GetKey(KeyCode.Return))
        {
            _menuSoundManager.PlaySound(1);
        }

        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
        {
            ResetMenu();
        }

        _menuSelections[_menuSelectionIndex].Select();
        

    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName.ToString());
    }

    public void ShowControls()
    {
        _selectionPanel.SetActive(false);
        _controlsPanel.SetActive(true);
    }

    public void ShowOptions()
    {
        _selectionPanel.SetActive(false);
        _optionsPanel.SetActive(true);
    }

    public void ShowCredits()
    {
        _selectionPanel.SetActive(false);
        _creditsPanel.SetActive(true);
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

        _menuSelectionIndex = 0;
        _menuSelections[_menuSelectionIndex].Select();
        _menuSoundManager.PlaySound(2);

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

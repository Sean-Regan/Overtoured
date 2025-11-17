using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartMenu : MonoBehaviour
{
    [Header("UI Pages")]
    public GameObject mainMenu;
    public GameObject levelSelect;
    public GameObject controls;
    public GameObject about;

    [Header("Main Menu Buttons")]
    public Button startButton;
    public Button controlsButton;
    public Button aboutButton;
    public Button quitButton;

    public List<Button> returnButtons;

    // Start is called before the first frame update
    void Start()
    {
        EnableControls();

        //Hook events
        startButton.onClick.AddListener(EnableLevelSelect);
        controlsButton.onClick.AddListener(EnableControls);
        aboutButton.onClick.AddListener(EnableAbout);
        quitButton.onClick.AddListener(QuitGame);

        foreach (var item in returnButtons)
        {
            item.onClick.AddListener(EnableMainMenu);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void EnableLevelSelect()
    {
        mainMenu.SetActive(false);
        levelSelect.SetActive(true);
        controls.SetActive(false);
        about.SetActive(false);
    }

    public void HideAll()
    {
        mainMenu.SetActive(false);
        levelSelect.SetActive(false);
        controls.SetActive(false);
        about.SetActive(false);
    }

    public void EnableMainMenu()
    {
        mainMenu.SetActive(true);
        levelSelect.SetActive(false);
        controls.SetActive(false);
        about.SetActive(false);
    }
    public void EnableControls()
    {
        mainMenu.SetActive(false);
        levelSelect.SetActive(false);
        controls.SetActive(true);
        about.SetActive(false);
    }
    public void EnableAbout()
    {
        mainMenu.SetActive(false);
        levelSelect.SetActive(false);
        controls.SetActive(false);
        about.SetActive(true);
    }
}

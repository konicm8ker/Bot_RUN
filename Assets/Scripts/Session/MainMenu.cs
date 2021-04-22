using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class MainMenu : MonoBehaviour
{
    public enum State { Game, Menu }
    public State gameState = State.Game;
    [SerializeField] Transform mainMenu;
    [SerializeField] FirstPersonController fpsController;
    GameObject objectives;
    GameObject bgFill;
    bool showMenu = false;
    int tabIndex = 1;

    void Start()
    {
        objectives = mainMenu.GetChild(4).gameObject;
        bgFill = mainMenu.GetChild(0).gameObject;
    }

    void Update()
    {
        if(fpsController.isPaused || fpsController.gameOver){ return; }
 
        // Process input for showing/hiding main menu ui
        if(CrossPlatformInputManager.GetButtonDown("Menu"))
        {
            showMenu = !showMenu;
            if(showMenu)
            {
                gameState = State.Menu;
                ToggleMenu(true);
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
                gameState = State.Game;
                ToggleMenu(false);
            }
        }
        
        if(gameState == State.Game) { return; } // Don't cycle tabs when menu is closed

        // Handle tab switching input when menu is displayed
        if(CrossPlatformInputManager.GetButtonDown("Menu Left"))
        {
            tabIndex -= 1;
            if(tabIndex < 1){ tabIndex = 3; }
            ProcessMenuTabs();
        }
        else if(CrossPlatformInputManager.GetButtonDown("Menu Right"))
        {
            tabIndex += 1;
            if(tabIndex > 3){ tabIndex = 1; }
            ProcessMenuTabs();
        }

    }

    private void ToggleMenu(bool value)
    {
        mainMenu.gameObject.SetActive(value);
    }

    private void ProcessMenuTabs()
    {
        int childCount = 0;
        foreach(Transform tab in mainMenu)
        {
            if(childCount == tabIndex)
            {
                tab.gameObject.SetActive(true);
            }
            else
            {
                tab.gameObject.SetActive(false);
            }
            childCount += 1;
        }
        bgFill.SetActive(true); // Always keep bgFill active for different aspect ratios

        if(tabIndex == 1) // Only display objectives on story tab
        {
            objectives.SetActive(true);
        }
        else
        {
            objectives.SetActive(false);
        }

    }
}

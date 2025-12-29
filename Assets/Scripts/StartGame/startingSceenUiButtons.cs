using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingSceenUiButtons : MonoBehaviour
{
    public ButtonGroupSelector playerSelector;
    public ButtonGroupSelector epidemicSelector;

    public GameObject firstPanel;
    public GameObject secondPanel;

    public void startGame()
    {
        if (playerSelector.SelectedValue == -1 || epidemicSelector.SelectedValue == -1)
        {
            Debug.Log("Please select both options!");
            return;
        }

        Debug.Log("Players: " + playerSelector.SelectedValue);
        Debug.Log("Epidemics: " + epidemicSelector.SelectedValue);

        PlayerDeck.playerCount = playerSelector.SelectedValue;
        PlayerDeck.epidemicCardCount = epidemicSelector.SelectedValue;

        SceneManager.LoadScene("MainGameScene");
    }

    public void backButton()
    {
        secondPanel.SetActive(false);
        firstPanel.SetActive(true);
    }

    public void playButton()
    {
        firstPanel.SetActive(false);
        secondPanel.SetActive(true);
    }
    
    public void quitGameButton()
    {
        Application.Quit();
    }
}

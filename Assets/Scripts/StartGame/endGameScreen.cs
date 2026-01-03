using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    public void playAgainButton()
    {
        SceneManager.LoadScene("StartGameScene");
    }

    public void quitGameButton()
    {
        Application.Quit();
    }
}

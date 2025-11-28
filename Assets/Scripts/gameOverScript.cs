using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    public static GameOverScript Instance;

    public GameObject panel;
    public Text text;

    public void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public void checkIfGameWon()
    {
        foreach (DiseaseColor Disease in DiseaseMarkers.Instance.diseaseColorDict.Values)
        {
            if(!Disease.isCuredDisease)
            {
                return;
            }
        }

        panel.SetActive(true);
        text.text = "You won";
    }

    public void GameLost()
    {
        panel.SetActive(true);
        text.text = "You Lost";
        Mainscript.main.waitingForCityClick = true;
    }

}

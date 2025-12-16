using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEnd : MonoBehaviour
{
    public static GameEnd Instance;

    public GameObject panel;
    public Text text;

    public void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public void checkIfGameWon()
    {
        foreach (DiseaseColorMarker Disease in DiseaseMarkers.diseaseColorDict.Values)
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

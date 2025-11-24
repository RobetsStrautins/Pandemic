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
        foreach (DesiseColor Desise in DesiseMarkers.Instance.desiseColorDict.Values)
        {
            if(!Desise.isCuredDesise)
            {
                return;
            }
        }

        panel.SetActive(true);
        text.text = "You won";
    }

    public void checkIfGameLost()
    {
        if(Mainscript.main.OutBreakCount>=8)
        {
            panel.SetActive(true);
            text.text = "You Lost";
        }
    }

}

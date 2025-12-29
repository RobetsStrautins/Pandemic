using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance;

    [SerializeField] private Text moveCount;
    [SerializeField] private Text whatPlayerTurn;
    [SerializeField] private Text outBreakCountText;
    [SerializeField] private Text infectionRateCountText;

    public GameObject gameOverPanel;
    public Text gameEndText;
    void Awake()
    {
        Instance = this;
        gameOverPanel.SetActive(false);
    }

    public void Setup()
    {
        updateMoveCount();
        updateOutBreakCount(0);
        updateInfectionRateCount(2);
    }

    public void usedAction()
    {
        Mainscript.main.playerTurnCount --;
        updateMoveCount();
    }
    
    public void updateMoveCount()
    {
        whatPlayerTurn.text = "Player " + (Mainscript.main.getActivePlayer().playerId+1) + " turn";
        moveCount.text = Mainscript.main.playerTurnCount.ToString() + "/4";
    }

    public void updateOutBreakCount(int outBreakCount)
    {
        outBreakCountText.text = "Out Breaks: " + outBreakCount;
    }

    public void updateInfectionRateCount(int infectionRateCount)
    {
        infectionRateCountText.text = "Infection rate: " + infectionRateCount;
    }

    public void checkIfGameWon()
    {
        foreach (DiseaseColorProgress progress in DiseaseMarkers.Instance.getAllDiseaseProgress())
        {
            if (progress == DiseaseColorProgress.NotCured)
            {
                return;
            }
        }

        gameOverPanel.SetActive(true);
        gameEndText.text = "You won";
    }

    public void gameLost(string reason)
    {
        gameOverPanel.SetActive(true);
        gameEndText.text = "You Lost \n" + reason;
        Mainscript.main.waitingForCityClick = true;
    }
}

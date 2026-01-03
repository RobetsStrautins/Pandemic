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
    void Awake() //konstruktors
    {
        Instance = this;
        gameOverPanel.SetActive(false);
    }

    public void Setup() //inicializācija
    {
        updateMoveCount();
        updateOutBreakCount(0);
        updateInfectionRateCount(2);
    }

    public void usedAction() //izsauc kad spēlētajs veicis gājienu
    {
        Mainscript.main.playerTurnCount --;
        updateMoveCount();
    }
    
    public void updateMoveCount() //atjaunina gājiena skaitu
    {
        whatPlayerTurn.text = "Player " + (Mainscript.main.getActivePlayer().playerId+1) + " turn";
        moveCount.text = Mainscript.main.playerTurnCount.ToString() + "/4";
    }

    public void updateOutBreakCount(int outBreakCount) //atjaunina uzliesmojumu skaitu
    {
        outBreakCountText.text = "Uzliesmojumu skaits: " + outBreakCount;
    }

    public void updateInfectionRateCount(int infectionRateCount) //atjaunina infekcijas biežumu
    {
        infectionRateCountText.text = "Infekcijas biežums: " + infectionRateCount;
    }

    public void checkIfGameWon() //pārbauda vai spēle ir uzvarēta
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
        ActionLog.Instance.addEntry("Tu esi uz uzvarējis", Color.green);
    }

    public void gameLost(string reason) //izsauc kad spēle ir zaudēta
    {
        gameOverPanel.SetActive(true);
        gameEndText.text = "You Lost \n" + reason;
        Mainscript.main.waitingForCityClick = true;
        ActionLog.Instance.addEntry("Tu esi uz zaudejis, jo " + reason, Color.green);
    }
}

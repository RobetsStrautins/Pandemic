using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mainscript : MonoBehaviour
{
    public static Mainscript main;

    public CitySpawner citySpawner;

    public GameObject player;
    public GameObject playerTop;

    [SerializeField]
    private Text moveCount;
    [SerializeField]
    private Text whatPlayerTurn;
    [SerializeField]
    private Text outBreakCountText;
    [SerializeField]
    private Text infectionRateCountText;

    public int playerTurnCount;

    private int playerCount = 2;
    public List<Player> playersList = new List<Player>();
    private int currentPlayerIndex = 0;
    private Player activePlayer;

    public bool waitingForCityClick = false;
    public string waitingForCityClickAction;

    public List<CityData> researchStationOnMap = new();

    void Awake()
    {
        main = this;
    }

    void Start()
    {
        citySpawner.Setup();
        DiseaseDeck.Instance.Setup();
        PlayerDeck.Instance.SetupBeforeEpidemicCard();
        
        for (int i = 0; i < playerCount; i++)
        {
            GameObject spawnedPlayer = Instantiate(player);
            activePlayer = spawnedPlayer.GetComponent<Player>();
            activePlayer.playerColor = UnityEngine.Random.ColorHSV(0f, 1f, 0.8f, 1f, 0.8f, 1f);
            activePlayer.name = "Speletaijs" + (i + 1);
            activePlayer.playerId = i;
            playersList.Add(activePlayer);

            for (int j = 0; j < 6 - playerCount;j++)
            {
                PlayerCardSpawnerScript.Instance.givePlayerCard(activePlayer);
            }
    
            GameObject spawnedPlayerTop = Instantiate(playerTop);
            PlayerTop newPlayerTop = spawnedPlayerTop.GetComponent<PlayerTop>();
            newPlayerTop.Init(activePlayer);

        }

        //PlayerDeck.Instance.SetupAfterEpidemicCard();

        activePlayer = playersList[0];
        PlayerCardSpawnerScript.Instance.showPlayersHand(activePlayer);

        CityData startingCity = CitySpawner.cityMap[3];//sakuma pilseta ar majinu
        startingCity.buildResearchStation();

        playerTurnCount = 4;///vajag 4
        updateMoveCount();
        updateOutBreakCount(0);
        updateInfectionRateCount(2);
    }

    public Player getActivePlayer()
    {
        return activePlayer;
    }

    public bool playerTurnComplite()
    {
        if (playerTurnCount == 0)
        {
            //Console.WriteLine("Nav Vairs gaijieni");
            return true;
        }

        return false;
    }

    public void flytoCity(CityData tempCity)
    {
        activePlayer.moveToCity(tempCity);
        updateMoveCount();
    }

    public void activePlayerMoveCitys(CityData pressedCity )
    {
        if (waitingForCityClick)
        {
            switch (waitingForCityClickAction)
            {
                case "FlyTo":
                    activePlayer.moveToCity(pressedCity);
                    break;

                case "BuildStation":
                    playerTurnCount++;
                    pressedCity.buildResearchStation();
                    break;

                case "AirLift":
                    playerTurnCount++;
                    PlayerDeck.Instance.airLiftPlayer.moveToCity(pressedCity);
                    break;
            }
            waitingForCityClick = false;

        }
        else
        {
            if (pressedCity == activePlayer.city)
            {
                CardInfoManager.Instance.showInfoWhenCityPressed(pressedCity, activePlayer);
            }
            if (!playerTurnComplite())
            {
                activePlayer.canMoveToCity(pressedCity);
            }
        }
        updateMoveCount();
    }

    public void nextTurn()
    {
        PlayerCardSpawnerScript.Instance.clearPlayerHand();

        if (!PlayerDeck.Instance.quietNight)
        {
            DiseaseDeck.Instance.infectCityCount();// infice pilsetas starp gajieniem
        }

        currentPlayerIndex = (currentPlayerIndex + 1) % playerCount;
        activePlayer = playersList[currentPlayerIndex];

        PlayerCardSpawnerScript.Instance.showPlayersHand(activePlayer);

        playerTurnCount = 4;//vajag 4
        updateMoveCount();
    }

    public void updateMoveCount()
    {
        whatPlayerTurn.text = "Player " + (activePlayer.playerId+1) + " turn";
        moveCount.text = playerTurnCount.ToString() + "/4";
    }

    public void updateOutBreakCount(int outBreakCount)
    {
        outBreakCountText.text = "Out Breaks: " + outBreakCount;
    }

    public void updateInfectionRateCount(int infectionRateCount)
    {
        infectionRateCountText.text = "Infection rate: " + infectionRateCount;
    }
    
    public bool inMiddleOfAcion()
    {
        if (CardInfoManager.isPopupOpen || waitingForCityClick)
        {
            return true;
        }

        return false;
    }
}
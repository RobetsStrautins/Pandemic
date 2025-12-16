using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Mainscript : MonoBehaviour
{
    public static Mainscript main;

    public CitySpawner citySpawner;

    public GameObject playerPrefab;
    public GameObject playerTopPrefab;

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
        new GameObject("DiseaseDeck").AddComponent<DiseaseDeck>();
        DiseaseDeck.Instance.Setup();
        PlayerDeck.Instance.SetupBeforeEpidemicCard();
        
        for (int i = 0; i < playerCount; i++)
        {
            GameObject spawnedPlayer = Instantiate(playerPrefab);
            activePlayer = spawnedPlayer.GetComponent<Player>();
            activePlayer.playerColor = UnityEngine.Random.ColorHSV(0f, 1f, 0.8f, 1f, 0.8f, 1f);
            activePlayer.name = "Speletaijs" + (i + 1);
            activePlayer.playerId = i;
            playersList.Add(activePlayer);

            for (int j = 0; j < 6 - playerCount;j++)
            {
                PlayerDeck.Instance.Draw(activePlayer);
            }
    
            GameObject spawnedPlayerTop = Instantiate(playerTopPrefab);
            PlayerTop newPlayerTop = spawnedPlayerTop.GetComponent<PlayerTop>();
            newPlayerTop.Init(activePlayer);

        }

        PlayerDeck.Instance.SetupAfterEpidemicCard();

        activePlayer = playersList[0];
        PlayerHandUi.Instance.renderHand(activePlayer);

        CityData startingCity = CitySpawner.cityMap[3];//sakuma pilseta ar majinu
        startingCity.buildResearchStation();

        playerTurnCount = 4;///vajag 4
        GameUI.Instance.Setup();
    }

    public Player getActivePlayer()
    {
        return activePlayer;
    }

    private bool playerTurnComplite()
    {
        if (playerTurnCount == 0)
        {
            //Console.WriteLine("Nav Vairs gaijieni");
            return true;
        }

        return false;
    }

    public void activePlayerMoveCitys(CityData pressedCity)
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
                PopUpButtonManager.Instance.showInfoWhenCityPressed(pressedCity, activePlayer);
            }
            if (!playerTurnComplite())
            {
                activePlayer.canMoveToCity(pressedCity);
            }
        }
        GameUI.Instance.updateMoveCount();
    }

    public void nextTurn()
    {
        PlayerHandUi.Instance.clearPlayerHand();

        if (!PlayerDeck.Instance.quietNight)
        {
            DiseaseDeck.Instance.infectCityCount();// infice pilsetas starp gajieniem
        }

        currentPlayerIndex = (currentPlayerIndex + 1) % playerCount;
        activePlayer = playersList[currentPlayerIndex];

        PlayerHandUi.Instance.renderHand(activePlayer);

        playerTurnCount = 4;//vajag 4
        GameUI.Instance.updateMoveCount();
    }
    
    public bool inMiddleOfAcion()
    {
        if (PopUpButtonManager.isPopupOpen || waitingForCityClick)
        {
            return true;
        }

        return false;
    }
}
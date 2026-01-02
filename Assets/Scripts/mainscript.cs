using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Mainscript : MonoBehaviour
{
    public static Mainscript main;

    public GameObject playerPrefab;
    public GameObject playerTopPrefab;

    public int playerTurnCount;

    private int playerCount;
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
        playerCount = PlayerDeck.playerCount;

        CitySpawner.Instance.Setup();
        PlayerDeck.SetupBeforeEpidemicCard();
        
        List<PlayerRole> roles = new List<PlayerRole>
        {
            PlayerRole.Medic,
            PlayerRole.Scientist,
            PlayerRole.QuarantineSpecialist,
            PlayerRole.Researcher,
            PlayerRole.OperationsExpert
        };

        for (int i = 0; i < playerCount; i++)
        {
            GameObject spawnedPlayer = Instantiate(playerPrefab);
            activePlayer = spawnedPlayer.GetComponent<Player>();

            int radnomRole = UnityEngine.Random.Range(0, roles.Count);
            activePlayer.playerRole = roles[radnomRole];///roles iedalijums
            roles.RemoveAt(radnomRole);
            activePlayer.name = "Player " + (i + 1);
            activePlayer.playerId = i;
            activePlayer.Init();
            
            playersList.Add(activePlayer);

            for (int j = 0; j < 6 - playerCount;j++)
            {
                PlayerDeck.draw(activePlayer);
            }
    
            GameObject spawnedPlayerTop = Instantiate(playerTopPrefab);
            PlayerTop newPlayerTop = spawnedPlayerTop.GetComponent<PlayerTop>();
            newPlayerTop.Init(activePlayer);
        }

        DiseaseDeck.Setup();
        PlayerDeck.SetupAfterEpidemicCard();

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
                    PlayerDeck.airLiftPlayer.moveToCity(pressedCity);
                    break;
            }
            waitingForCityClick = false;

        }
        else
        {
            if (pressedCity == activePlayer.city)
            {
                PopUpButtonManager.Instance.showInfoWhenMyCityPressed(pressedCity, activePlayer);
            }
            if (!playerTurnComplite())
            {
                activePlayer.moveToCityIfConnected(pressedCity);
            }
        }
        GameUI.Instance.updateMoveCount();
    }

    public void nextTurn()
    {
        PlayerHandUi.Instance.clearPlayerHand();

        if (!PlayerDeck.quietNight)
        {
            DiseaseDeck.infectPhase();// infice pilsetas starp gajieniem
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

    public void putCitysUnderQuarantine(Player player)//QuarantineSpecialist ability
    {
        foreach (var city in CitySpawner.cityMap.Values)
        {
            city.cityIsUnderQuarantine = false;
        }

        if (player.playerRole == PlayerRole.QuarantineSpecialist)
        {
            CityData currentCity = player.city;
            currentCity.cityIsUnderQuarantine = true;

            foreach (var connectedCityId in currentCity.connectedCity)
            {
                CityData connectedCity = CitySpawner.cityMap[connectedCityId];
                connectedCity.cityIsUnderQuarantine = true;
            }
        }
    }

    public Color stringToColor(string s)
    {
        string str = s.ToLower();
        switch (str)
        {
            case "blue":
                return new Color(0f, 0f, 1f, 1f);
            case "red":
                return new Color(1f, 0f, 0f, 1f);
            case "yellow":
                return new Color(1f, 47f / 51f, 0.015686275f, 1f);
            case "black":
                return new Color(0f, 0f, 0f, 1f);
        }
        Debug.Log("nav krasa" + str);
        return new Color(1f, 1f, 1f, 1f);
    }
}

public enum PlayerRole
{
    Medic,
    Scientist,
    QuarantineSpecialist,
    Researcher,
    OperationsExpert,
    ContingencyPlanner,
    Dispitcher,
}
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

    void Awake() //konstruktors
    {
        main = this;
    }

    void Start() //inicializācija
    {
        playerCount = PlayerDeck.playerCount;

        CitySpawner.Instance.Setup(); //pilsetu izveide
        PlayerDeck.SetupBeforeEpidemicCard(); //spēlētāju kāršu kavas izveide
        
        List<PlayerRole> roles = new List<PlayerRole>
        {
            PlayerRole.Medic,
            PlayerRole.Scientist,
            PlayerRole.QuarantineSpecialist,
            PlayerRole.Researcher,
            PlayerRole.OperationsExpert
        };

        for (int i = 0; i < playerCount; i++) //spēlētāju izveide
        {
            GameObject spawnedPlayer = Instantiate(playerPrefab);
            activePlayer = spawnedPlayer.GetComponent<Player>();

            int radnomRole = UnityEngine.Random.Range(0, roles.Count);
            activePlayer.playerRole = roles[radnomRole]; //roles iedalijums
            roles.RemoveAt(radnomRole);
            activePlayer.name = "Spēlētājs " + (i + 1);
            activePlayer.playerId = i;
            activePlayer.Init();
            
            playersList.Add(activePlayer);

            for (int j = 0; j < 6 - playerCount; j++) //spēlētāju sākuma kāršu izdale
            {
                PlayerDeck.draw(activePlayer);
            }
    
            GameObject spawnedPlayerTop = Instantiate(playerTopPrefab);
            PlayerTop newPlayerTop = spawnedPlayerTop.GetComponent<PlayerTop>();
            newPlayerTop.Init(activePlayer);
        }

        DiseaseDeck.Setup(); //slimību kāršu kavas izveide
        PlayerDeck.SetupAfterEpidemicCard(); //epidēmijas kāršu ielikšana spēlētāju kāršu kavā

        activePlayer = playersList[0];
        PlayerHandUi.Instance.renderHand(activePlayer);

        CityData startingCity = CitySpawner.cityMap[3]; //sakuma pilseta ar majinu
        startingCity.buildResearchStation();

        playerTurnCount = 4; //vajag 4
        GameUI.Instance.Setup(); //spelesUI inicializācija

        ActionLog.Instance.addEntry("Spēle sākas!");
        ActionLog.Instance.addEntry("Spēlētājs " + (activePlayer.playerId + 1) + " sāk gājienu!", Color.blue);
    }

    public Player getActivePlayer() //atgriež aktīvo spēlētāju
    {
        return activePlayer;
    }

    private bool playerTurnComplite() //pārbauda vai spēlētājam ir beigušies gājieni
    {
        if (playerTurnCount <= 0)
        {
            ActionLog.Instance.addEntry("Darbības vairs nav, jabeidz gājiens", Color.grey);
            return true;
        }

        return false;
    }

    public void activePlayerMoveCitys(CityData pressedCity) //kad spēlētājs nospiež uz pilsētu izpilda attiecīgo darbību
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
                    if (!pressedCity.hasResearchStation())
                    {
                        pressedCity.buildResearchStation();
                    }
                    else
                    {
                        return;
                    }
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

    public void nextTurn() //nākamais gājiens
    {
        PlayerHandUi.Instance.clearPlayerHand();

        ActionLog.Instance.addEntry("--------------------------------------------");
        if (!PlayerDeck.quietNight)
        {
            DiseaseDeck.infectPhase(); // infice pilsetas starp gajieniem
        }
        else
        {
            PlayerDeck.quietNight = false;
            ActionLog.Instance.addEntry("Ir klusa nakts nekas nenotiek"); 
        }

        currentPlayerIndex = (currentPlayerIndex + 1) % playerCount;
        activePlayer = playersList[currentPlayerIndex];

        PlayerHandUi.Instance.renderHand(activePlayer);

        playerTurnCount = 4; //vajag 4
        GameUI.Instance.updateMoveCount();
        ActionLog.Instance.addEntry("--------------------------------------------");
        ActionLog.Instance.addEntry("Spēlētājs " + (activePlayer.playerId + 1) + " sāk gājienu!", Color.blue);
    }
    
    public bool inMiddleOfAcion() //pārbauda vai notiek kāda darbība
    {
        if (PopUpButtonManager.isPopupOpen || waitingForCityClick)
        {
            ActionLog.Instance.addEntry("Tu esi vidū citai darbībai ", Color.grey);
            return true;
        }

        return false;
    }

    public void putCitysUnderQuarantine(Player player) //Karanīnas speciālista spējas īstenošana
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

    public Color stringToColor(string s) //pārveido string uz Color
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
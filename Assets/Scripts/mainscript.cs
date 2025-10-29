using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mainscript : MonoBehaviour
{
    public static Mainscript main;

    public CitySpawner citySpawner;
    public PlayerCardSpawnerScript PlayerCardSpawnerScript;

    public GameObject player;
    private Player activePlayer;
    public Text moveCount;
    public int playerTurnCount;

    private bool waitingForCityClick = false;
    private PlayerCard currentCard;

    void Awake()
    {
        main = this;
    }

    void Start()
    {
        citySpawner.Setup();

        GameObject spawnedPlayer = Instantiate(player);
        activePlayer = spawnedPlayer.GetComponent<Player>();

        playerTurnCount = 1;///vajag 4
        updateMoveCount();
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

    public void activePlayerMoveCitys(CityData pressedCity)
    {
        if (!playerTurnComplite())
        {
            if (waitingForCityClick)
            {
                activePlayer.moveToCity(pressedCity);
                waitingForCityClick = false;

            }
            else
            {
                activePlayer.clearCubs(pressedCity);
                activePlayer.canMoveToCity(pressedCity); 
            }

        }
        updateMoveCount();
    }

    public void nextTurn()
    {
        int cityId = UnityEngine.Random.Range(1, 7);
        CityData randomCity = CitySpawner.cityMap[cityId];
        randomCity.addCubs(1);
        //Debug.LogWarning($"added cube to {randomCity.cityName}");
        playerTurnCount = 2;//vajag 4
        PlayerCardSpawnerScript.givePlayerCard();
    }

    public void updateMoveCount()
    {
        moveCount.text = playerTurnCount.ToString() + "/4";
    }

    public void popUp(PlayerCard tempCard)
    {
        CardInfoManager.Instance.ShowInfo(tempCard, activePlayer);
    }
    
    public void StartFlyAnywhere(PlayerCard card)
    {
        waitingForCityClick = true;
        currentCard = card;
    }

}

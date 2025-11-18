using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerId;

    private int startingCityId = 3;
    public CityData city;

    public PlayerCardList playerCardList = new PlayerCardList();
    public Color playerColor;


    void Start()
    {
        if (CitySpawner.cityMap.ContainsKey(startingCityId))
        {
            city = CitySpawner.cityMap[startingCityId];
            transform.position = new Vector3(city.Xcord, city.Ycord, -1);
        }

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = playerColor;
        }

    }

    public void canMoveToCity(CityData pressedCity)
    {
        if (city.connectedCity.Contains(pressedCity.id))
        {
            city = pressedCity;
            transform.position = new Vector3(city.Xcord, city.Ycord, -1);
            Mainscript.main.playerTurnCount -= 1;
        }
    }

    public void moveToCity(CityData pressedCity)
    {
        city = pressedCity;
        transform.position = new Vector3(city.Xcord, city.Ycord, -1);
        Mainscript.main.playerTurnCount -= 1;
    }
}

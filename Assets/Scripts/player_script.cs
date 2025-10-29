using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int startingCityId = 1;
    public CityData city;

    void Start()
    {
        if (CitySpawner.cityMap.ContainsKey(startingCityId))
        {
            city = CitySpawner.cityMap[startingCityId];
            transform.position = new Vector3(city.Xcord, city.Ycord, -1);
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

    public void clearCubs(CityData pressedCity)
    {
        if (city.id == pressedCity.id)
        {
            if (pressedCity.removeCubs(1))
            {
                Mainscript.main.playerTurnCount -= 1;
            }
        }
    }
}

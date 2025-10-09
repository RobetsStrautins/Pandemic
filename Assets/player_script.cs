using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int cityId = 1;
    private CityData city;

    void Start()
    {
        if (CitySpawner.cityMap.ContainsKey(cityId))
        {
            city = CitySpawner.cityMap[cityId];
            transform.position = new Vector3(city.Xcord, city.Ycord,-1);
        }
    }

    public void canMoveToCity(CityData moveCity)
    {
        if (city.connectedCity.Contains(moveCity.id))
        {
            city = moveCity;
            transform.position = new Vector3(city.Xcord, city.Ycord, -1);
            Mainscript.main.playerTrunCount -= 1;
        }
        else
        {
            Debug.LogWarning($"Cant move to city with ID {moveCity.id}");
        }
    }
}

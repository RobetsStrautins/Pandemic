using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    private int cityid = 1;
    private CityData city;

    void Start()
    {
        if (CitySpawner.cityMap.ContainsKey(cityid))
        {
            city = CitySpawner.cityMap[cityid];
            transform.position = new Vector3(city.Xcord, city.Ycord,-1);
        }
        else
        {
            Debug.LogWarning($"City with ID {cityid} not found!");
        }
    }

    public void canMoveToCity(CityData moveCity)
    {
        if (city.connectedcity.Contains(moveCity.id))
        {
            city = moveCity;
            transform.position = new Vector3(city.Xcord, city.Ycord, -1);
            mainscript.main.playerTrunCount -= 1;
        }
        else
        {
            Debug.LogWarning($"Cant move to city with ID {moveCity.id}");
        }
    }
}

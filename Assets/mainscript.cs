using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mainscript : MonoBehaviour
{
    public static Mainscript main;  

    public CitySpawner citySpawner;
    public GameObject player;
    private Player activePlayer;
    public int playerTrunCount = 4;

    void Awake()
    {
        main = this;
    }

    void Start()
    {
        citySpawner.Setup();

        GameObject spawnedPlayer = Instantiate(player);
        activePlayer = spawnedPlayer.GetComponent<Player>();
    }

    void Update()
    {
        if (playerTrunCount == 0)
        {

            int cityId = Random.Range(1, 7);
            CityData randomCity = CitySpawner.cityMap[cityId];
            randomCity.addCubs(1);
            Debug.LogWarning($"added cube to {randomCity.cityName}");
            playerTrunCount = 2;//vajag 4
        }
    }

    public void activePlayerMoveCitys(CityData pressedCity)
    {
        activePlayer.clearCubs(pressedCity);
        activePlayer.canMoveToCity(pressedCity);
    }
}

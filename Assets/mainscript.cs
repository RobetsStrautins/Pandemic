using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainscript : MonoBehaviour
{
    public static mainscript main;  

    public CitySpawner citySpawner;
    public GameObject player;
    private player activePlayer;
    public int playerTrunCount = 4;

    void Awake()
    {
        main = this;
    }

    void Start()
    {
        citySpawner.Setup();

        GameObject spawnedPlayer = Instantiate(player);
        activePlayer = spawnedPlayer.GetComponent<player>();
    }

    void Update()
    {
        if (playerTrunCount == 0)
        {
            int cityid = Random.Range(1, 7);
            CityData randomCity = CitySpawner.cityMap[cityid];

            randomCity.addCubs(1);
        }
    }

    public void activePlayerMoveCitys(CityData movecity)
    {
        activePlayer.canMoveToCity(movecity);
    }
}

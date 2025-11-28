using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiseaseDeck : MonoBehaviour
{
    public static DiseaseDeck Instance;

    public List<CityData> infectionDeck = new List<CityData>();
    public List<CityData> usedInfectionDeck = new List<CityData>();

    private List<CityData>  CitiesOutBreakHappend = new();
    public int OutBreakCount = 0;
    private int infectionRateIndex = 0;
    private int[] infectionRateList = {2,2,2,3,3,4,4};

    void Awake()
    {
        Instance = this;
    }

    public void Setup()
    {
        infectionDeck.Clear();

        foreach (var city in CitySpawner.cityMap.Values)
        {
            infectionDeck.Add(city);
        }

        shuffle(infectionDeck);

        for(int i = 3;i > 0;i--)
        {
            for(int j = 0;j > 3;j++)
            {
                infectCities(i);
            }
        }
    }

    private void shuffle(List<CityData> list)
    {
        int cardCount = list.Count;

        while (cardCount > 1)
        {
            cardCount--;
            int swap = Random.Range(0, cardCount + 1);
            (list[swap], list[cardCount]) = (list[cardCount], list[swap]); 
        }
    }

    public void infectCities(int cubeCount)
    {
        CityData infectedCity = infectionDeck[0];

        infectedCity.addCubs(cubeCount);
        
        CitiesOutBreakHappend.Clear();
        
        usedInfectionDeck.Add(infectionDeck[0]);
        infectionDeck.Remove(infectionDeck[0]);
    }

    public void infectCityCount()
    {
        for (int i = 0; i < infectionRateList[infectionRateIndex]; i++)
        {
            infectCities(1);
        }
    }

    public void epidemic()
    {

        Debug.LogWarning("aaaa epidemija");

        int temp = infectionDeck.Count-1;
        CityData lastCity = infectionDeck[temp];

        lastCity.addCubs(3);
        CitiesOutBreakHappend.Clear();

        usedInfectionDeck.Add(infectionDeck[temp]);
        infectionDeck.Remove(infectionDeck[temp]);

        shuffle(usedInfectionDeck);

        infectionDeck.AddRange(usedInfectionDeck);
        usedInfectionDeck.Clear();

        infectionRateIndex++;
        Mainscript.main.updateInfectionRateCount(infectionRateList[infectionRateIndex]);
        
    }

    public void outBreak(CityData outBreakCity)
    {
        OutBreakCount++;
        Mainscript.main.updateOutBreakCount(OutBreakCount);

        if(OutBreakCount >= 8)
        {
            Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            GameOverScript.Instance.GameLost();
        }

        if(!CitiesOutBreakHappend.Contains(outBreakCity))
        {
            CitiesOutBreakHappend.Add(outBreakCity);
        }
        
        foreach (int cityId in outBreakCity.connectedCity)
        {
            CityData closeCity = CitySpawner.cityMap[cityId];
            if (!CitiesOutBreakHappend.Contains(closeCity))
            {
                closeCity.addCubs(1);
            }
        }
    }
}

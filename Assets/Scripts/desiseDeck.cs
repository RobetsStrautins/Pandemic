using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesiseDeck : MonoBehaviour
{
    public static DesiseDeck Instance;

    public List<CityData> infectionDeck = new List<CityData>();
    public List<CityData> usedInfectionDeck = new List<CityData>();

    private List<CityData>  CitiesOutBreakHappend = new();
    public int OutBreakCount = 0;

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

        Debug.Log($"Infection deck built. {infectionDeck.Count} cards.");

        infectCities(3);
        infectCities(3);
        infectCities(2);
        infectCities(2);
        infectCities(1);
        infectCities(1);

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

    public void epidemic()
    {
        int temp = infectionDeck.Count-1;
        CityData lastCity = infectionDeck[temp];

        lastCity.addCubs(3);
        CitiesOutBreakHappend.Clear();

        usedInfectionDeck.Add(infectionDeck[temp]);
        infectionDeck.Remove(infectionDeck[temp]);

        shuffle(usedInfectionDeck);

        infectionDeck.AddRange(usedInfectionDeck);
        usedInfectionDeck.Clear();
    }

    public void outBreak(CityData outBreakCity)
    {
        OutBreakCount++;
        Mainscript.main.updateOutBreakCount(OutBreakCount);

        GameOverScript.Instance.checkIfGameLost();

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
                Debug.LogWarning($"added cube to {closeCity.cityName}");
            }
        }
    }
}

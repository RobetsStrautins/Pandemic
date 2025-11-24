using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesiseDeck : MonoBehaviour
{
    public static DesiseDeck Instance;

    public List<CityData> infectionDeck = new List<CityData>();
    public List<CityData> usedInfectionDeck = new List<CityData>();

    void Awake()
    {
        Instance=this;
    }

    public void Setup()
    {
        infectionDeck.Clear();

        foreach (var city in CitySpawner.cityMap.Values)
        {
            infectionDeck.Add(city);
        }

        Shuffle(infectionDeck, infectionDeck.Count);

        Debug.Log($"Infection deck built. {infectionDeck.Count} cards.");
    }

    private void Shuffle(List<CityData> list, int cardCount)
    {
        while (cardCount > 1)
        {
            cardCount--;
            int swap = Random.Range(0, cardCount + 1);
            (list[swap], list[cardCount]) = (list[cardCount], list[swap]); 
        }
    }

    public void infectCities()
    {
        usedInfectionDeck.Add(infectionDeck[0]);
        infectionDeck.Remove(infectionDeck[0]);
    }


}

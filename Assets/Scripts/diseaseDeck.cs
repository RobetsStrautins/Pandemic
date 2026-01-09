using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DiseaseDeck
{
    private static List<CityData> infectionDeck = new List<CityData>();
    public static List<CityData> usedInfectionDeck = new List<CityData>();

    private static List<CityData> citiesOutBreakHappend = new List<CityData>();
    private static int outBreakCount;

    private static int infectionRateIndex;
    private static int[] infectionRateList = {2,2,2,3,3,4,4};

    public static void Setup() //inficē sākuma pilsētas
    {
        infectionDeck.Clear();
        usedInfectionDeck.Clear();
        citiesOutBreakHappend.Clear();
        
        outBreakCount = 0;
        infectionRateIndex = 0;

        foreach (var city in CitySpawner.cityMap.Values)
        {
            infectionDeck.Add(city);
        }

        shuffle(infectionDeck);

        for(int i = 3; i > 0 ; i--)
        {
            ActionLog.Instance.addEntry("Nakamās tris pilsētas tiek inficētas ar " + i + " kubiciņiem.");
            for(int j = 0; j < 3; j++)
            {
                infectCities(i);
            }
        }
        ActionLog.Instance.addEntry("--------------------------------------------");
    }

    private static void shuffle(List<CityData> list) //sajauc infekciju kārtis
    {
        int cardCount = list.Count;

        while (cardCount > 1)
        {
            cardCount--;
            int swap = Random.Range(0, cardCount + 1);
            (list[swap], list[cardCount]) = (list[cardCount], list[swap]); 
        }
    }

    private static void infectCities(int cubeCount) //inficē pilsētu
    {
        CityData infectedCity = infectionDeck[0];

        ActionLog.Instance.addEntry("Inficē " + infectedCity.cityName );
        infectedCity.addCubes(cubeCount);
        
        citiesOutBreakHappend.Clear();
        
        usedInfectionDeck.Add(infectionDeck[0]);
        infectionDeck.Remove(infectionDeck[0]);
    }

    public static void infectPhase() //inficēšanas fāze gājiena beigās
    {
        for (int i = 0; i < infectionRateList[infectionRateIndex]; i++)
        {
            infectCities(1);
        }
    }

    public static void epidemic() //epidēmijas funkcija
    {
        Debug.LogWarning("aaaa epidemija");

        int temp = infectionDeck.Count-1;
        CityData lastCity = infectionDeck[temp];

        lastCity.addCubes(3);
        citiesOutBreakHappend.Clear();
        ActionLog.Instance.addEntry("Epidēmija " + lastCity.cityName, Color.red);
        ActionLog.Instance.addEntry("Tiek pielikti 3 kubiciņi " + lastCity.cityName);
        usedInfectionDeck.Add(infectionDeck[temp]);
        infectionDeck.Remove(infectionDeck[temp]);

        shuffle(usedInfectionDeck);

        infectionDeck.InsertRange(0, usedInfectionDeck);
        usedInfectionDeck.Clear();

        infectionRateIndex++;
        GameUI.Instance.updateInfectionRateCount(infectionRateList[infectionRateIndex],infectionRateIndex);
    }

    public static void outBreak(CityData outBreakCity) //uzliesmojuma funkcija
    {
        outBreakCount++;
        GameUI.Instance.updateOutBreakCount(outBreakCount);

        if(outBreakCount >= 8)
        {
            GameUI.Instance.gameLost("Uzliesmojumi sasniedza 8");
        }

        ActionLog.Instance.addEntry(outBreakCity.cityName + " uzliesmo! Tas ir " + outBreakCount + ". uzliesmojums!", Color.red);
        
        if(!citiesOutBreakHappend.Contains(outBreakCity))
        {
            citiesOutBreakHappend.Add(outBreakCity);
        }
        
        foreach (int cityId in outBreakCity.connectedCity)
        {
            CityData closeCity = CitySpawner.cityMap[cityId];
            if (!citiesOutBreakHappend.Contains(closeCity))
            {
                closeCity.addCubes(1);
            }
        }
    }
}

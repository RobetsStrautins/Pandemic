using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerDeckTests
{
    [SetUp]
    public void Setup()
    {
        CitySpawner.cityMap = new Dictionary<int, CityData>();

        CitySpawner.cityMap[1] = CreateCity(1);
        CitySpawner.cityMap[2] = CreateCity(2);
        CitySpawner.cityMap[3] = CreateCity(3);
    }

    [Test]
    public void SetupBeforeEpidemicCard_CreatesCityCards()
    {
        PlayerDeck.SetupBeforeEpidemicCard();

        int expectedCardCount = CitySpawner.cityMap.Count + 4;
        Assert.AreEqual(expectedCardCount, PlayerDeck.testDeck.Count);
    }

    [Test]
    public void SetupAfterEpidemicCard_AddsEpidemicCards()
    {
        PlayerDeck.SetupBeforeEpidemicCard();
        PlayerDeck.SetupAfterEpidemicCard();

        int epidemicCount = PlayerDeck.testDeck.Count(c => c.Type == CardType.Epidemic);

        Assert.AreEqual(PlayerDeck.epidemicCardCount, epidemicCount);
    }

    // ---------- Helpers ----------

    private CityData CreateCity(int id)
    {
        return new CityData
        {
            id = id,
            connectedCity = new List<int>()
        };
    }
}

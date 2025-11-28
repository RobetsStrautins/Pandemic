using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : MonoBehaviour
{
    public static PlayerDeck Instance;

    public int epidemicCount = 4;

    public List<CardData> deck = new List<CardData>();
    public List<CardData> usedDeck = new List<CardData>();

    void Awake()
    {
        Instance = this;
    }

    public void Setup()
    {
        deck.Clear();
        usedDeck.Clear();

        foreach (var city in CitySpawner.cityMap.Values)
        {
            deck.Add(new PlayerCityCardData(city));
        }

        for (int i = 0; i < 5; i++)
        {
            //deck.Add(new BonusCardData("Bonus Card " + (i + 1), Random.Range(1, 5)));
        }

        for (int i = 0; i < epidemicCount; i++)
        {
            deck.Add(new PandemicCardData("Pandemic"));
        }

        shuffle(deck);
    }

    public CardData Draw()
    {
        if (deck.Count == 0)
        {
            deck.AddRange(usedDeck);
            usedDeck.Clear();
            shuffle(deck);
        }

        CardData top = deck[0];
        deck.RemoveAt(0);
        return top;
    }

    public void shuffle(List<CardData> list)
    {
        int cardCount=list.Count;
        while (cardCount > 1)
        {
            cardCount--;
            int swap = Random.Range(0, cardCount + 1);
            (list[swap], list[cardCount]) = (list[cardCount], list[swap]); 
        }
    }
}

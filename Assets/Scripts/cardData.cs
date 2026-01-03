using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    City,
    Event,
    Epidemic
}

public enum EventCardType
{
    GovernmentGrant,
    QuietNight,
    ResilientPopulation,
    Airlift,
    Forecast,
}

[System.Serializable]
public abstract class CardData 
{
    public CardType Type;

    protected CardData(CardType type) //konstruktors
    {
        Type = type;
    }
}

[System.Serializable]
public class PlayerCityCardData : CardData
{
    public CityData cityCard;

    public PlayerCityCardData(CityData city) : base(CardType.City) //konstruktors
    {
        cityCard = city;
    }
}

[System.Serializable]
public class EventCardData : CardData
{
    public string title;
    public string description;
    public EventCardType eventType;

    public EventCardData(EventCardType type, string title, string description): base(CardType.Event) //konstruktors
    {
        eventType = type;
        this.title = title;
        this.description = description;
    }
}

[System.Serializable]
public class EpidemicCardData : CardData
{
    public EpidemicCardData() : base(CardType.Epidemic) {  }//konstruktors
}
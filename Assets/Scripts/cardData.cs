using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    City,
    Bonus,
    Pandemic
}

public abstract class CardData 
{
    public CardType Type;

    protected CardData(CardType type)
    {
        Type = type;
    }
}

public class PlayerCityCardData : CardData
{
    public CityData cityCard;

    public PlayerCityCardData(CityData city) : base(CardType.City)
    {
        cityCard = city;
    }
}

public class PandemicCardData : CardData
{
    public string eventName;

    public PandemicCardData(string name) : base(CardType.Pandemic)
    {
        eventName = name;
    }
}





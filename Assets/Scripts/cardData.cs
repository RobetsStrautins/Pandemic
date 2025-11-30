using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    City,
    Bonus,
    Epidemic
}

public enum BonusCardType
{
    ValdibasSubsidija,
    KlusaNakts,
    PopulacijasPretosanas,
    GaisaTransportas,
    Prognoze
}

[System.Serializable]
public abstract class CardData 
{
    public CardType Type;

    protected CardData(CardType type)
    {
        Type = type;
    }
}

[System.Serializable]
public class PlayerCityCardData : CardData
{
    public CityData cityCard;

    public PlayerCityCardData(CityData city) : base(CardType.City)
    {
        cityCard = city;
    }
}

[System.Serializable]
public class BonusCardData : CardData
{
    public string title;
    public string description;
    public BonusCardType bonusType;

    public BonusCardData(BonusCardType type, string title, string description): base(CardType.Bonus)
    {
        bonusType = type;
        this.title = title;
        this.description = description;
    }
}

[System.Serializable]
public class EpidemicCardData : CardData
{
    public string eventName;

    public EpidemicCardData(string name) : base(CardType.Epidemic)
    {
        eventName = name;
    }
}

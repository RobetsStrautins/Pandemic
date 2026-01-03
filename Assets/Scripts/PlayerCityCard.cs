using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCityCard : MonoBehaviour
{
    public CardData cardData;
    public CityData cardsCityData;
    public Text cityLabel;
    public GameObject cardBackgroundColor;
    
    public void Init(CardData data) //inicializē spēlētāja pilsētas karti
    {
        cardData = data;

        PlayerCityCardData card = data as PlayerCityCardData;
        cardsCityData = card.cityCard;

        name = "Card " + cardsCityData.cityName;

        cardBackgroundColor.GetComponent<SpriteRenderer>().color = cardsCityData.unityColor;

        if (cityLabel != null)
            cityLabel.text = cardsCityData.cityName;
    }

    private void OnMouseDown() //kad nospiež uz spēlētāja pilsētas karti
    {
        if (Mainscript.main.inMiddleOfAcion())
        {
            return;
        }

        PopUpButtonManager.Instance.showInfoWhenCardPressed(this, Mainscript.main.getActivePlayer());

    }
}

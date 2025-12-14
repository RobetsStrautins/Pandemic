using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCityCard : MonoBehaviour
{
    public CardNode myNode;
    public CityData cardsCityData;
    public Text cityLabel;
    public GameObject cardBackgroundColor;
    
    public void Init(CardNode cardNode)
    {
        myNode = cardNode;

        PlayerCityCardData card = myNode.data as PlayerCityCardData;
        cardsCityData = card.cityCard;

        name = "Card " + cardsCityData.cityName;

        cardBackgroundColor.GetComponent<SpriteRenderer>().color = cardsCityData.unityColor;

        if (cityLabel != null)
            cityLabel.text = cardsCityData.cityName;
    }

    private void OnMouseDown()
    {
        if (Mainscript.main.inMiddleOfAcion())
        {
            return;
        }

        PopUpButtonManager.Instance.showInfoWhenCardPressed(this, Mainscript.main.getActivePlayer());

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCityCardInPopUp : MonoBehaviour
{
    public CardData cardData;
    public CityData cardsCityData;
    public Text cityLabel;
    public Image cardBackgroundColor;

    public Button onClickButton;

    private bool isSelected = false;
    private Color baseColor;
    private Color selectedColor;


    public void Init(string text, Action onClick, CardData data) //inicializē spēlētāja pilsētas karti pop-up logā
    {
        cardData = data;
        
        name = text + " button";
        onClickButton.onClick.RemoveAllListeners();
        onClickButton.onClick.AddListener(() => onClick.Invoke());

        PlayerCityCardData card = data as PlayerCityCardData;
        cardsCityData = card.cityCard;

        name = "Card " + cardsCityData.cityName;

        cardBackgroundColor.color = cardsCityData.unityColor;
        baseColor = cardsCityData.unityColor;
        selectedColor = baseColor * 0.7f; 

        if (cityLabel != null)
            cityLabel.text = cardsCityData.cityName;
    }
    

    public void selectCard() //izvēlas vai atceļ izvēli uz kartes
    {
        if (!isSelected && SelectionManager.canSelectMore())
        {
            isSelected = true;
            SelectionManager.selectCard(this);
            cardBackgroundColor.color = selectedColor;
        }
        else if (isSelected)
        {
            isSelected = false;
            SelectionManager.deselectCard(this);
            cardBackgroundColor.color = baseColor;
        }

    }

    public bool IsSelected => isSelected;
}

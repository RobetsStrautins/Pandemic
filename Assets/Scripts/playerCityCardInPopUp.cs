using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCityCardInPopUp : MonoBehaviour
{
    public CardNode myNode;
    public CityData cardsCityData;
    public Text cityLabel;
    public Image cardBackgroundColor;

    public Button onClickButton;

    private bool isSelected = false;
    private Color baseColor;
    private Color selectedColor;


    public void Init(string text, Action onClick, CardNode cardNode)
    {
        myNode = cardNode;
        
        name = text + " button";
        onClickButton.onClick.RemoveAllListeners();
        onClickButton.onClick.AddListener(() => onClick.Invoke());

        PlayerCityCardData card = myNode.data as PlayerCityCardData;
        cardsCityData = card.cityCard;

        name = "Card " + cardsCityData.cityName;

        cardBackgroundColor.color = cardsCityData.unityColor;
        baseColor = cardsCityData.unityColor;
        selectedColor = baseColor * 0.7f; 

        if (cityLabel != null)
            cityLabel.text = cardsCityData.cityName;
    }
    

    public void onCardClicked()
    {
        if (!isSelected && SelectionManager.Instance.CanSelectMore())
        {
            isSelected = true;
            SelectionManager.Instance.SelectCard(this);
            cardBackgroundColor.color = selectedColor;
        }
        else if (isSelected)
        {
            isSelected = false;
            SelectionManager.Instance.DeselectCard(this);
            cardBackgroundColor.color = baseColor;
        }

    }

    public bool IsSelected => isSelected;
}

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

    private bool isSelected = false;
    private Color baseColor;
    private Color selectedColor;

    private bool multiCardSelect;

    public void Init(CardNode cardNode , bool multi)
    {
        myNode = cardNode;
        multiCardSelect = multi;

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
        if (SelectionManager.Instance == null)
        {
            return;
        } 

        if(multiCardSelect)
        {
            onCardClickedWhenMultipleCard();
        }
        else
        {
            onCardClickedWhenSingleCard();
        }
    }

    public void onCardClickedWhenMultipleCard()
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

    public void onCardClickedWhenSingleCard()
    {
        PopUpButtonManager.Instance.showInfoWhenFromOtherPLayer(myNode, Mainscript.main.getActivePlayer());
    }

    public bool IsSelected => isSelected;
}

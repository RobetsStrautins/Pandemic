using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCardInPopUp : MonoBehaviour
{
    public CardNode myNode;
    public Text cityLabel;
    public GameObject cardBackgroundColor;

    public void Init(CardNode cardNode)
    {
        myNode = cardNode;

        name = "Card " + myNode.cityCard.cityName;

        cardBackgroundColor.GetComponent<Image>().color = myNode.cityCard.unityColor;

        if (cityLabel != null)
            cityLabel.text = myNode.cityCard.cityName;
    }
}

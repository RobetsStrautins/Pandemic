using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCard : MonoBehaviour
{
    public CardNode myNode;
    public Text cityLabel;
    public GameObject cardBackgroundColor;
    
    public void Init(CardNode cardNode)
    {
        myNode = cardNode;

        name = "Card " + myNode.cityCard.cityName;

        cardBackgroundColor.GetComponent<SpriteRenderer>().color = myNode.cityCard.unityColor;
        if (cityLabel != null)
            cityLabel.text = myNode.cityCard.cityName;
    } 

    private void OnMouseDown()
    {
        if (Mainscript.main.inMiddleOfAcion())
        {
            return;
        }

        CardInfoManager.Instance.showInfoWhenCardPressed(this, Mainscript.main.getActivePlayer());

    }
}

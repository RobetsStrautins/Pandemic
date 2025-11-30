using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerBonesCard : MonoBehaviour
{
    public CardNode myNode;
    public Text title;
    public Text description;
    public BonusCardType bonusCardType;

    public void Init(CardNode cardNode)
    {
        myNode = cardNode;

        BonusCardData bonusCard = myNode.data as BonusCardData;

        bonusCardType = bonusCard.bonusType;

        name = bonusCard.title;

        title.text = bonusCard.title;
        description.text = bonusCard.description;
    }

    private void OnMouseDown()
    {
        if (Mainscript.main.inMiddleOfAcion())
        {
            return;
        }

        CardInfoManager.Instance.showInfoWhenBonusCardPressed(title.text, bonusCardType, myNode, Mainscript.main.getActivePlayer());

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBonesCard : MonoBehaviour
{
    public CardData cardData;
    public Text title;
    public Text description;
    public void Init(CardData data)
    {
        cardData = data;

        BonusCardData bonusCard = data as BonusCardData;

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

        PopUpButtonManager.Instance.showInfoWhenBonusCardPressed(cardData, Mainscript.main.getActivePlayer());

    }
}

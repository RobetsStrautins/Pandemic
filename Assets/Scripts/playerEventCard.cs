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

        EventCardData eventCard = data as EventCardData;

        name = eventCard.title;

        title.text = eventCard.title;
        description.text = eventCard.description;
    }

    private void OnMouseDown()
    {
        if (Mainscript.main.inMiddleOfAcion())
        {
            return;
        }

        PopUpButtonManager.Instance.showInfoWhenEventCardPressed(cardData, Mainscript.main.getActivePlayer());

    }
}

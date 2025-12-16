using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBonusCardInPopUp : MonoBehaviour
{
    public CardData cardData;
    public Text title;
    public Text description;

    public Button onClickButton;

    public void Init(string text, Action onClick, CardData data)
    {
        cardData = data;

        BonusCardData bonusCard = data as BonusCardData;

        name = bonusCard.title;

        title.text = bonusCard.title;
        description.text = bonusCard.description;

        name = text + " button";
        onClickButton.onClick.RemoveAllListeners();
        onClickButton.onClick.AddListener(() => onClick.Invoke());
    }
}

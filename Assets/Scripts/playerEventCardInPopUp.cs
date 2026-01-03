using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEventCardInPopUp : MonoBehaviour
{
    public CardData cardData;
    public Text title;
    public Text description;

    public Button onClickButton;

    public void Init(string text, Action onClick, CardData data) //inicializē spēlētāja notikumu kārti pop-up logā
    {
        cardData = data;

        EventCardData eventCard = data as EventCardData;

        name = eventCard.title;

        title.text = eventCard.title;
        description.text = eventCard.description;

        name = text + " button";
        onClickButton.onClick.RemoveAllListeners();
        onClickButton.onClick.AddListener(() => onClick.Invoke());
    }
}

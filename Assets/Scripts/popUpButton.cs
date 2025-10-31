using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public enum ButtonActionType
{
    Card,
    City,
}

public class PopUpButton : MonoBehaviour
{
    public TMP_Text buttonText;
    
    private PlayerCard card;
    private CityData city;

    private ButtonActionType buttonActionType;

    public void Init(string name, PlayerCard card, PopUpScript popup)
    {
        this.name = name;
        this.card = card;

        buttonActionType = ButtonActionType.Card;

        if (name == "flyTo")
        {
            buttonText.text = "Lidot uz " + card.cityCard.cityName;
            Button btn = GetComponent<Button>();
            btn.onClick.AddListener(() => popup.flyTo(card));
        }
        else if (name == "flyAnywhere")
        {
            buttonText.text = "Lidot no " + card.cityCard.cityName;
            Button btn = GetComponent<Button>();
            btn.onClick.AddListener(() => popup.flyAnywhere(card));
        }
        else if (name == "makereRearchStation")
        {
            buttonText.text = "Uztaisit majinu";
            Button btn = GetComponent<Button>();
            btn.onClick.AddListener(() => popup.makereRearchStation(card));
        }
        else
        {
            buttonText.text = name;
        }

    }

    public void Init(string name, CityData city)
    {
        this.name = name;
        this.city = city;
        buttonActionType = ButtonActionType.City;

        buttonText.text = name;

    }

    private void OnMouseDown()
    {
        if (!Mainscript.main.playerTurnComplite())
        {
            if (buttonActionType == ButtonActionType.Card)
            {
                pressedCardButton();
            }
            else
            {
                pressedCityButton();
            }
            CardInfoManager.Instance.hideInfo();
        }
        else
        {
            Debug.Log("Nav pietiekami daudz gajieni");
        }
    }

    private void pressedCardButton()
    {
        if (name == "flyTo")
        {
            if (!Mainscript.main.playerTurnComplite())
            {
                Mainscript.main.flytoCity(card.cityCard);
            }
        }
        else if (name == "flyAnywhere")
        {
            Debug.Log("Nospied pilsetu");
            Mainscript.main.StartFlyAnywhere(card);
        }
        else if (name == "makereRearchStation")
        {
            card.cityCard.buildResearchStation();
        }

        PlayerCardSpawnerScript.playerCardList.removeCards(card.myNode);
    }
    
    private void pressedCityButton()
    {
        if (name == "flyTo")
        {
            if (!Mainscript.main.playerTurnComplite())
            {
                Mainscript.main.flytoCity(card.cityCard);
            }
        }
    }
}

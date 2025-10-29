using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class popUpButton : MonoBehaviour
{
    public TMP_Text buttonText;

    private PlayerCard card;

    public void Init(string name, PlayerCard card)
    {
        this.name = name;
        this.card = card;

        if (name == "flyTo")
        {
            buttonText.text = "Lidot uz " + card.cityCard.cityName;
        }
        else if (name == "flyAnywhere")
        {
            buttonText.text = "Lidot no " + card.cityCard.cityName;
        }
       
    }


    private void OnMouseDown()
    {
        if (!Mainscript.main.playerTurnComplite())
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

            PlayerCardSpawnerScript.playerCardList.removeCards(card.myNode);
            CardInfoManager.Instance.hideInfo();
        }
        else
        {
            Debug.Log("Nav pietiekami daudz gajieni");
        }
    }
}

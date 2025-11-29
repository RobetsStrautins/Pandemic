using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class PopUpButton : MonoBehaviour
{
    public TMP_Text buttonText;

    public void Init(string name, PlayerCityCard card, PopUpScript popup)
    {
        this.name = name;
        Button btn = GetComponent<Button>();

        if (name == "flyTo")
        {
            buttonText.text = "Lidot uz " + card.cardsCityData.cityName;
            btn.onClick.AddListener(() => popup.flyTo(card));
        }
        else if (name == "flyAnywhere")
        {
            buttonText.text = "Lidot no " + card.cardsCityData.cityName;
            btn.onClick.AddListener(() => popup.flyAnywhere(card));
        }
        else if (name == "makeRearchStation")
        {
            buttonText.text = "Uztaisit majinu";
            btn.onClick.AddListener(() => popup.makeRearchStation(card));
        }
        else if (name == "removeCard")
        {
            buttonText.text = "Nomest karti";
            btn.onClick.AddListener(() => popup.removeCard(card));
        }
        else
        {
            buttonText.text = name;
        }

        if (Mainscript.main.playerTurnComplite() && name != "removeCard")
        {
            btn.interactable = false;
        }
    }
    
    public void Init(string name, PlayerCityCard card, PopUpScript popup, Player player)
    {
        this.name = name;
        Button btn = GetComponent<Button>();

        if (name == "giveCard")
        {
            buttonText.text = "Iedot karti speletajam " + (player.playerId+1);
            btn.onClick.AddListener(() => popup.giveCard(card, player));
        }
        else
        {
            buttonText.text = name;
        }
    }
    
    public void Init(string name, string cityname, CardNode cardNode, PopUpScript popup, Player player)
    {
        this.name = name;
        Button btn = GetComponent<Button>();

        if (name == "takeCard")
        {
            buttonText.text = $"Paņemt {cityname} karti no {player.playerId+1}";
            btn.onClick.AddListener(() => popup.takeCard(cardNode, player));
        }
        else
        {
            buttonText.text = name;
        }
    }

    public void Init(string name, CityData city, PopUpScript popup)
    {
        this.name = name;
        buttonText.text = name;

        Button btn = GetComponent<Button>();

        if (name == "flyToResearchStation")
        {
            btn.onClick.AddListener(() => popup.flyToResearchStation(city));
        }
        else if (name == "trasport")
        {
            btn.onClick.AddListener(() => popup.trasportTo(city));
            buttonText.text = city.cityName;
        }

        string[] nameSplit = name.Split(' ');

        if (nameSplit[0] == "cureDisease")
        {
            btn.onClick.AddListener(() => popup.cureDiseasePopUp(nameSplit[1]));
            buttonText.text = "Izarstet slimibu " + nameSplit[1];
        }

        else if (nameSplit[0] == "Remove")
        {
            if (int.TryParse(nameSplit[1], out int cubeCount))
            {
                if (cubeCount > Mainscript.main.playerTurnCount)
                {
                    btn.interactable = false;
                }

                btn.onClick.AddListener(() => popup.clearCubs(city, cubeCount));
            }
            else
            {
                btn.onClick.AddListener(() => popup.clearAllCubs(city));
            }
        }
        
        if (Mainscript.main.playerTurnComplite())
        {
            btn.interactable = false;
        }

    }
}

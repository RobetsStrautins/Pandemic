using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardInfoManager : MonoBehaviour
{
    public GameObject panel;
    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public Transform popUp;
    public GameObject button;

    public PopUpScript popupScript;

    public static CardInfoManager Instance;
    public static bool isPopupOpen = false;

    private List<GameObject> buttonList = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public void showInfoWhenCardPressed(PlayerCard card, Player player)//izarstet,lidota ar maju,nonemt cubicinu
    {
        isPopupOpen = true;

        GameObject cardObj;
        PopUpButton newButton;
        if (player.city == card.myNode.cityCard)
        {
            cardObj = Instantiate(button, popUp.transform);
            newButton = cardObj.GetComponentInChildren<PopUpButton>();
            newButton.Init("flyAnywhere", card, popupScript);
            buttonList.Add(cardObj);

            if (!card.myNode.cityCard.hasResearchStation())
            {
                cardObj = Instantiate(button, popUp.transform);
                newButton = cardObj.GetComponentInChildren<PopUpButton>();
                newButton.Init("makereRearchStation", card, popupScript);
                buttonList.Add(cardObj);
            }

            foreach (Player playerInList in Mainscript.main.playersList)
            {            
                if (player != playerInList && playerInList.city==card.myNode.cityCard)
                {
                    cardObj = Instantiate(button, popUp.transform);
                    newButton = cardObj.GetComponentInChildren<PopUpButton>();
                    newButton.Init("giveCard", card, popupScript, playerInList);
                    buttonList.Add(cardObj);
                }
            }
        }
        else
        {
            cardObj = Instantiate(button, popUp.transform);
            newButton = cardObj.GetComponentInChildren<PopUpButton>();
            newButton.Init("flyTo", card, popupScript);
            buttonList.Add(cardObj);
        }

        cardObj = Instantiate(button, popUp.transform);
        newButton = cardObj.GetComponentInChildren<PopUpButton>();
        newButton.Init("removeCard", card, popupScript);
        buttonList.Add(cardObj);

        buttonPos();
        titleText.text = card.myNode.cityCard.cityName;
        descriptionText.text = "cardDescription";
        panel.SetActive(true);
    }

    public void showInfoWhenCityPressed(CityData city, Player player)
    {
        isPopupOpen = true;

        GameObject cardObj;
        PopUpButton newButton;

        string colorThatCanBeCured = countCardColors(player);
        //Debug.Log("aaaadsa" + DesiseMarkers.Instance.desiseColorDict[colorThatCanBeCured].isCuredDesise);
        if (colorThatCanBeCured !=null && city.hasResearchStation())
        {
            if(!DesiseMarkers.Instance.desiseColorDict[colorThatCanBeCured].isCuredDesise)
            {
                cardObj = Instantiate(button, popUp.transform);
                newButton = cardObj.GetComponentInChildren<PopUpButton>();
                newButton.Init("cureDesise " + colorThatCanBeCured, city, popupScript);
                buttonList.Add(cardObj);
            }
        }
        
        if (city.hasResearchStation() && Mainscript.main.researchStationOnMap.Count >= 2)
        {
            cardObj = Instantiate(button, popUp.transform);
            newButton = cardObj.GetComponentInChildren<PopUpButton>();
            newButton.Init("flyToResearchStation", city, popupScript);
            buttonList.Add(cardObj);
        }


        int cubs = city.getCubs();
        if (cubs >= 1 && !DesiseMarkers.Instance.desiseColorDict[city.color])
        {
            cardObj = Instantiate(button, popUp.transform);
            newButton = cardObj.GetComponentInChildren<PopUpButton>();
            newButton.Init("Remove " + cubs + " cubs", city, popupScript);
            buttonList.Add(cardObj);
        } 
        
        for (int i = 1; i <= cubs; i++)
        {
            cardObj = Instantiate(button, popUp.transform);
            newButton = cardObj.GetComponentInChildren<PopUpButton>();
            newButton.Init("Remove " + i + " cubs", city, popupScript);
            buttonList.Add(cardObj);
        }

        buttonPos();
        titleText.text = "Ko darit " + city.cityName;
        descriptionText.text = "cardDescription";
        panel.SetActive(true);
    }
    
    public void showReserchOpcions(CityData pressedcity)
    {
        isPopupOpen = true;

        GameObject cardObj;
        PopUpButton newButton;

        foreach (CityData city in Mainscript.main.researchStationOnMap)
        {
            if (!(pressedcity == city))
            {
                cardObj = Instantiate(button, popUp.transform);
                newButton = cardObj.GetComponentInChildren<PopUpButton>();
                newButton.Init("trasport", city, popupScript);
                buttonList.Add(cardObj);
            }
        }
        
        buttonPos();
        titleText.text = "Uz kuru lidot?";
        descriptionText.text = "cardDescription";
        panel.SetActive(true);
    }

    private void buttonPos()
    {
        float startY = 300;
        int index = 0;

        foreach (GameObject obj in buttonList)
        {
            RectTransform rt = obj.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0f, startY + index * -80);
            index++;
        }
    }

    public void hideInfo()
    {
        panel.SetActive(false);
        isPopupOpen = false;

        foreach (GameObject obj in buttonList)
        {
            Destroy(obj);
        }
        buttonList.Clear();

        PlayerCardSpawnerScript.Instance.showPlayersHand(Mainscript.main.getActivePlayer());
    }

    private string countCardColors(Player player)
    {
        var colorCounts = new Dictionary<string, int>
        {
            { "Red", 0 },
            { "Yellow", 0 },
            { "Black", 0 },
            { "Blue", 0 }
        };

        CardNode current = player.playerCardList.first;

        while (current != null)
        {
            if (colorCounts.ContainsKey(current.cityCard.color))
            {
                colorCounts[current.cityCard.color]++;
            }

            current = current.next;
        }

        foreach (var color in colorCounts)
        {
            if (color.Value >= 5)
            {
                return color.Key;   
            }
        }

        return null;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CardInfoManager : MonoBehaviour
{
    public GameObject panel;
    public Text titleText;
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

    public void showInfoWhenCardPressed(PlayerCityCard card, Player player)//izarstet,lidota ar maju,nonemt cubicinu
    {
        isPopupOpen = true;

        GameObject cardObj;
        PopUpButton newButton;

        if (card.myNode.data.Type != CardType.City)
        {
           return; 
        }

        if (player.city == card.cardsCityData)
        {
            cardObj = Instantiate(button, popUp.transform);
            newButton = cardObj.GetComponentInChildren<PopUpButton>();
            newButton.Init("flyAnywhere", card, popupScript);
            buttonList.Add(cardObj);

            if (!card.cardsCityData.hasResearchStation())
            {
                cardObj = Instantiate(button, popUp.transform);
                newButton = cardObj.GetComponentInChildren<PopUpButton>();
                newButton.Init("makeRearchStation", card, popupScript);
                buttonList.Add(cardObj);
            }

            foreach (Player playerInList in Mainscript.main.playersList)
            {            
                if (player != playerInList && playerInList.city == card.cardsCityData)
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

        titleText.text = card.cardsCityData.cityName;
        panel.SetActive(true);
        StartCoroutine(scrollToTop());
    }

    public void showInfoWhenBonusCardPressed(string title, BonusCardType bonusCardType, CardNode node,  Player player)
    {
        isPopupOpen = true;

        GameObject cardObj;
        PopUpButton newButton;

        cardObj = Instantiate(button, popUp.transform);
        newButton = cardObj.GetComponentInChildren<PopUpButton>();
        newButton.Init("useCard", title, bonusCardType, node, popupScript, player);
        buttonList.Add(cardObj);

        cardObj = Instantiate(button, popUp.transform);
        newButton = cardObj.GetComponentInChildren<PopUpButton>();
        newButton.Init("removeCard", title, bonusCardType, node, popupScript, player);
        buttonList.Add(cardObj);

        titleText.text = title;
        panel.SetActive(true);
        StartCoroutine(scrollToTop());
    }

    public void showInfoWhenCityPressed(CityData city, Player player)
    {
        isPopupOpen = true;

        GameObject cardObj;
        PopUpButton newButton;

        string colorThatCanBeCured = countCardColors(player);
        //Debug.Log("aaaadsa" + DiseaseMarkers.Instance.diseaseColorDict[colorThatCanBeCured].isCuredDisease);
        if (colorThatCanBeCured !=null && city.hasResearchStation())
        {
            if(!DiseaseMarkers.Instance.diseaseColorDict[colorThatCanBeCured].isCuredDisease)
            {
                cardObj = Instantiate(button, popUp.transform);
                newButton = cardObj.GetComponentInChildren<PopUpButton>();
                newButton.Init("cureDisease " + colorThatCanBeCured, city, popupScript);
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
        if (cubs >= 1 && DiseaseMarkers.Instance.diseaseColorDict[city.color].isCuredDisease)
        {
            cardObj = Instantiate(button, popUp.transform);
            newButton = cardObj.GetComponentInChildren<PopUpButton>();
            newButton.Init("Remove all cubs", city, popupScript);
            buttonList.Add(cardObj);
        }
        else
        {
            for (int i = 1; i <= cubs; i++)
            {
                cardObj = Instantiate(button, popUp.transform);
                newButton = cardObj.GetComponentInChildren<PopUpButton>();
                newButton.Init("Remove " + i + " cubs", city, popupScript);
                buttonList.Add(cardObj);
            }  
        }

        titleText.text = "Ko darit " + city.cityName;
        panel.SetActive(true);
        StartCoroutine(scrollToTop());
    }

    public void showInfoWhenFromOtherPLayer(CardNode cardNode, Player player)
    {
        isPopupOpen = true;

        GameObject cardObj;
        PopUpButton newButton;

        if (cardNode.data.Type == CardType.City)
        {
            PlayerCityCardData card = cardNode.data as PlayerCityCardData;
            CityData cardsCityData = card.cityCard;

            foreach (Player playerInList in Mainscript.main.playersList)
            {            
                if (player != playerInList && playerInList.city == cardsCityData)
                {
                    cardObj = Instantiate(button, popUp.transform);
                    newButton = cardObj.GetComponentInChildren<PopUpButton>();
                    newButton.Init("takeCard", cardsCityData.cityName, cardNode, popupScript, playerInList);
                    buttonList.Add(cardObj);
                }
            }

            titleText.text = cardsCityData.cityName;
            panel.SetActive(true);
            StartCoroutine(scrollToTop());
        }

        if (buttonList.Count != 0)
        {
            PlayerCardInfoManager.Instance.hideInfo();
        }
        else
        {
            hideInfo();
        }
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
        
        titleText.text = "Uz kuru lidot?";
        panel.SetActive(true);
        StartCoroutine(scrollToTop());
    }

    public void showAllOpcionsDiscardDisease(CardNode cardNode, Player player)
    {
        isPopupOpen = true;

        GameObject cardObj;
        PopUpButton newButton;

        foreach (CityData city in DiseaseDeck.Instance.usedInfectionDeck)
        {
            cardObj = Instantiate(button, popUp.transform);
            newButton = cardObj.GetComponentInChildren<PopUpButton>();
            newButton.Init(city, popupScript, cardNode, player);
            buttonList.Add(cardObj);
        }
        
        titleText.text = "Kuru karti iznemt";
        panel.SetActive(true);
        StartCoroutine(scrollToTop());
    }
    public void showAllPlayers(CardNode cardNode, Player player)
    {
        isPopupOpen = true;

        GameObject cardObj;
        PopUpButton newButton;

        foreach (Player playerlist in Mainscript.main.playersList)
        {
            cardObj = Instantiate(button, popUp.transform);
            newButton = cardObj.GetComponentInChildren<PopUpButton>();
            newButton.Init(playerlist, popupScript, cardNode, player);
            buttonList.Add(cardObj);
        }
        
        titleText.text = "Kuru karti iznemt";
        panel.SetActive(true);
        StartCoroutine(scrollToTop());
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
            if(current.data.Type == CardType.City)
            {
                var currentCity = current.data as PlayerCityCardData;

                if (colorCounts.ContainsKey(currentCity.cityCard.color))
                {
                    colorCounts[currentCity.cityCard.color]++;
                }  
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

    private IEnumerator scrollToTop()
    {
        yield return null;

        ScrollRect scroll = panel.GetComponentInChildren<ScrollRect>();
        scroll.verticalNormalizedPosition = 1;
    }
}

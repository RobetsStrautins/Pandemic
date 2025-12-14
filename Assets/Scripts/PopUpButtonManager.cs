using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PopUpButtonManager : MonoBehaviour
{
    public GameObject panel;
    public Text titleText;
    public Transform popUp;
    public GameObject button;

    public PopUpScript popupScript;

    public static PopUpButtonManager Instance;
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

        if (card.myNode.data.Type != CardType.City)
        {
           return; 
        }

        if (player.city == card.cardsCityData)
        {
            CreateButton("Lidot no " + card.cardsCityData.cityName, () => popupScript.flyAnywhere(card));

            if (!card.cardsCityData.hasResearchStation())
            {
                CreateButton("Uztaisīt mājiņu", () => popupScript.makeRearchStation(card));
            }

            foreach (Player p in Mainscript.main.playersList)
            {
                if (p != player && p.city == card.cardsCityData)
                {
                    CreateButton("Iedot karti spēlētājam " + (p.playerId + 1), () => popupScript.giveCard(card, p));
                }
            }
        }
        else
        {
            CreateButton("Lidot uz " + card.cardsCityData.cityName, () => popupScript.flyTo(card));
        }

        CreateButton("Nomest karti", () => popupScript.removeCard(card.myNode));

        titleText.text = card.cardsCityData.cityName;
        panel.SetActive(true);
        StartCoroutine(scrollToTop());
    }

    public void showInfoWhenBonusCardPressed(string title, BonusCardType bonusCardType, CardNode node,  Player player)
    {
        isPopupOpen = true;

        switch (bonusCardType)
        {
            case BonusCardType.KlusaNakts:
                CreateButton("Izmantot " + title, () => popupScript.quietNight(node, player));
                break;

            case BonusCardType.PopulacijasPretosanas:
                CreateButton("Izmantot " + title, () => popupScript.resilientPopulation(node, player));
                break;

            case BonusCardType.ValdibasSubsidija:
                CreateButton("Izmantot " + title, () => popupScript.governmentGrant(node, player));
                break;

            case BonusCardType.GaisaTransportas:
                CreateButton("Izmantot " + title, () => popupScript.airLift(node, player));
                break;
        }

        CreateButton("Nomest karti", () => popupScript.removeCard(node));

        titleText.text = title;
        panel.SetActive(true);
        StartCoroutine(scrollToTop());
    }

    public void showInfoWhenCityPressed(CityData city, Player player)
    {
        isPopupOpen = true;

        string colorThatCanBeCured = countCardColors(player);

        if (colorThatCanBeCured !=null && city.hasResearchStation())
        {
            if(!DiseaseMarkers.Instance.diseaseColorDict[colorThatCanBeCured].isCuredDisease)
            {
                CreateButton("Izarstet slimibu " + colorThatCanBeCured, () => popupScript.cureDiseasePopUp(colorThatCanBeCured));
            }
        }
        
        if (city.hasResearchStation() && Mainscript.main.researchStationOnMap.Count >= 2)
        {
            CreateButton("Lidot starp stacijam" + colorThatCanBeCured, () => popupScript.flyToResearchStation(city));
        }

        int cubs = city.getCubs();

        if (cubs >= 1 && DiseaseMarkers.Instance.diseaseColorDict[city.color].isCuredDisease)
        {
            CreateButton("Nonemt visus kubicinus", () => popupScript.clearAllCubs(city));
        }
        else
        {
            for (int i = 1; i <= cubs; i++)
            {
                int cubeCount = i;
                if(i > Mainscript.main.playerTurnCount)
                {
                    CreateButton($"Nonemt {i} kubicinus", () => popupScript.clearCubs(city, cubeCount), false);
                }
                else
                {
                    CreateButton($"Nonemt {i} kubicinus", () => popupScript.clearCubs(city, cubeCount));
                }
            }  
        }

        titleText.text = "Ko darit " + city.cityName;
        panel.SetActive(true);
        StartCoroutine(scrollToTop());
    }

    public void showInfoWhenFromOtherPLayer(CardNode cardNode, Player player)
    {
        isPopupOpen = true;

        if (cardNode.data.Type == CardType.City)
        {
            PlayerCityCardData card = cardNode.data as PlayerCityCardData;
            CityData cardsCityData = card.cityCard;

            foreach (Player playerInList in Mainscript.main.playersList)
            {            
                if (player != playerInList && playerInList.city == cardsCityData)
                {
                    CreateButton($"Paņemt {cardsCityData.cityName} karti no {player.playerId+1}", () => popupScript.takeCard(cardNode, playerInList));
                }
            }

            titleText.text = cardsCityData.cityName;
            panel.SetActive(true);
            StartCoroutine(scrollToTop());
        }

        if (buttonList.Count != 0)
        {
            PopUpCardManager.Instance.hideInfo();
        }
        else
        {
            hideInfo();
        }
    }
    
    public void showReserchOpcions(CityData pressedcity)
    {
        isPopupOpen = true;

        foreach (CityData city in Mainscript.main.researchStationOnMap)
        {
            if (!(pressedcity == city))
            {
                CreateButton(city.cityName, () => popupScript.trasportTo(city));
            }
        }
        
        titleText.text = "Uz kuru lidot?";
        panel.SetActive(true);
        StartCoroutine(scrollToTop());
    }

    public void showAllOpcionsDiscardDisease(CardNode cardNode, Player player)
    {
        isPopupOpen = true;

        foreach (CityData city in DiseaseDeck.Instance.usedInfectionDeck)
        {
            CreateButton(city.cityName, () => popupScript.discardDisease(city, cardNode, player));
        }
        
        titleText.text = "Kuru karti iznemt";
        panel.SetActive(true);
        StartCoroutine(scrollToTop());
    }

    public void showAllPlayers(CardNode cardNode, Player player)
    {
        isPopupOpen = true;

        foreach (Player playerList in Mainscript.main.playersList)
        {
            CreateButton("Parvietot speletaju " + (playerList.playerId + 1) + " uz jebkuru pilsetu", () => popupScript.airLift2(playerList, cardNode, player));
        }
        
        titleText.text = "Kuru cilveku parcel";
        panel.SetActive(true);
        StartCoroutine(scrollToTop());
    }

    void CreateButton(string text, Action onClick, bool interactable = true)
    {
        GameObject obj = Instantiate(button, popUp.transform);
        PopUpButtonAction newButton = obj.GetComponentInChildren<PopUpButtonAction>();
        newButton.Init(text, onClick, interactable);
        buttonList.Add(obj);
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

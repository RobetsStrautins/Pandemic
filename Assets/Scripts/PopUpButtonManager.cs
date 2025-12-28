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
    public GameObject buttonPrefab;

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
        if (Mainscript.main.playerTurnCount == 0)
        {
           return; 
        }

        isPopupOpen = true;

        if (card.cardData.Type != CardType.City)
        {
           return; 
        }

        if(player.playerRole == PlayerRole.OperationsExpert && player.city.hasResearchStation())
        {
            CreateButton("Lidot uz jebkuru pilsetu (Operaciju specialists)", () => popupScript.flyAnywhere(card));

            foreach (Player p in Mainscript.main.playersList)
            {
                if (p != player && p.city == card.cardsCityData)
                {
                    CreateButton("Iedot karti spēlētājam " + (p.playerId + 1), () => popupScript.giveCard(card, p));
                }
            }
        }
        else if (player.city == card.cardsCityData)
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

        CreateButton("Nomest karti", () => popupScript.removeCard(card.cardData, player));

        titleText.text = card.cardsCityData.cityName;
        panel.SetActive(true);
        StartCoroutine(scrollToTop());
    }

    public void showInfoWhenBonusCardPressed(CardData data, Player player)
    {

        isPopupOpen = true;

        var card = data as BonusCardData;
        switch (card.bonusType)
        {
            case BonusCardType.KlusaNakts:
                CreateButton("Izmantot " + card.title, () => popupScript.quietNight(data, player));
                break;

            case BonusCardType.PopulacijasPretosanas:
                CreateButton("Izmantot " + card.title, () => popupScript.resilientPopulation(data, player));
                break;

            case BonusCardType.ValdibasSubsidija:
                CreateButton("Izmantot " + card.title, () => popupScript.governmentGrant(data, player));
                break;

            case BonusCardType.GaisaTransportas:
                CreateButton("Izmantot " + card.title, () => popupScript.airLift(data, player));
                break;
        }

        if (player == Mainscript.main.getActivePlayer())
        {
            CreateButton("Nomest karti", () => popupScript.removeCard(data, player));
        }

        titleText.text = card.title;
        panel.SetActive(true);
        StartCoroutine(scrollToTop());
    }

    public void showInfoWhenMyCityPressed(CityData city, Player player)
    {
        if (Mainscript.main.playerTurnCount == 0)
        {
           return; 
        }

        isPopupOpen = true;

        string colorThatCanBeCured = countCardColors(player);

        if (colorThatCanBeCured != null && city.hasResearchStation())
        {
            if(DiseaseMarkers.Instance.getDiseaseProgress(colorThatCanBeCured) == DiseaseColorProgress.NotCured)
            {
                CreateButton("Izarstet slimibu " + colorThatCanBeCured, () => popupScript.cureDiseasePopUp(colorThatCanBeCured));
            }
        }
        
        if (city.hasResearchStation() && Mainscript.main.researchStationOnMap.Count >= 2)
        {
            CreateButton("Lidot starp stacijam", () => popupScript.flyToResearchStation(city));
        }

        int cubs = city.getCubs();

        if (cubs >= 1 && (DiseaseMarkers.Instance?.getDiseaseProgress(city.color) != DiseaseColorProgress.NotCured || player.playerRole == PlayerRole.Medic))
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
        
        if(player.playerRole == PlayerRole.OperationsExpert && !city.hasResearchStation())
        {
            CreateButton("Uztaisīt mājiņu (Operaciju specialists)", () => city.buildResearchStation());
        }


        titleText.text = "Ko darit " + city.cityName;
        panel.SetActive(true);
        StartCoroutine(scrollToTop());
    }

    public void showInfoWhenCityCardsFromOtherPlayer(CardData data, Player playerThatWasPressed)
    {
        if (Mainscript.main.playerTurnCount == 0)
        {
           return; 
        }

        isPopupOpen = true;

        if (data.Type == CardType.City)
        {
            PlayerCityCardData card = data as PlayerCityCardData;
            CityData cardsCityData = card.cityCard;

            Player activePlayer = Mainscript.main.getActivePlayer();

            if (activePlayer.city == cardsCityData)//abi atrodas viena pilseta)
            {
                CreateButton($"Paņemt {cardsCityData.cityName} karti no {playerThatWasPressed.playerId+1} speletaja", () => popupScript.takeCard(data, playerThatWasPressed));
            }
            else if (playerThatWasPressed.playerRole == PlayerRole.Researcher || activePlayer.playerRole == PlayerRole.Researcher)//Researcher ability
            {
                CreateButton($"Paņemt {cardsCityData.cityName} karti no {playerThatWasPressed.playerId+1} speletaja (Petnieks)", () => popupScript.takeCard(data, playerThatWasPressed));
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
    
    public void showCitysWithReserchStation(CityData pressedcity)
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

    public void showAllDiscardDiseaseCards(CardData data, Player player)
    {
        isPopupOpen = true;

        foreach (CityData city in DiseaseDeck.usedInfectionDeck)
        {
            CreateButton(city.cityName, () => popupScript.discardDisease(city, data, player));
        }
        
        titleText.text = "Kuru karti iznemt";
        panel.SetActive(true);
        StartCoroutine(scrollToTop());
    }

    public void showAllPlayers(CardData data, Player player)
    {
        isPopupOpen = true;

        foreach (Player playerList in Mainscript.main.playersList)
        {
            CreateButton("Parvietot speletaju " + (playerList.playerId + 1) + " uz jebkuru pilsetu", () => popupScript.airLift2(playerList, data, player));
        }
        
        titleText.text = "Kuru cilveku parcel";
        panel.SetActive(true);
        StartCoroutine(scrollToTop());
    }

    public void showRemoveOpcion(CardData data,  Player player)
    {
        isPopupOpen = true;

        if(data.Type == CardType.City)
        {
            var card = data as PlayerCityCardData;
            CreateButton("Nomest " + card.cityCard.cityName + " karti", () => popupScript.removeCard(data, player));
            titleText.text = card.cityCard.cityName;
        }
        else if(data.Type == CardType.Bonus)
        {
            var card = data as BonusCardData;
            CreateButton("Nomest " + card.Type.ToString() + " karti", () => popupScript.removeCard(data, player));
            switch (card.bonusType)
            {
                case BonusCardType.KlusaNakts:
                    CreateButton("Izmantot " + card.title, () => popupScript.quietNight(data, player));
                    break;

                case BonusCardType.PopulacijasPretosanas:
                    CreateButton("Izmantot " + card.title, () => popupScript.resilientPopulation(data, player));
                    break;

                case BonusCardType.ValdibasSubsidija:
                    CreateButton("Izmantot " + card.title, () => popupScript.governmentGrant(data, player));
                    break;

                case BonusCardType.GaisaTransportas:
                    CreateButton("Izmantot " + card.title, () => popupScript.airLift(data, player));
                    break;
            }

            titleText.text = card.title;
        }
        
        panel.SetActive(true);
        StartCoroutine(scrollToTop());
    }

    void CreateButton(string text, Action onClick, bool interactable = true)
    {
        GameObject obj = Instantiate(buttonPrefab, popUp.transform);
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
            { "Yellow", 0 },
            { "Red", 0 },
            { "Blue", 0 },
            { "Black", 0 }
        };

        int neededCardCount = 5;

        if (player.playerRole == PlayerRole.Scientist)
        {
            neededCardCount = 4;
        }

        foreach (CardData cardData in player.playerCardList.getAllCards())
        {
            if (cardData.Type == CardType.City)
            {
                var cityCard = cardData as PlayerCityCardData;
                if (colorCounts.ContainsKey(cityCard.cityCard.color))
                {
                    colorCounts[cityCard.cityCard.color]++;
                }
            }
        }

        foreach (var color in colorCounts)
        {
            if (color.Value >= neededCardCount)
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

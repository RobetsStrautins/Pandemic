using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;
using System;

public class PopUpCardManager : MonoBehaviour
{
    public static PopUpCardManager Instance;
    public GameObject panel;
    public GameObject confirmButton;
    public GameObject closeButton;
    private RectTransform closeButtonRT;
    public Text titleText;
    public Transform popUp;
    public GameObject playerPopUpCityCardsPrefab;
    public GameObject playerPopUpBonesCardsPrefab;

    private List<GameObject> buttonList = new List<GameObject>();

    private void Awake() //konstruktors
    {
        Instance = this;
        panel.SetActive(false);

        closeButtonRT = closeButton.GetComponent<RectTransform>();
    }

    public void showPlayerCardFromPlayerTop(Player player) //parāda cita spēlētāja kartis 
    {
        PopUpButtonManager.isPopupOpen = true;

        foreach (CardData cardData in player.playerCardList.getAllCards())
        {
            CardData data = cardData;
            if (data.Type == CardType.City)
            {
                CreateCityCard("City", () => PopUpButtonManager.Instance.showInfoWhenCityCardsFromOtherPlayer(data, player), data);
            }
            else if (data.Type == CardType.Event)
            {
                var card = data as EventCardData;
                CreateEventCard("Event", () => PopUpButtonManager.Instance.showInfoWhenEventCardPressed(data, player), data);
            }
        }

        closeButtonRT.anchoredPosition = new Vector2(0, -190);
        confirmButton.SetActive(false);

        CardPos();
        titleText.text = "Speletaja " + (player.playerId + 1) + " kartis";
        panel.SetActive(true);
    }

    public void pickCardsToCureDisease(string tempColor) //parāda spēlētāja kartis, kuras var izmantot slimības izārstēšanai
    {
        Player player = Mainscript.main.getActivePlayer();

        PopUpButtonManager.isPopupOpen = true;

        foreach (CardData cardData in player.playerCardList.getAllCards())
        {
            if(cardData.Type == CardType.City)
            {
                var currentCity = cardData as PlayerCityCardData;
                CardData data = cardData;
                SelectionManager.selectedCards.Clear();
                if(currentCity.cityCard.color == tempColor)
                {
                    GameObject obj = Instantiate(playerPopUpCityCardsPrefab, popUp.transform);
                    PlayerCityCardInPopUp newPlayerCityCardInPopUp = obj.GetComponentInChildren<PlayerCityCardInPopUp>();
                    newPlayerCityCardInPopUp.Init("selecto karti", () => newPlayerCityCardInPopUp.selectCard(), data);
                    buttonList.Add(obj);
                }
            }

        }

        closeButtonRT.anchoredPosition = new Vector2(-400, -190);
        confirmButton.SetActive(true);

        CardPos();
        titleText.text = "Izvelies kartis kuras izmantot";
        panel.SetActive(true);
    }

    public void maxCardLimit(Player player) //parāda spēlētāja kartis, ja ir pārsniegts karšu limits
    {
        PopUpButtonManager.isPopupOpen = true;

        foreach (CardData cardData in player.playerCardList.getAllCards())
        {
            CardData data = cardData;
            if (data.Type == CardType.City)
            {
                CreateCityCard("City", () => PopUpButtonManager.Instance.showRemoveOpcion(data, player), data);
            }
            else if (data.Type == CardType.Event)
            {
                CreateEventCard("Event", () => PopUpButtonManager.Instance.showRemoveOpcion(data, player), data);
            }
        }

        closeButtonRT.anchoredPosition = new Vector2(0, -190);
        confirmButton.SetActive(false);

        CardPos();
        titleText.text = "Speletajam ir par daudz kartis, janomet karti";
        panel.SetActive(true);
    }

    private void CreateCityCard(string text, Action onClick, CardData data) //izveido pilsētas karti pop-up logā
    {
        GameObject obj = Instantiate(playerPopUpCityCardsPrefab, popUp.transform);
        PlayerCityCardInPopUp newPlayerCityCardInPopUp = obj.GetComponentInChildren<PlayerCityCardInPopUp>();
        newPlayerCityCardInPopUp.Init(text, onClick, data);
        buttonList.Add(obj);
    }

    private void CreateEventCard(string text, Action onClick, CardData data) //izveido notikumu karti pop-up logā
    {
        GameObject obj = Instantiate(playerPopUpBonesCardsPrefab, popUp.transform);
        PlayerEventCardInPopUp newPlayerEventCardInPopUp = obj.GetComponentInChildren<PlayerEventCardInPopUp>();
        newPlayerEventCardInPopUp.Init(text, onClick, data);
        buttonList.Add(obj);
    }

    private void CardPos() //izkārto kartis pop-up logā
    {
        float startX = -805;
        int index = 0;

        foreach (GameObject obj in buttonList)
        {
            RectTransform rt = obj.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(startX + index * + 200, 0f);
            index++;
        }
    }

    public void hideInfo() //aizver pop-up logu
    {
        panel.SetActive(false);
        PopUpButtonManager.isPopupOpen = false;

        foreach (GameObject obj in buttonList)
        {
            Destroy(obj);
        }
        buttonList.Clear();
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;
using System;

public class PopUpCardManager : MonoBehaviour
{
    public GameObject panel;
    public GameObject confirmButton;
    public GameObject closeButton;
    private RectTransform closeButtonRT;
    public Text titleText;
    public Transform popUp;
    public GameObject playerPopUpCityCardsPrefab;
    public GameObject playerPopUpBonesCardsPrefab;

    public static PopUpCardManager Instance;

    private List<GameObject> buttonList = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
        panel.SetActive(false);

        closeButtonRT = closeButton.GetComponent<RectTransform>();
    }

    public void showPlayerCardFromPLayerTop(Player player)
    {
        PopUpButtonManager.isPopupOpen = true;

        foreach (CardData cardData in player.playerCardList.getAllCards())
        {
            CardData data = cardData;
            if (data.Type == CardType.City)
            {
                CreateCityCard("City", () => PopUpButtonManager.Instance.showInfoWhenCityCardsFromOtherPlayer(data, player), data);
            }
            else if (data.Type == CardType.Bonus)
            {
                var card = data as BonusCardData;
                CreateBonusCard("Bonus", () => PopUpButtonManager.Instance.showInfoWhenBonusCardPressed(data, player), data);
            }
        }

        closeButtonRT.anchoredPosition = new Vector2(0, -190);
        confirmButton.SetActive(false);

        CardPos();
        titleText.text = "Speletaja " + (player.playerId + 1) + " kartis";
        panel.SetActive(true);
    }

    public void pickCardsToCureDisease(string tempColor)
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
                    newPlayerCityCardInPopUp.Init("selecto karti", () => newPlayerCityCardInPopUp.onCardClicked(), data);
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

    public void maxCardLimit(Player player)
    {
        PopUpButtonManager.isPopupOpen = true;

        foreach (CardData cardData in player.playerCardList.getAllCards())
        {
            CardData data = cardData;
            if (data.Type == CardType.City)
            {
                CreateCityCard("City", () => PopUpButtonManager.Instance.showRemoveOpcion(data, player), data);
            }
            else if (data.Type == CardType.Bonus)
            {
                CreateBonusCard("Bonus", () => PopUpButtonManager.Instance.showRemoveOpcion(data, player), data);
            }
        }

        closeButtonRT.anchoredPosition = new Vector2(0, -190);
        confirmButton.SetActive(false);

        CardPos();
        titleText.text = "Speletajam ir par daudz kartis, janomet karti";
        panel.SetActive(true);
    }

    void CreateCityCard(string text, Action onClick, CardData data)
    {
        GameObject obj = Instantiate(playerPopUpCityCardsPrefab, popUp.transform);
        PlayerCityCardInPopUp newPlayerCityCardInPopUp = obj.GetComponentInChildren<PlayerCityCardInPopUp>();
        newPlayerCityCardInPopUp.Init(text, onClick, data);
        buttonList.Add(obj);
    }

    void CreateBonusCard(string text, Action onClick, CardData data)
    {
        GameObject obj = Instantiate(playerPopUpBonesCardsPrefab, popUp.transform);
        PlayerBonusCardInPopUp newPlayerBonusCardInPopUp = obj.GetComponentInChildren<PlayerBonusCardInPopUp>();
        newPlayerBonusCardInPopUp.Init(text, onClick, data);
        buttonList.Add(obj);
    }

    private void CardPos()
    {
        float startX = -805;
        int index = 0;

        foreach (GameObject obj in buttonList)
        {
            RectTransform rt = obj.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(startX + index * + 230, 0f);
            index++;
        }
    }

    public void hideInfo()
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


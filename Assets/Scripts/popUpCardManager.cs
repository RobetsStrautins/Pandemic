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

        CardNode current = player.playerCardList.first;

        while (current != null)
        {
            CardNode node = current;
            if (current.data.Type == CardType.City)
            {
                CreateCityCard("City", () => PopUpButtonManager.Instance.showInfoWhenFromOtherPLayer(node, Mainscript.main.getActivePlayer()), node);
            }
            else if (current.data.Type == CardType.Bonus)
            {
                var card = current.data as BonusCardData;
                CreateBonusCard("Bonus", () => PopUpButtonManager.Instance.showInfoWhenBonusCardPressed(node, player), node);
            }

            current = current.next;
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

        CardNode current = player.playerCardList.first;

        while (current != null)
        {
            if(current.data.Type == CardType.City)
            {
                var currentCity = current.data as PlayerCityCardData;
                CardNode node = current;

                if(currentCity.cityCard.color == tempColor)
                {
                    GameObject obj = Instantiate(playerPopUpCityCardsPrefab, popUp.transform);
                    PlayerCityCardInPopUp newPlayerCityCardInPopUp = obj.GetComponentInChildren<PlayerCityCardInPopUp>();
                    newPlayerCityCardInPopUp.Init("selecto karti", () => newPlayerCityCardInPopUp.onCardClicked(), node);
                    buttonList.Add(obj);
                }
            }
            
            current = current.next;
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

        CardNode current = player.playerCardList.first;

        while (current != null)
        {
            CardNode node = current;
            if (current.data.Type == CardType.City)
            {
                CreateCityCard("City", () => PopUpButtonManager.Instance.showRemoveOpcion(node, player), node);
            }
            else if (current.data.Type == CardType.Bonus)
            {
                CreateBonusCard("Bonus", () => PopUpButtonManager.Instance.showRemoveOpcion(node, player), node);
            }

            current = current.next;
        }

        closeButtonRT.anchoredPosition = new Vector2(0, -190);
        confirmButton.SetActive(false);

        CardPos();
        titleText.text = "Speletajam ir par daudz kartis, janomet karti";
        panel.SetActive(true);
    }

    void CreateCityCard(string text, Action onClick, CardNode node)
    {
        GameObject obj = Instantiate(playerPopUpCityCardsPrefab, popUp.transform);
        PlayerCityCardInPopUp newPlayerCityCardInPopUp = obj.GetComponentInChildren<PlayerCityCardInPopUp>();
        newPlayerCityCardInPopUp.Init(text, onClick, node);
        buttonList.Add(obj);
    }

    void CreateBonusCard(string text, Action onClick, CardNode node)
    {
        GameObject obj = Instantiate(playerPopUpBonesCardsPrefab, popUp.transform);
        PlayerBonusCardInPopUp newPlayerBonusCardInPopUp = obj.GetComponentInChildren<PlayerBonusCardInPopUp>();
        newPlayerBonusCardInPopUp.Init(text, onClick,node);
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


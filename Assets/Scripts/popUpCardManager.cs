using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PopUpCardManager : MonoBehaviour
{
    public GameObject panel;
    public GameObject confirmButton;
    public GameObject closeButton;
    private RectTransform closeButtonRT;
    public Text titleText;
    public Transform popUp;
    public GameObject playerPopUpCityCards;
    public GameObject playerPopUpBonesCards;

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

        GameObject cardObj;
        PlayerCityCardInPopUp playerCityCardInPopUp;
        PlayerBonusCardInPopUp playerBonusCardInPopUp;

        CardNode current = player.playerCardList.first;

        while (current != null)
        {
            if (current.data.Type == CardType.City)
            {
                cardObj = Instantiate(playerPopUpCityCards, popUp.transform);
                playerCityCardInPopUp = cardObj.GetComponentInChildren<PlayerCityCardInPopUp>();
                playerCityCardInPopUp.Init(current, false);
                buttonList.Add(cardObj);
            }
            else if (current.data.Type == CardType.Bonus)
            {
                cardObj = Instantiate(playerPopUpBonesCards, popUp.transform);
                playerBonusCardInPopUp = cardObj.GetComponentInChildren<PlayerBonusCardInPopUp>();
                playerBonusCardInPopUp.Init(current, player);
                buttonList.Add(cardObj);
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

        GameObject cardObj;
        PlayerCityCardInPopUp playerCityCardInPopUp;

        CardNode current = player.playerCardList.first;

        while (current != null)
        {
            if(current.data.Type == CardType.City)
            {
                var currentCity = current.data as PlayerCityCardData;

                if(currentCity.cityCard.color == tempColor)
                {
                    cardObj = Instantiate(playerPopUpCityCards, popUp.transform);
                    playerCityCardInPopUp = cardObj.GetComponentInChildren<PlayerCityCardInPopUp>();
                    playerCityCardInPopUp.Init(current, true);
                    buttonList.Add(cardObj);
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


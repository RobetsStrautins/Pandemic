using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class CureInfoManager : MonoBehaviour
{
    public GameObject panel;
    public TMP_Text titleText;
    public Transform popUp;
    public GameObject playerPanalCards;

    public PopUpScript popupScript;
    public static CureInfoManager Instance;

    private List<GameObject> buttonList = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }
    
    public void pickCardsToCureDisease(string tempColor)
    {
        Player player = Mainscript.main.getActivePlayer();

        CardInfoManager.isPopupOpen = true;

        GameObject cardObj;
        PlayerCardInPopUp playerCardInPopUp;

        CardNode current = player.playerCardList.first;

        while (current != null)
        {
            if(current.data.Type == CardType.City)
            {
                var currentCity = current.data as PlayerCityCardData;

                if(currentCity.cityCard.color == tempColor)
                {
                    cardObj = Instantiate(playerPanalCards, popUp.transform);
                    playerCardInPopUp = cardObj.GetComponentInChildren<PlayerCardInPopUp>();
                    playerCardInPopUp.Init(current, true);
                    buttonList.Add(cardObj);
                }

                current = current.next;
            }
        }

        buttonPos();
        titleText.text = "Izvelies kartis kuras izmantot";
        panel.SetActive(true);
    }

    private void buttonPos()
    {
        float startX = -805;
        int index = 0;

        foreach (GameObject obj in buttonList)
        {
            RectTransform rt = obj.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(startX + index * +230, 0f);
            index++;
        }
    }

    public void hideInfo()
    {
        
        panel.SetActive(false);
        CardInfoManager.isPopupOpen = false;

        foreach (GameObject obj in buttonList)
        {
            Destroy(obj);
        }

        SelectionManager.Instance.selectedCards.Clear();
        buttonList.Clear();
    }
}


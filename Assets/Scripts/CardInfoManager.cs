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

    private List<GameObject> buttonList = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public void ShowInfoWhenCardPressed(PlayerCard card, Player player)//izarstet,lidota ar maju,nonemt cubicinu
    {
        GameObject cardObj;
        PopUpButton newButton;
        if (player.city == card.cityCard)
        {
            cardObj = Instantiate(button, popUp.transform);
            newButton = cardObj.GetComponentInChildren<PopUpButton>();
            newButton.Init("flyAnywhere", card, popupScript);
            buttonList.Add(cardObj);

            if (!card.cityCard.hasResearchStation())
            {
                cardObj = Instantiate(button, popUp.transform);
                newButton = cardObj.GetComponentInChildren<PopUpButton>();
                newButton.Init("makereRearchStation", card, popupScript);
                buttonList.Add(cardObj);
            }
        }
        else
        {
            cardObj = Instantiate(button, popUp.transform);
            newButton = cardObj.GetComponentInChildren<PopUpButton>();
            newButton.Init("flyTo", card, popupScript);
            buttonList.Add(cardObj);
        }

        buttonPos();
        titleText.text = card.cityCard.cityName;
        descriptionText.text = "cardDescription";
        panel.SetActive(true);
    }

    public void ShowInfoWhenCityPressed(CityData city, Player player)
    {
        GameObject cardObj;
        PopUpButton newButton;

        if (false)///kkad jauztaisa xd
        {
            cardObj = Instantiate(button, popUp.transform);
            newButton = cardObj.GetComponentInChildren<PopUpButton>();
            newButton.Init("cureDesise", city);
            buttonList.Add(cardObj);
        }

        if (city.hasResearchStation())
        {
            cardObj = Instantiate(button, popUp.transform);
            newButton = cardObj.GetComponentInChildren<PopUpButton>();
            newButton.Init("flyToResearchStation", city);
            buttonList.Add(cardObj);
        }

        int cubs = city.getCubs();
        for (int i = 0; i < cubs; i++)
        {
            cardObj = Instantiate(button, popUp.transform);
            newButton = cardObj.GetComponentInChildren<PopUpButton>();
            newButton.Init("Remove " + i + "cubs", city);
            buttonList.Add(cardObj);
        }
        
        buttonPos();
        titleText.text ="Ko darit "+ city.cityName;
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

        foreach (GameObject obj in buttonList)
        {
            Destroy(obj);
        }
        buttonList.Clear();
    }
}

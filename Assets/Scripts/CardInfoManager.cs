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

    public static CardInfoManager Instance;

    private void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public void ShowInfo(CityData city, Player player)
    {
        if (true)
        {
            GameObject cardObj = Instantiate(button, popUp.transform);
            popUpButton newCard = cardObj.GetComponentInChildren<popUpButton>();
            newCard.Init("flyTo");
        }
        titleText.text = city.cityName;
        descriptionText.text = "cardDescription";
        panel.SetActive(true);
    }

    public void hideInfo()
    {
        panel.SetActive(false);
    }
}

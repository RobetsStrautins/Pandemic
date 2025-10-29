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

    private List<GameObject> buttonList = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public void ShowInfo(PlayerCard card, Player player)
    {
        if (player.city == card.cityCard)
        {
            GameObject cardObj = Instantiate(button, popUp.transform);
            popUpButton newCard = cardObj.GetComponentInChildren<popUpButton>();
            newCard.Init("flyAnywhere", card);
            buttonList.Add(cardObj);
        }
        else
        {
            GameObject cardObj = Instantiate(button, popUp.transform);
            popUpButton newCard = cardObj.GetComponentInChildren<popUpButton>();
            newCard.Init("flyTo", card);
            buttonList.Add(cardObj);
        }

        titleText.text = card.cityCard.cityName;
        descriptionText.text = "cardDescription";
        panel.SetActive(true);
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

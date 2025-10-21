using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardInfoManager : MonoBehaviour
{
    public GameObject panel;
    public TMP_Text titleText;
    public TMP_Text descriptionText;

    public static CardInfoManager Instance;

    private void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public void ShowInfo(string title, string description)
    {
        panel.SetActive(true);
        titleText.text = title;
        descriptionText.text = description;
    }

    public void HideInfo()
    {
        panel.SetActive(false);
    }
}

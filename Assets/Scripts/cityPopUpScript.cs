using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CityPopUpScript : MonoBehaviour
{
    public GameObject panel;
    public TMP_Text titleText;
    public Transform popUp;
    public GameObject button;

    public static CityPopUpScript Instance;

    private void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public void hideInfo()
    {
        panel.SetActive(false);
    }

}

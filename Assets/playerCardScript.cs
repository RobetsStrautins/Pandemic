using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCard : MonoBehaviour
{
    public CityData cityCard;
    public CardNode myNode;
    public TextMeshProUGUI cityLabel;
    public GameObject cardBackgroundColor;
    
    public void Init(CityData data)
    {
        cityCard = data;

        name = "Card " + data.cityName;

        //cardBackgroundColor.GetComponent<SpriteRenderer>().color = data.color;
        if (cityLabel != null)
            cityLabel.text = data.cityName;
    } 

    public void OnMouseDown()
    {
        //...
    }
}

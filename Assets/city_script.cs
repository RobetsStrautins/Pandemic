using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class city : MonoBehaviour
{
    CityData cityData = new CityData();
    public TextMeshProUGUI cityLabel;

    public void Init(CityData data)
    {
        cityData.id = data.id;
        cityData.cityName = data.cityName;
        cityData.Xcord = data.Xcord;
        cityData.Ycord = data.Ycord;
        cityData.color = data.color;

        transform.position = new Vector3(cityData.Xcord, cityData.Ycord);
        gameObject.name = "city " + cityData.cityName;

        if (cityLabel != null)
            cityLabel.text = cityData.cityName;
    }
}

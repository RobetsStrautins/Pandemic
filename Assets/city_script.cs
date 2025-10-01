using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class city : MonoBehaviour
{
    public int id;
    public string cityName = "London";
    public TextMeshProUGUI cityLabel;
    public float Xcord;
    public float Ycord;
    public string color;
    public List<city> connectedCities;
    void Start()
    {
        cityLabel.text = cityName;
    }
    
}

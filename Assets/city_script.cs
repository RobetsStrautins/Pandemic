using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.MemoryProfiler;
public class city : MonoBehaviour
{
    [SerializeField] private CityData cityData = new CityData();
    public TextMeshProUGUI cityLabel;

    public void Init(CityData data)
    {
        cityData=data;///Savieno cityData ar city
        cityData.cityObj = this;  

        transform.position = new Vector3(cityData.Xcord, cityData.Ycord);
        gameObject.name = "city " + cityData.cityName;

        if (cityLabel != null)
            cityLabel.text = cityData.cityName;
    }

    void OnMouseDown()
    {
        Debug.Log("City clicked: " + gameObject.name);

        mainscript.main.activePlayerMoveCitys(cityData);
    }
}

[System.Serializable]
public class CityData
{
    public int id;
    public string cityName;
    public float Xcord;
    public float Ycord;
    public string color;
    public List<int> connectedcity = new List<int>();
    private int cubs = 0;

    public city cityObj;

    public void addCubs(int newCubs)
    {
        if (cubs + newCubs > 3)
        {
            cubs = 3;
        }
        else
        {
            cubs += newCubs;
        }
    }

    public int getCubs()
    {
        return cubs;
    }
}
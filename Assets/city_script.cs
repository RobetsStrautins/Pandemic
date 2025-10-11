using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.MemoryProfiler;
public class City : MonoBehaviour
{
    [SerializeField] private CityData cityData = new CityData();
    public TextMeshProUGUI cityLabel;
    public GameObject[] resourceCubes;

    public void Init(CityData data)
    {
        cityData = data;///Savieno cityData ar city
        cityData.cityObj = this;

        transform.position = new Vector3(cityData.Xcord, cityData.Ycord);
        gameObject.name = "city " + cityData.cityName;

        if (cityLabel != null)
            cityLabel.text = cityData.cityName;

        updateCubs();
    }

    public void updateCubs()
    {
        int cubs = cityData.getCubs();

        for (int i = 0; i < resourceCubes.Length; i++)
        {
            resourceCubes[i].SetActive(i < cubs);
        }
    }

    void OnMouseDown()
    {
        Mainscript.main.activePlayerMoveCitys(cityData);
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
    public List<int> connectedCity = new List<int>();
    private int cubs = 0;

    public City cityObj;

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
        cityObj?.updateCubs();
    }

    public bool removeCubs(int removeCubs)
    {
        if (cubs == 0)
        {
            return false;
        }
        cubs -= removeCubs;  
        cityObj?.updateCubs();
        return true;
    }
    public int getCubs()
    {
        return cubs;
    }
}
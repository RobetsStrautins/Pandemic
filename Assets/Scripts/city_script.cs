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

        cityData.unityColor = stringToColor(cityData.color);
        
        updateCubs();
        setUpcolor();
        
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

    private void setUpcolor() 
    {
        Transform point = transform.Find("Point");
        SpriteRenderer pointRender = point.GetComponent<SpriteRenderer>();

        pointRender.color = cityData.unityColor;
        cubeColor(cityData.unityColor);
    }
    private Color stringToColor(string strin)
    {
        string str = strin.ToLower();
        switch (str)
        {
            case "blue":
                return new Color(0f, 0f, 1f, 1f);
            case "red":
                return new Color(1f, 0f, 0f, 1f);
            case "yellow":
                return new Color(1f, 47f / 51f, 0.015686275f, 1f);
            case "black":
                return new Color(0f, 0f, 0f, 1f);
        }
        Debug.Log("nav krasa" + str);
        return new Color(1f, 1f, 1f, 1f) ;
    }
        
    private void cubeColor(Color colorValue)
    {
        for(int i=1; i<4;i++)
        {
            Transform cube = transform.Find("Cube " + i);
            Renderer cubeRenderer = cube.GetComponent<Renderer>();
            cubeRenderer.material.color = colorValue;
        }
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
    public Color unityColor;
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
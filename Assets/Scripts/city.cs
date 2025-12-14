using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class City : MonoBehaviour
{
    [SerializeField] private CityData cityData = new CityData();
    public Text cityLabel;
    public GameObject[] resourceCubes;
    public GameObject rStation;

    public void Init(CityData data)
    {
        cityData = data;///Savieno cityData ar city
        cityData.cityObj = this;

        transform.position = new Vector3(cityData.Xcord, cityData.Ycord);
        gameObject.name = "city " + cityData.cityName;

        if (cityLabel != null)
        {
            cityLabel.text = cityData.cityName;
        }

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
        if (Mainscript.main.inMiddleOfAcion() && !Mainscript.main.waitingForCityClick)
        {
            return;
        }
        
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
        return new Color(1f, 1f, 1f, 1f);
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
    private bool researchStation = false;

    public City cityObj;

    public void addCubs(int newCubs)
    {
        if(!DiseaseMarkers.Instance.diseaseColorDict[color].isExtinctDisease)
        {
            Debug.Log($"added {newCubs} cube to {cityName}");
            if (cubs + newCubs > 3)
            {
                DiseaseMarkers.Instance.addColorCubes(color, 3 - cubs);
                cubs = 3;
                DiseaseDeck.Instance.outBreak(this);
            }
            else
            {
                DiseaseMarkers.Instance.addColorCubes(color, newCubs);
                cubs += newCubs;
            }
            cityObj?.updateCubs();
        }
    }

    public void removeCubs(int removeCubs)
    {
        cubs -= removeCubs;
        DiseaseMarkers.Instance.cubeColorCount[color] -= removeCubs;
        cityObj?.updateCubs();
    }
    
    public int getCubs()
    {
        return cubs;
    }

    public void buildResearchStation()
    {
        cityObj.rStation.SetActive(true);
        researchStation = true;
        Mainscript.main.playerTurnCount -= 1;
        Mainscript.main.researchStationOnMap.Add(this);
        Mainscript.main.updateMoveCount();
    }

    public bool hasResearchStation()
    {
        return researchStation;
    }
}
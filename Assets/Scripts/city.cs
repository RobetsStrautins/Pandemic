using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityUi : MonoBehaviour
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

        updateCubes();
        setUpColor();
    }   

    public void updateCubes()
    {
        int cubes = cityData.getCubes();

        for (int i = 0; i < resourceCubes.Length; i++)
        {
            resourceCubes[i].SetActive(i < cubes);
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

    private void setUpColor() 
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
    private int cubes = 0;
    private bool researchStation = false;

    public bool cityIsUnderQuarantine = false;
    public CityUi cityObj;

    public void addCubes(int newCubes)
    {
        if(DiseaseMarkers.Instance?.getDiseaseProgress(color) != DiseaseColorProgress.DiseaseEradicated && !cityIsUnderQuarantine)
        {
            Debug.Log($"added {newCubes} cube to {cityName}");
            if (cubes + newCubes > 3)
            {
                DiseaseMarkers.Instance?.chengeColorCubes(color, 3 - cubes);
                cubes = 3;
                DiseaseDeck.outBreak(this);
            }
            else
            {
                DiseaseMarkers.Instance?.chengeColorCubes(color, newCubes);
                cubes += newCubes;
            }
            cityObj?.updateCubes();
        }
    }

    public void removeCubes(int removeCubes)
    {
        cubes -= removeCubes;
        DiseaseMarkers.Instance.chengeColorCubes(color, cubes);
        cityObj?.updateCubes();
    }
    
    public int getCubes()
    {
        return cubes;
    }

    public void buildResearchStation()
    {
        cityObj.rStation.SetActive(true);
        researchStation = true;
        Mainscript.main.playerTurnCount -= 1;
        Mainscript.main.researchStationOnMap.Add(this);
        GameUI.Instance.updateMoveCount();
        PopUpButtonManager.Instance.hideInfo();
    }

    public bool hasResearchStation()
    {
        return researchStation;
    }
}
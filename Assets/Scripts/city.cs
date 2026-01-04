using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityUi : MonoBehaviour
{
    [SerializeField] private CityData cityData;
    public Text cityLabel;
    public GameObject[] resourceCubes;
    public GameObject researchStation;

    public void Init(CityData data) //inicializē city objektu
    {
        cityData = data; //Savieno cityData ar city
        cityData.cityObj = this;

        transform.position = new Vector3(cityData.Xcord, cityData.Ycord);
        gameObject.name = "city " + cityData.cityName;

        if (cityLabel != null)
        {
            cityLabel.text = cityData.cityName;
        }

        cityData.unityColor = Mainscript.main.stringToColor(cityData.color);

        updateCubes();
        setUpColor();
    }   

    public void updateCubes() //atjaunina infekcijas kubiciņus
    {
        int cubes = cityData.getCubes();

        for (int i = 0; i < resourceCubes.Length; i++)
        {
            resourceCubes[i].SetActive(i < cubes);
        }
    }

    void OnMouseDown() //kad noklikšķina uz city
    {
        if (Mainscript.main.inMiddleOfAcion() && !Mainscript.main.waitingForCityClick)
        {
            return;
        }
        
        Mainscript.main.activePlayerMoveCitys(cityData);
    }

    private void setUpColor() //iestata krāsu
    {
        Transform point = transform.Find("Point");
        SpriteRenderer pointRender = point.GetComponent<SpriteRenderer>();

        pointRender.color = cityData.unityColor;
        cubeColor(cityData.unityColor);
    }
        
    private void cubeColor(Color colorValue) //iestata kubiciņu krāsu
    {
        for(int i = 1; i < 4; i++)
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

    public void addCubes(int newCubes) //pievieno kubiciņus pilsētai
    {
        if(DiseaseMarkers.Instance.getDiseaseProgress(color) != DiseaseColorProgress.Eradicated )
        {
            if(!cityIsUnderQuarantine)
            {
                Debug.Log($"added {newCubes} cube to {cityName}");
                if (cubes + newCubes > 3)
                {
                    DiseaseMarkers.Instance.chengeColorCubes(color, 3 - cubes);
                    cubes = 3;
                    DiseaseDeck.outBreak(this);
                }
                else
                {
                    DiseaseMarkers.Instance.chengeColorCubes(color, newCubes);
                    cubes += newCubes;
                }
                cityObj?.updateCubes(); 
            }
        }
        else
        {
            ActionLog.Instance.addEntry("Karantīnas speciālists apstadināja izplatību " + cityName);
        }

    }

    public void removeCubes(int removeCubes) //noņem kubiciņus no pilsētas
    {
        cubes -= removeCubes;
        DiseaseMarkers.Instance.chengeColorCubes(color, cubes);
        cityObj?.updateCubes();
        ActionLog.Instance.addEntry(removeCubes + " kubiciņi noņemti no " + cityName);
    }
    
    public int getCubes() //atgriež kubiciņu skaitu pilsētā
    {
        return cubes;
    }

    public void buildResearchStation() //uzbūvē izpētes staciju
    {
        cityObj.researchStation.SetActive(true);
        researchStation = true;
        GameUI.Instance.usedAction();
        Mainscript.main.researchStationOnMap.Add(this);
        PopUpButtonManager.Instance.hideInfo();
        ActionLog.Instance.addEntry("Izpētes stacija uzbūvēta " + cityName);
    }

    public bool hasResearchStation() //pārbauda vai ir izpētes stacija
    {
        return researchStation;
    }
}
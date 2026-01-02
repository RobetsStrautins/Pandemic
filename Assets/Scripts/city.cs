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

        cityData.unityColor = Mainscript.main.stringToColor(cityData.color);

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
        
    private void cubeColor(Color colorValue)
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
        cityObj.researchStation.SetActive(true);
        researchStation = true;
        GameUI.Instance.usedAction();
        Mainscript.main.researchStationOnMap.Add(this);
        PopUpButtonManager.Instance.hideInfo();
    }

    public bool hasResearchStation()
    {
        return researchStation;
    }
}
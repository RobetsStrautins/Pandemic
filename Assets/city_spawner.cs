using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitySpawner : MonoBehaviour
{
    public GameObject city;

    void Start()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("cities");
        CityDataList cityList = JsonUtility.FromJson<CityDataList>(jsonFile.text);

        foreach (CityData data in cityList.cities)
        {
            GameObject cityObj = Instantiate(city);
            cityObj.GetComponent<city>().Init(data);
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
}

[System.Serializable]
public class CityDataList
{
    public CityData[] cities;
}
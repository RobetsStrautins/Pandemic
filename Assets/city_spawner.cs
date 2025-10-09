using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitySpawner : MonoBehaviour
{
    public GameObject city;
    public GameObject connectionParent;
    public static Dictionary<int, CityData> cityMap = new Dictionary<int, CityData>();//c++ map

    public void Setup()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("cities");//dabut pilsetas
        WorldData world = JsonUtility.FromJson<WorldData>(jsonFile.text);

        foreach (CityData data in world.cities)
        {
            cityMap[data.id] = data;
        }

        foreach (IntPair temp in world.connections)
        {
            if (cityMap.ContainsKey(temp.first) && cityMap.ContainsKey(temp.second))
            {
                CityData city1 = cityMap[temp.first];
                CityData city2 = cityMap[temp.second];

                if (!city1.connectedCity.Contains(temp.second))
                {
                    city2.connectedCity.Add(temp.first);
                    city1.connectedCity.Add(temp.second);
                }
            }
        }

        foreach (CityData data in world.cities)
        {
            GameObject cityObj = Instantiate(city);
            cityObj.GetComponent<City>().Init(data);
        }

        connectionParent = new GameObject("Connections");
        foreach (IntPair temp in world.connections)
        {
            CityData city1 = cityMap[temp.first];
            CityData city2 = cityMap[temp.second];

            Vector3 city1Cords = new Vector3(city1.Xcord, city1.Ycord, 20);
            Vector3 city2Cords = new Vector3(city2.Xcord, city2.Ycord, 20);
            DrawConnection(city1Cords, city2Cords);
        }
    }

    void DrawConnection(Vector3 start, Vector3 end)
    {
        GameObject lineObj = new GameObject("Connection");
        lineObj.transform.parent = connectionParent.transform; 

        LineRenderer lineRenderer = lineObj.AddComponent<LineRenderer>();

        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.useWorldSpace = true;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.black;
    }
}

[System.Serializable]
public class WorldData
{
    public CityData[] cities;
    public IntPair[] connections;
}

[System.Serializable]
public class IntPair
{
    public int first;
    public int second;
}


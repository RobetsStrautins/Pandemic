using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitySpawner : MonoBehaviour
{
    public GameObject city;
    public GameObject connectionParent;
    public GameObject CityParent;
    public static Dictionary<int, CityData> cityMap = new Dictionary<int, CityData>();//c++ map

    public void Setup()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("cities");//dabut pilsetas
        WorldData world = JsonUtility.FromJson<WorldData>(jsonFile.text);

        foreach (CityData data in world.cities)
        {
            cityMap[data.id] = data;
        }

        foreach (IntPair temp in world.connections)//savieno pilsetas
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

        CityParent = new GameObject("Cities");
        foreach (CityData data in world.cities)// uztaiasa pilsetas ui
        {
            GameObject cityObj = Instantiate(city);
            cityObj.transform.parent = CityParent.transform;
            cityObj.GetComponent<CityUi>().Init(data);
        }

        connectionParent = new GameObject("Connections");
        Vector3 city1Cords;
        Vector3 city2Cords;
        foreach (IntPair temp in world.connections)// uztaisa ui connections
        {
            CityData city1 = cityMap[temp.first];
            CityData city2 = cityMap[temp.second];
            GameObject lineObj = new GameObject("Connection " + city1.id + " " + city2.id);

            if((city1.id == 1 && city2.id == 40)||(city1.id == 1 && city2.id == 46)||(city1.id == 13 && city2.id == 48))
            {
                city1Cords = new Vector3(city1.Xcord, city1.Ycord, 20);
                city2Cords = new Vector3(-11, city2.Ycord, 20);
                DrawConnection(city1Cords, city2Cords, lineObj);

                GameObject lineObj2 = new GameObject("Connection " + city1.id + " " + city2.id); 
                city1Cords = new Vector3(10, city1.Ycord, 20);
                city2Cords = new Vector3(city2.Xcord, city2.Ycord, 20);
                DrawConnection(city1Cords, city2Cords, lineObj2);
                
                continue;
            }
            
            city1Cords = new Vector3(city1.Xcord, city1.Ycord, 20);
            city2Cords = new Vector3(city2.Xcord, city2.Ycord, 20);
            DrawConnection(city1Cords, city2Cords, lineObj);
        }
    }

    void DrawConnection(Vector3 start, Vector3 end, GameObject lineObj)
    {
        lineObj.transform.parent = connectionParent.transform;

        LineRenderer lineRenderer = lineObj.AddComponent<LineRenderer>();

        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
        lineRenderer.startWidth = 0.03f;
        lineRenderer.endWidth = 0.03f;
        lineRenderer.useWorldSpace = true;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.yellow;
        lineRenderer.endColor = Color.yellow;
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


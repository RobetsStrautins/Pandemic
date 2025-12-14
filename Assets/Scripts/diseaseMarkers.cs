using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiseaseMarkers : MonoBehaviour
{
    public static DiseaseMarkers Instance;

    public GameObject Disease;

    private string[] colorList = { "Yellow", "Red", "Blue", "Black" };

    public Dictionary<string, DiseaseColor> diseaseColorDict = new Dictionary<string, DiseaseColor>();

    public Dictionary<string, int> cubeColorCount = new Dictionary<string,int>
    {
        { "Yellow", 0 },
        { "Red", 0 },
        { "Blue", 0 },
        { "Black", 0 }
    };

    public void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        for (int i = 0; i < colorList.Length; i++)
        {
            GameObject newDisease = Instantiate(Disease, transform);

            DiseaseColor colorScript = newDisease.GetComponent<DiseaseColor>();
            colorScript.Init(colorList[i]);

            Vector3 spawnPos = transform.position + new Vector3(0, -i * 0.7f, 0);
            newDisease.transform.position = spawnPos;

            diseaseColorDict[colorList[i]] = colorScript;

        }
    }

    public void checkForExtinctDisease(string color)
    {
        if(cubeColorCount[color] == 0)
        {
            diseaseColorDict[color].extinctDisease();
        }
    }

    public void addColorCubes(string color, int cubCount)
    {
        cubeColorCount[color] += cubCount;

        if (cubeColorCount[color] > 24)
        {
            Debug.LogWarning("Lost, " + color + " cubs receh over 24");
            GameEnd.Instance.GameLost();
        }

    }
}

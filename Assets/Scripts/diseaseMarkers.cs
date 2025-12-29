using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiseaseMarkers : MonoBehaviour
{
    public static DiseaseMarkers Instance;

    public GameObject Disease;

    private string[] colorList = { "Yellow", "Red", "Blue", "Black" };

    public static Dictionary<string, DiseaseColorMarker> diseaseColorDict = new Dictionary<string, DiseaseColorMarker>();

    private static Dictionary<string, int> cubeColorCount = new Dictionary<string,int>
    {
        { "Yellow", 0 },
        { "Red", 0 },
        { "Blue", 0 },
        { "Black", 0 }
    };

    private static Dictionary<string, DiseaseColorProgress> diseaseProgress = new Dictionary<string, DiseaseColorProgress>
    {
        { "Yellow", DiseaseColorProgress.NotCured },
        { "Red", DiseaseColorProgress.NotCured },
        { "Blue", DiseaseColorProgress.NotCured },
        { "Black", DiseaseColorProgress.NotCured }
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

            DiseaseColorMarker colorScript = newDisease.GetComponent<DiseaseColorMarker>();
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

    public void chengeColorCubes(string color, int cubCount)
    {
        cubeColorCount[color] += cubCount;

        if (cubeColorCount[color] > 24)
        {
            Debug.LogWarning("Lost, " + color + " cubs receh over 24");
            GameUI.Instance.gameLost(color + " kubicini parsniedza 24");
        }
    }

    public void updateDiseaseProgress(string color, DiseaseColorProgress progress)
    {
        diseaseProgress[color] = progress;
    }

    public DiseaseColorProgress getDiseaseProgress(string color)
    {
        return diseaseProgress[color];
    }

    public IEnumerable<DiseaseColorProgress> getAllDiseaseProgress()
    {
        return diseaseProgress.Values;
    }
}

public enum DiseaseColorProgress
{
    NotCured,
    Cured,
    DiseaseEradicated
}

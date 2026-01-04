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

    public void Awake() //konstruktors
    {
        Instance = this;
    }

    void Start() //inicializācija
    {

        diseaseColorDict.Clear();

        cubeColorCount = new Dictionary<string, int>
        {
            { "Yellow", 0 },
            { "Red", 0 },
            { "Blue", 0 },
            { "Black", 0 }
        };

        diseaseProgress = new Dictionary<string, DiseaseColorProgress>
        {
            { "Yellow", DiseaseColorProgress.NotCured },
            { "Red", DiseaseColorProgress.NotCured },
            { "Blue", DiseaseColorProgress.NotCured },
            { "Black", DiseaseColorProgress.NotCured }
        };

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

    public void checkForExtinctDisease(string color) //pārbauda vai slimība ir iznīcināta
    {
        if(cubeColorCount[color] == 0)
        {
            diseaseColorDict[color].extinctDisease();
        }
    }

    public void chengeColorCubes(string color, int cubCount) //maina kubiciņu skaitu
    {
        cubeColorCount[color] += cubCount;
        diseaseColorDict[color].text.text = cubeColorCount[color].ToString();

        if (cubeColorCount[color] > 24)
        {
            GameUI.Instance.gameLost(color + " kubicini parsniedza 24");
        }
    }

    public void updateDiseaseProgress(string color, DiseaseColorProgress progress) //atjauno slimības progresu
    {
        diseaseProgress[color] = progress;
        ActionLog.Instance.addEntry(color + " slimibas progress ir tagad ir " + progress.ToString().ToLower());
    }

    public DiseaseColorProgress getDiseaseProgress(string color) //atgriež slimības progresu
    {
        return diseaseProgress[color];
    }

    public IEnumerable<DiseaseColorProgress> getAllDiseaseProgress() //atgriež visu slimību progresu
    {
        return diseaseProgress.Values;
    }
}

public enum DiseaseColorProgress
{
    NotCured,
    Cured,
    Eradicated
}

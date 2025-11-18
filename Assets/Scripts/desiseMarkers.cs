using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesiseMarkers : MonoBehaviour
{
    public static DesiseMarkers Instance;

    public GameObject Desise;

    private string[] colorList = { "Yellow", "Red", "Blue", "Black" };

    public Dictionary<string, DesiseColor> desiseColorDict = new Dictionary<string, DesiseColor>();

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
            GameObject newDesise = Instantiate(Desise, transform);

            DesiseColor colorScript = newDesise.GetComponent<DesiseColor>();
            colorScript.Init(colorList[i]);

            Vector3 spawnPos = transform.position + new Vector3(0, -i * 0.7f, 0);
            newDesise.transform.position = spawnPos;

            desiseColorDict[colorList[i]] = colorScript;

        }
    }

    public void checkForExtinctDisease(string color)
    {
        if(cubeColorCount[color] == 0)
        {
            desiseColorDict[color].extinctDisease();
        }
    }
}

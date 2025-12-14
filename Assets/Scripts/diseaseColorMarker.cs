using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiseaseColorMarker : MonoBehaviour
{
    public SpriteRenderer cure;
    public SpriteRenderer cureSilutet;

    public GameObject cross;

    public bool isCuredDisease = false;
    public bool isExtinctDisease = false;

    public void Init(string cureColor)
    {
        name = cureColor + " marker";
        cure.color = stringToColor(cureColor);

        Color siluetColor = stringToColor(cureColor);
        siluetColor.a = 100f / 255f;
        cureSilutet.color = siluetColor;
        cross.SetActive(false);

    }

    public void curedDisease()
    {
        Color siluetColor = cureSilutet.color;
        siluetColor.a = 1;
        cureSilutet.color = siluetColor;

        cure.gameObject.SetActive(false);
        isCuredDisease = true;
    }

    public void extinctDisease()
    {
        cross.SetActive(true);
        isExtinctDisease = true;
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
}

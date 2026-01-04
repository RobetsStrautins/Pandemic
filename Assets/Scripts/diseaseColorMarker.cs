using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DiseaseColorMarker : MonoBehaviour
{
    public SpriteRenderer cure;
    public SpriteRenderer cureSilutet;
    public GameObject cross;

    public Text text;
    private string diseaseColor;

    public void Init(string cureColor) //konstruktors
    {
        name = cureColor + " marker";
        diseaseColor = cureColor;
        cure.color = Mainscript.main.stringToColor(cureColor);

        Color siluetColor = Mainscript.main.stringToColor(cureColor);
        siluetColor.a = 100f / 255f;
        cureSilutet.color = siluetColor;
        cross.SetActive(false);
    }

    public void curedDisease() //izsauc kad izārstē slimību
    {
        Color siluetColor = cureSilutet.color;
        siluetColor.a = 1;
        cureSilutet.color = siluetColor;

        cure.gameObject.SetActive(false);

        DiseaseMarkers.Instance.updateDiseaseProgress(diseaseColor, DiseaseColorProgress.Cured);

        GameUI.Instance.checkIfGameWon();
    }

    public void extinctDisease() //izsauc kad slimība ir iznīcināta
    {
        text.text = " ";
        cross.SetActive(true);
        DiseaseMarkers.Instance.updateDiseaseProgress(diseaseColor, DiseaseColorProgress.Eradicated);
    }
}

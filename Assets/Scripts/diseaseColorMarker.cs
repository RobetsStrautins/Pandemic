using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiseaseColorMarker : MonoBehaviour
{
    public SpriteRenderer cure;
    public SpriteRenderer cureSilutet;
    public GameObject cross;

    private string diseaseColor;

    public void Init(string cureColor)
    {
        name = cureColor + " marker";
        diseaseColor = cureColor;
        cure.color = Mainscript.main.stringToColor(cureColor);

        Color siluetColor = Mainscript.main.stringToColor(cureColor);
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

        DiseaseMarkers.Instance.updateDiseaseProgress(diseaseColor, DiseaseColorProgress.Cured);

        GameUI.Instance.checkIfGameWon();
    }

    public void extinctDisease()
    {
        cross.SetActive(true);
        DiseaseMarkers.Instance.updateDiseaseProgress(diseaseColor, DiseaseColorProgress.Eradicated);
    }
}

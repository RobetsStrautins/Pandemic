using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Diagnostics;

public class PopUpButtonAction : MonoBehaviour
{
    public Text buttonText;
    public Button onClickButton;

    public void Init(string text, Action onClick, bool interactable) //inicializē pop-up loga pogu
    {
        name = text + " button";
        buttonText.text = text;
        onClickButton.onClick.RemoveAllListeners();
        onClickButton.onClick.AddListener(() => onClick.Invoke());
        onClickButton.interactable = interactable;
    }
}

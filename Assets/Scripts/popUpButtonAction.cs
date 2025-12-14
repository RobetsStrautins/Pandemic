using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Diagnostics;

public class PopUpButtonAction : MonoBehaviour
{
    public Text buttonText;
    public Button button;

    public void Init(string text, Action onClick, bool interactable)
    {
        name = text + " button";
        buttonText.text = text;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => onClick.Invoke());
        button.interactable = interactable;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionLog : MonoBehaviour
{
    public static ActionLog Instance;

    public Text actionLogText;
    private int maxEntries = 100; 
    private Queue<string> entries = new Queue<string>();

    public ScrollRect scrollRect;

    void Awake() //konstruktors
    {
        Instance = this;
    }

    public void addEntry(string message) //pievieno ierakstu bez krāsas
    {
        addEntry(message, Color.black);
    }

    public void addEntry(string message, Color color) //pievieno ierakstu ar krāsu
    {
        string colored = $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{message}</color>";
        entries.Enqueue(colored);

        if (entries.Count > maxEntries)
        {
            entries.Dequeue();
        }

        refreshLog();
    }

    private void refreshLog() //atjaunina sarkastu
    {
        actionLogText.text = string.Join("\n", entries);
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }
}

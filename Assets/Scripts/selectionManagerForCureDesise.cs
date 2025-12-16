using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance;
    private int maxSelections = 5;

    public static List<PlayerCityCardInPopUp> selectedCards = new List<PlayerCityCardInPopUp>();

    private void Awake()
    {
        Instance = this;
    }

    public void SelectCard(PlayerCityCardInPopUp card)
    {
        if (!selectedCards.Contains(card))
        {
            selectedCards.Add(card);
        }
    }

    public void DeselectCard(PlayerCityCardInPopUp card)
    {
        selectedCards.Remove(card);
    }

    public bool CanSelectMore()
    {
        return selectedCards.Count < maxSelections;
    }
}

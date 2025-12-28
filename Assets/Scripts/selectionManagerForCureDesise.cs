using System.Collections.Generic;
using UnityEngine;

public static class SelectionManager 
{
    private static int maxSelections = 5;

    public static List<PlayerCityCardInPopUp> selectedCards = new List<PlayerCityCardInPopUp>();

    public static void SelectCard(PlayerCityCardInPopUp card)
    {
        if (!selectedCards.Contains(card))
        {
            selectedCards.Add(card);
        }
    }

    public static void DeselectCard(PlayerCityCardInPopUp card)
    {
        selectedCards.Remove(card);
    }

    public static bool CanSelectMore()
    {
        return selectedCards.Count < maxSelections;
    }
}

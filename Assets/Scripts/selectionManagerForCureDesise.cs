using System.Collections.Generic;
using UnityEngine;

public static class SelectionManager 
{
    private static int maxSelections = 5;

    public static List<PlayerCityCardInPopUp> selectedCards = new List<PlayerCityCardInPopUp>();

    public static void selectCard(PlayerCityCardInPopUp card)
    {
        if (!selectedCards.Contains(card))
        {
            selectedCards.Add(card);
        }
    }

    public static void deselectCard(PlayerCityCardInPopUp card)
    {
        selectedCards.Remove(card);
    }

    public static bool canSelectMore()
    {
        return selectedCards.Count < maxSelections;
    }
}

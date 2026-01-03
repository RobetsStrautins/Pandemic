using System.Collections.Generic;
using UnityEngine;

public static class SelectionManager 
{
    private static int maxSelections = 5;

    public static List<PlayerCityCardInPopUp> selectedCards = new List<PlayerCityCardInPopUp>();

    public static void selectCard(PlayerCityCardInPopUp card) //pievieno izveleto kārti
    {
        if (!selectedCards.Contains(card))
        {
            selectedCards.Add(card);
        }
    }

    public static void deselectCard(PlayerCityCardInPopUp card) //noņem izvēlēto kārti
    {
        selectedCards.Remove(card);
    }

    public static bool canSelectMore() //atgriež vai var vel pievienot
    {
        return selectedCards.Count < maxSelections;
    }
}

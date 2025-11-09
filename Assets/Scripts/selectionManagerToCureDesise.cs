using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance;
    public int maxSelections = 5;

    private List<PlayerCardInPopUp> selectedCards = new List<PlayerCardInPopUp>();

    private void Awake()
    {
        Instance = this;
    }

    public void SelectCard(PlayerCardInPopUp card)
    {
        if (!selectedCards.Contains(card))
            selectedCards.Add(card);
    }

    public void DeselectCard(PlayerCardInPopUp card)
    {
        selectedCards.Remove(card);
    }

    public bool CanSelectMore()
    {
        return selectedCards.Count < maxSelections;
    }

    public void ConfirmSelection()
    {
        Debug.Log("Selected cards:");
        foreach (var card in selectedCards)
        {
            Debug.Log(card.myNode.cityCard.cityName);
        }

        // Here you can trigger your "keep these cards" logic
    }
}

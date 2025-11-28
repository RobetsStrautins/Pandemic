using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance;
    public int maxSelections = 5;

    public List<PlayerCardInPopUp> selectedCards = new List<PlayerCardInPopUp>();

    private void Awake()
    {
        Instance = this;
    }

    public void SelectCard(PlayerCardInPopUp card)
    {
        if (!selectedCards.Contains(card))
        {
            selectedCards.Add(card);
        }
    }

    public void DeselectCard(PlayerCardInPopUp card)
    {
        selectedCards.Remove(card);
    }

    public bool CanSelectMore()
    {
        return selectedCards.Count < maxSelections;
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCardInPopUp : MonoBehaviour
{
    public CardNode myNode;
    public Text cityLabel;
    public Image cardBackgroundColor;

    private bool isSelected = false;
    private Color baseColor;
    private Color selectedColor;

    public void Init(CardNode cardNode)
    {
        myNode = cardNode;
        name = "Card " + myNode.cityCard.cityName;

        cardBackgroundColor.color = myNode.cityCard.unityColor;
        baseColor = myNode.cityCard.unityColor;
        selectedColor = baseColor * 0.7f; // Darker version for selection

        if (cityLabel != null)
            cityLabel.text = myNode.cityCard.cityName;
    }

    // Called when the button is clicked
    public void OnCardClicked()
    {
        if (SelectionManager.Instance == null) return;

        if (!isSelected && SelectionManager.Instance.CanSelectMore())
        {
            isSelected = true;
            SelectionManager.Instance.SelectCard(this);
            Highlight(true);
        }
        else if (isSelected)
        {
            isSelected = false;
            SelectionManager.Instance.DeselectCard(this);
            Highlight(false);
        }
    }

    private void Highlight(bool enable)
    {
        cardBackgroundColor.color = enable ? selectedColor : baseColor;
    }

    public bool IsSelected => isSelected;
}

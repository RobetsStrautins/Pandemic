using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConfirmButton : MonoBehaviour
{
    public void onConfirmClicked()
    {
        if (SelectionManager.selectedCards.Count == 5)
        {
            string color = SelectionManager.selectedCards[0].cardsCityData.color;

            if (DiseaseMarkers.diseaseColorDict.ContainsKey(color))
            {
                DiseaseMarkers.diseaseColorDict[color].curedDisease();
                DiseaseMarkers.Instance.checkForExtinctDisease(color);
            }

            Player player = Mainscript.main.getActivePlayer();

            foreach (var card in SelectionManager.selectedCards)
            {
                player.playerCardList.removeCard(card.cardData);
            }

            PlayerHandUi.Instance.renderHand(player);

            Mainscript.main.playerTurnCount -= 1;
            GameUI.Instance.updateMoveCount();

            PopUpCardManager.Instance.hideInfo();

            GameUI.Instance.checkIfGameWon();
        }
        else
        {
            Debug.LogWarning("Need to select 5 cards");
        }
    }
}

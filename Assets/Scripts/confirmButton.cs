using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConfirmButton : MonoBehaviour
{
    public void onConfirmClicked()
    {
        int neededCardCount = 5;

        Player player = Mainscript.main.getActivePlayer();
        if (player.playerRole == PlayerRole.Scientist)
        {
            neededCardCount = 4;
        }

        if (SelectionManager.selectedCards.Count == neededCardCount)
        {
            string color = SelectionManager.selectedCards[0].cardsCityData.color;

            if (DiseaseMarkers.diseaseColorDict.ContainsKey(color))
            {
                DiseaseMarkers.diseaseColorDict[color].curedDisease();
                DiseaseMarkers.Instance.checkForExtinctDisease(color);
            }

            foreach (var card in SelectionManager.selectedCards)
            {
                player.playerCardList.removeCard(card.cardData);
            }

            PlayerHandUi.Instance.renderHand(player);

            Mainscript.main.playerTurnCount -= 1;
            GameUI.Instance.updateMoveCount();

            PopUpCardManager.Instance.hideInfo();

            foreach (var playerInList in Mainscript.main.playersList)
            {
                playerInList.removeCubsFromCurrentCity();
            }
        }
        else
        {
            Debug.LogWarning("Need to select 5 cards or 4 if Scientist to cure disease");
        }
    }
}

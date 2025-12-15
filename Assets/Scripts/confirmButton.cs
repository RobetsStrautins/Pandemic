using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConfirmButton : MonoBehaviour
{
    public void onConfirmClicked()
    {
        if (SelectionManager.Instance.selectedCards.Count == 5)
        {
            string color = SelectionManager.Instance.selectedCards[0].cardsCityData.color;

            if (DiseaseMarkers.Instance.diseaseColorDict.ContainsKey(color))
            {
                DiseaseMarkers.Instance.diseaseColorDict[color].curedDisease();
                DiseaseMarkers.Instance.checkForExtinctDisease(color);
            }

            Player player = Mainscript.main.getActivePlayer();

            foreach (var card in SelectionManager.Instance.selectedCards)
            {
                player.playerCardList.removeCard(card.myNode);
            }

            PlayerCardSpawnerScript.Instance.showPlayersHand(player);

            Mainscript.main.playerTurnCount -= 1;
            Mainscript.main.updateMoveCount();

            PopUpCardManager.Instance.hideInfo();

            GameEnd.Instance.checkIfGameWon();
        }
        else
        {
            Debug.LogWarning("Need to select 5 cards");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConfirmButton : MonoBehaviour
{
    public SelectionManager selectionManager;

    public void onConfirmClicked()
    {
        if (selectionManager.selectedCards.Count == 5)
        {
            string color = selectionManager.selectedCards[0].cardsCityData.color;

            if (DesiseMarkers.Instance.desiseColorDict.ContainsKey(color))
            {
                DesiseMarkers.Instance.desiseColorDict[color].curedDesise();
                DesiseMarkers.Instance.checkForExtinctDisease(color);
            }

            Player player = Mainscript.main.getActivePlayer();

            foreach (var card in selectionManager.selectedCards)
            {
                player.playerCardList.removeCard(card.myNode);
            }

            PlayerCardSpawnerScript.Instance.showPlayersHand(player);

            Mainscript.main.playerTurnCount -= 1;
            Mainscript.main.updateMoveCount();

            CureInfoManager.Instance.hideInfo();

            GameOverScript.Instance.checkIfGameWon();
        }
        else
        {
            Debug.LogWarning("Need to select 5 cards");
        }
    }
}

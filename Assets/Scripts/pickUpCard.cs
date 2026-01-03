using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCard : MonoBehaviour
{
    public static bool playerTookCards = false;

    void OnMouseDown() //kad nospiež uz pacelšanas karti
    {
        if (Mainscript.main.inMiddleOfAcion())
        {
            return;
        }

        if(!playerTookCards)
        {
            Player player = Mainscript.main.getActivePlayer();
            PlayerDeck.draw(player);
            PlayerDeck.draw(player);
            PlayerHandUi.Instance.renderHand(player);

            Mainscript.main.playerTurnCount = 0;
            GameUI.Instance.updateMoveCount();

            playerTookCards = true;
            if (player.playerRole == PlayerRole.QuarantineSpecialist)
            {
                Mainscript.main.putCitysUnderQuarantine(player);
            }
        }
    }

}

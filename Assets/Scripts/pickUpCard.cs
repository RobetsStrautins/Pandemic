using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCard : MonoBehaviour
{

    public static bool playerTookCards = false;

    void OnMouseDown()
    {
        if (Mainscript.main.inMiddleOfAcion())
        {
            return;
        }

        if(!playerTookCards)
        {
            Player player = Mainscript.main.getActivePlayer();
            PlayerDeck.Instance.Draw(player);
            PlayerDeck.Instance.Draw(player);
            PlayerHandUi.Instance.renderHand(player);

            Mainscript.main.playerTurnCount = 0;
            GameUI.Instance.updateMoveCount();

            playerTookCards = true;
        }
    }
}

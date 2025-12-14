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

        //if(Mainscript.main.playerTurnComplite() && !playerTookCards)
        {
            Player player = Mainscript.main.getActivePlayer();
            PlayerDeck.Instance.Draw(player);
            PlayerDeck.Instance.Draw(player);
            PlayerCardSpawnerScript.Instance.showPlayersHand(player);

            playerTookCards = true;
            
            //if kas parabuda max kartis
        }
    }
}

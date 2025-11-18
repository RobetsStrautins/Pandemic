using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCardBack : MonoBehaviour
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
            PlayerCardSpawnerScript.Instance.givePlayerCard(player);
            PlayerCardSpawnerScript.Instance.givePlayerCard(player);
            PlayerCardSpawnerScript.Instance.showPlayersHand(player);

            playerTookCards = true;
            
            //if kas parabuda max kartis
        }
    }
}

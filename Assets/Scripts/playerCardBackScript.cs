using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCardBack : MonoBehaviour
{
    public PlayerCardSpawnerScript PlayerCardSpawnerScript;

    public static bool playerTookCards = false;

    void OnMouseDown()
    {
        if (Mainscript.main.inMiddleOfAcion())
        {
            return;
        }

        if(Mainscript.main.playerTurnComplite() && !playerTookCards)
        {
            Player player = Mainscript.main.getActivePlayer();
            PlayerCardSpawnerScript.givePlayerCard(player);
            PlayerCardSpawnerScript.givePlayerCard(player);
            PlayerCardSpawnerScript.showPlayersHand(player);

            playerTookCards = true;
            
            //if kas parabuda max kartis
        }
    }
}

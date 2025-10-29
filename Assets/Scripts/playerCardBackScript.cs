using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCardBack : MonoBehaviour
{
    void OnMouseDown()
    {
        if(Mainscript.main.playerTurnComplite())
        {
            Mainscript.main.nextTurn();
            Mainscript.main.updateMoveCount();
        }
    }
}

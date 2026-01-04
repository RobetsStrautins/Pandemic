using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerTop : MonoBehaviour
{
    public Text playerIdText;
    public Text playerRoleText;
    private Player player;

    public void Init(Player player) //inicializē spēlētāju
    {
        this.player = player;

        playerIdText.text = player.name;
        playerRoleText.text = player.playerRole.ToString();
        name = "PlayerTop " + (player.playerId + 1);

        transform.localPosition = new Vector3(-3 + 1.5f * player.playerId, 4.5f);

        SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
        sr.color = player.playerColor;
    }

    void OnMouseDown() //kad nospiež uz spēlētāja
    {
        if (Mainscript.main.inMiddleOfAcion())
        {
            return;
        }

        if (player != Mainscript.main.getActivePlayer())
        {
            PopUpCardManager.Instance.showPlayerCardFromPlayerTop(player);
        }
        else
        {
            
        }
    }
}

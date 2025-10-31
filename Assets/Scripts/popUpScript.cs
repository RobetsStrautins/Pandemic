using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpScript : MonoBehaviour
{
    public void exitPopUp()
    {
        CardInfoManager.Instance.hideInfo();
    }

    public void flyTo(PlayerCard card)
    {
        Mainscript.main.flytoCity(card.cityCard);
        
        PlayerCardSpawnerScript.playerCardList.removeCards(card.myNode);
        exitPopUp();
    }

    public void flyAnywhere(PlayerCard card)
    {
        Debug.Log("Nospied pilsetu");
        Mainscript.main.StartFlyAnywhere(card);

        PlayerCardSpawnerScript.playerCardList.removeCards(card.myNode);
        exitPopUp();
    }

    public void makereRearchStation(PlayerCard card)
    {
        card.cityCard.buildResearchStation();

        PlayerCardSpawnerScript.playerCardList.removeCards(card.myNode);
        exitPopUp();
    }
}

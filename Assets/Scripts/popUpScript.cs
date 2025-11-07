using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpScript : MonoBehaviour
{
    public void exitPopUp()
    {
        PlayerCardInfoManager.Instance.hideInfo();
        CardInfoManager.Instance.hideInfo();
    }

    public void flyTo(PlayerCard card)
    {
        Mainscript.main.flytoCity(card.myNode.cityCard);

        Player player = Mainscript.main.getActivePlayer();
        player.playerCardList.removeCard(card.myNode, card);

        exitPopUp();
    }

    public void flyAnywhere(PlayerCard card)
    {
        Debug.Log("Nospied pilsetu");
        Mainscript.main.waitingForCityClick = true;

        Player player = Mainscript.main.getActivePlayer();
        player.playerCardList.removeCard(card.myNode, card);

        exitPopUp();
    }

    public void makereRearchStation(PlayerCard card)
    {
        card.myNode.cityCard.buildResearchStation();

        Player player = Mainscript.main.getActivePlayer();
        player.playerCardList.removeCard(card.myNode, card);

        exitPopUp();
    }

    public void giveCard(PlayerCard card, Player playerToGiveCard)
    {
        playerToGiveCard.playerCardList.newNodeCard(card.myNode.cityCard);

        Player player = Mainscript.main.getActivePlayer();
        player.playerCardList.removeCard(card.myNode, card);

        exitPopUp();
    }

    public void removeCard(PlayerCard card)
    {
        Player player = Mainscript.main.getActivePlayer();
        player.playerCardList.removeCard(card.myNode, card);

        exitPopUp();
    }

    public void flyToResearchStation(CityData city)
    {
        exitPopUp();
        CardInfoManager.Instance.showReserchOpcions(city);
    }

    public void trasportTo(CityData city)
    {
        Mainscript.main.flytoCity(city);

        exitPopUp();
    }

    public void clearCubs(CityData city, int count)
    {
        city.removeCubs(count);

        Mainscript.main.playerTurnCount -= count;
        Mainscript.main.updateMoveCount();

        exitPopUp();
    }

    public void endTurnButton()
    {
        if (Mainscript.main.playerTurnCount == 0 && PlayerCardBack.playerTookCards)
        {
            PlayerCardBack.playerTookCards = false;
            Mainscript.main.nextTurn();
        }
    }
}

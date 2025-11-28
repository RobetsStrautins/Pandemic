using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpScript : MonoBehaviour
{
    public void exitPopUp()
    {
        PlayerCardInfoManager.Instance.hideInfo();
        CardInfoManager.Instance.hideInfo();
        CureInfoManager.Instance.hideInfo();
    }

    public void flyTo(PlayerCityCard card)
    {
        Mainscript.main.flytoCity(card.cardsCityData);

        Player player = Mainscript.main.getActivePlayer();
        player.playerCardList.removeCard(card.myNode, card);

        exitPopUp();
    }

    public void flyAnywhere(PlayerCityCard card)
    {
        Debug.Log("Nospied pilsetu");
        Mainscript.main.waitingForCityClick = true;

        Player player = Mainscript.main.getActivePlayer();
        player.playerCardList.removeCard(card.myNode, card);

        exitPopUp();
    }

    public void makeRearchStation(PlayerCityCard card)
    {
        card.cardsCityData.buildResearchStation();

        Player player = Mainscript.main.getActivePlayer();
        player.playerCardList.removeCard(card.myNode, card);

        exitPopUp();
    }

    public void giveCard(PlayerCityCard card, Player playerToGiveCard)
    {
        playerToGiveCard.playerCardList.newNodeCard(card.myNode.data);

        Player player = Mainscript.main.getActivePlayer();
        player.playerCardList.removeCard(card.myNode, card);

        exitPopUp();
    }

    public void removeCard(PlayerCityCard card)
    {
        Player player = Mainscript.main.getActivePlayer();
        player.playerCardList.removeCard(card.myNode, card);

        exitPopUp();
    }

    public void cureDiseasePopUp(string color)
    {
        exitPopUp();
        CureInfoManager.Instance.pickCardsToCureDisease(color);
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

    public void clearAllCubs(CityData city)
    {
        int cubs = city.getCubs();
        city.removeCubs(cubs);

        Mainscript.main.playerTurnCount --;
        Mainscript.main.updateMoveCount();

        DiseaseMarkers.Instance.checkForExtinctDisease(city.color);

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

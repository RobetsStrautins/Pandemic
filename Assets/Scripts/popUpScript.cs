using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpScript : MonoBehaviour
{
    public void exitPopUp()
    {
        PopUpCardManager.Instance.hideInfo();
        PopUpButtonManager.Instance.hideInfo();
    }

    public void flyTo(PlayerCityCard card)
    {
        Mainscript.main.flytoCity(card.cardsCityData);

        Player player = Mainscript.main.getActivePlayer();
        player.playerCardList.removeCard(card.myNode);

        exitPopUp();
    }

    public void flyAnywhere(PlayerCityCard card)
    {
        Debug.Log("Nospied pilsetu");
        Mainscript.main.waitingForCityClick = true;
        Mainscript.main.waitingForCityClickAction = "FlyTo";

        Player player = Mainscript.main.getActivePlayer();
        player.playerCardList.removeCard(card.myNode);

        exitPopUp();
    }

    public void makeRearchStation(PlayerCityCard card)
    {
        card.cardsCityData.buildResearchStation();

        Player player = Mainscript.main.getActivePlayer();
        player.playerCardList.removeCard(card.myNode);

        exitPopUp();
    }

    public void giveCard(PlayerCityCard card, Player playerToGiveCard)
    {
        playerToGiveCard.playerCardList.newNodeCard(card.myNode.data);

        Player player = Mainscript.main.getActivePlayer();
        player.playerCardList.removeCard(card.myNode);

        exitPopUp();
    }

    public void takeCard(CardNode cardNode, Player playerToTakeCard)
    {
        playerToTakeCard.playerCardList.removeCard(cardNode);   

        Player player = Mainscript.main.getActivePlayer();
        player.playerCardList.newNodeCard(cardNode.data);

        PlayerCardSpawnerScript.Instance.showPlayersHand(Mainscript.main.getActivePlayer());

        exitPopUp();
    }

    public void removeCard(CardNode myNode)
    {
        Player player = Mainscript.main.getActivePlayer();
        player.playerCardList.removeCard(myNode);

        exitPopUp();
    }

    public void cureDiseasePopUp(string color)
    {
        Debug.LogWarning("aaaaa" + color);
        exitPopUp();
        PopUpCardManager.Instance.pickCardsToCureDisease(color);
    }

    public void flyToResearchStation(CityData city)
    {
        exitPopUp();
        PopUpButtonManager.Instance.showReserchOpcions(city);
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

    public void quietNight(CardNode myNode, Player player)
    {
        PlayerDeck.Instance.quietNight = true;

        player.playerCardList.removeCard(myNode);
        exitPopUp();
    }

    public void resilientPopulation(CardNode myNode, Player player)
    {
        exitPopUp();

        PopUpButtonManager.Instance.showAllOpcionsDiscardDisease(myNode, player);
    }

    public void discardDisease(CityData CityToRemove, CardNode myNode, Player player)
    {
        player.playerCardList.removeCard(myNode);
        DiseaseDeck.Instance.discardDiseaseFromDeck(CityToRemove);
        exitPopUp();
    }

    public void governmentGrant(CardNode myNode, Player player)
    {
        Debug.Log("Nospied pilsetu");
        Mainscript.main.waitingForCityClick = true;
        Mainscript.main.waitingForCityClickAction = "BuildStation";

        player.playerCardList.removeCard(myNode);
        exitPopUp();
    }

    public void airLift(CardNode myNode, Player player)
    { 
        exitPopUp();

        PopUpButtonManager.Instance.showAllPlayers(myNode, player);
    }

    public void airLift2(Player playerList, CardNode myNode, Player player)
    {
        PlayerDeck.Instance.airLiftPlayer = playerList;

        Debug.Log("Nospied pilsetu");
        Mainscript.main.waitingForCityClick = true;
        Mainscript.main.waitingForCityClickAction = "AirLift";

        player.playerCardList.removeCard(myNode);
        exitPopUp();
    }

    public void endTurnButton()
    {
        if (Mainscript.main.playerTurnCount == 0 && PickUpCard.playerTookCards)
        {
            PickUpCard.playerTookCards = false;
            Mainscript.main.nextTurn();
        }
    }
}

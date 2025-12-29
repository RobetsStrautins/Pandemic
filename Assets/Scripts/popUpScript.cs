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
        Player player = Mainscript.main.getActivePlayer();

        player.moveToCity(card.cardsCityData);
        player.playerCardList.removeCard(card.cardData);
        GameUI.Instance.usedAction();

        exitPopUp();
    }

    public void flyAnywhere(PlayerCityCard card)
    {
        Debug.Log("Nospied pilsetu");
        Mainscript.main.waitingForCityClick = true;
        Mainscript.main.waitingForCityClickAction = "FlyTo";

        Player player = Mainscript.main.getActivePlayer();
        player.playerCardList.removeCard(card.cardData);

        exitPopUp();
    }

    public void makeRearchStation(PlayerCityCard card)
    {
        card.cardsCityData.buildResearchStation();

        Player player = Mainscript.main.getActivePlayer();
        player.playerCardList.removeCard(card.cardData);
        
        exitPopUp();
    }

    public void giveCard(PlayerCityCard card, Player playerToGiveCard)
    {
        playerToGiveCard.playerCardList.newNodeCard(card.cardData);

        Player player = Mainscript.main.getActivePlayer();
        player.playerCardList.removeCard(card.cardData);

        GameUI.Instance.usedAction();

        exitPopUp();
    }

    public void takeCard(CardData data, Player playerToTakeCard)
    {
        playerToTakeCard.playerCardList.removeCard(data);

        Player player = Mainscript.main.getActivePlayer();
        player.playerCardList.newNodeCard(data);

        GameUI.Instance.usedAction();

        PlayerHandUi.Instance.renderHand(Mainscript.main.getActivePlayer());

        exitPopUp();
    }

    public void removeCard(CardData data, Player player)
    {
        player.playerCardList.removeCard(data);

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
        PopUpButtonManager.Instance.showCitysWithReserchStation(city);
    }

    public void trasportTo(CityData city)
    {
        Player player = Mainscript.main.getActivePlayer();
        player.moveToCity(city);

        exitPopUp();
    }

    public void clearCubs(CityData city, int count)
    {
        city.removeCubs(count);

        Mainscript.main.playerTurnCount -= count;
        GameUI.Instance.updateMoveCount();

        exitPopUp();
    }

    public void clearAllCubs(CityData city)
    {
        int cubs = city.getCubs();
        city.removeCubs(cubs);

        Mainscript.main.playerTurnCount --;
        GameUI.Instance.updateMoveCount();

        DiseaseMarkers.Instance.checkForExtinctDisease(city.color);

        exitPopUp();
    }

    public void quietNight(CardData data, Player player)
    {
        PlayerDeck.quietNight = true;

        player.playerCardList.removeCard(data);
        exitPopUp();
    }

    public void resilientPopulation(CardData data, Player player)
    {
        exitPopUp();

        PopUpButtonManager.Instance.showAllDiscardDiseaseCards(data, player);
    }

    public void discardDisease(CityData CityToRemove, CardData data, Player player)
    {
        player.playerCardList.removeCard(data);
        PlayerDeck.discardDiseaseFromDeck(CityToRemove);
        exitPopUp();
    }

    public void governmentGrant(CardData data, Player player)
    {
        Debug.Log("Nospied pilsetu");
        Mainscript.main.waitingForCityClick = true;
        Mainscript.main.waitingForCityClickAction = "BuildStation";

        player.playerCardList.removeCard(data);
        exitPopUp();
    }

    public void airLift(CardData data, Player player)
    { 
        exitPopUp();

        PopUpButtonManager.Instance.showAllPlayers(data, player);
    }

    public void airLift2(Player playerList, CardData data, Player player)
    {
        PlayerDeck.airLiftPlayer = playerList;

        Debug.Log("Nospied pilsetu");
        Mainscript.main.waitingForCityClick = true;
        Mainscript.main.waitingForCityClickAction = "AirLift";

        player.playerCardList.removeCard(data);
        exitPopUp();
    }

    public void endTurnButton()
    {
        Player player = Mainscript.main.getActivePlayer();
        if(player.playerCardList.playerCardCount > 7)
        {
            Debug.LogWarning("Speletajam ir par daudz kartis, nomest liekas kartis");
            PopUpCardManager.Instance.maxCardLimit(player);
        }
        else if(PickUpCard.playerTookCards)
        {
            if(Mainscript.main.inMiddleOfAcion())
            {
                Debug.LogWarning("Esi vidu citai darbibai");
                return;
            }
            
            PickUpCard.playerTookCards = false;
            Mainscript.main.nextTurn();
        }
    }

    public void closeButtonForButtonPanal()
    {
        PopUpButtonManager.Instance.hideInfo();
    }

    public void closeButtonForCardPanal()
    {
        PopUpCardManager.Instance.hideInfo();
    }
}

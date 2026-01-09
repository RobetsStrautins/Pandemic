using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpScript : MonoBehaviour
{
    public void exitPopUp() //aizver pop-up logus
    {
        PopUpCardManager.Instance.hideInfo();
        PopUpButtonManager.Instance.hideInfo();
    }

    public void flyTo(PlayerCityCard card) //pārlido uz izvēlēto pilsētu
    {
        Player player = Mainscript.main.getActivePlayer();

        player.moveToCity(card.cardsCityData);
        player.playerCardList.removeCard(card.cardData);

        exitPopUp();
    }

    public void flyAnywhere(PlayerCityCard card) //pārlido uz jebkuru pilsētu
    {
        Debug.Log("Nospied pilsētu");
        Mainscript.main.waitingForCityClick = true;
        Mainscript.main.waitingForCityClickAction = "FlyTo";

        Player player = Mainscript.main.getActivePlayer();
        player.playerCardList.removeCard(card.cardData);

        exitPopUp();
    }

    public void makeRearchStation(PlayerCityCard card) //izveido izpētes staciju
    {
        card.cardsCityData.buildResearchStation();

        Player player = Mainscript.main.getActivePlayer();
        player.playerCardList.removeCard(card.cardData);
        
        exitPopUp();
    }

    public void giveCard(PlayerCityCard card, Player playerToGiveCard) //dod karti citam spēlētājam
    {
        playerToGiveCard.playerCardList.newNodeCard(card.cardData);

        Player player = Mainscript.main.getActivePlayer();
        player.playerCardList.removeCard(card.cardData);

        GameUI.Instance.usedAction();

        exitPopUp();

        if(playerToGiveCard.playerCardList.playerCardCount > 7)
        {
            PopUpCardManager.Instance.maxCardLimit(playerToGiveCard);
        }
    }

    public void takeCard(CardData data, Player playerToTakeCard) //paņem karti no cita spēlētāja
    {
        playerToTakeCard.playerCardList.removeCard(data);

        Player player = Mainscript.main.getActivePlayer();
        player.playerCardList.newNodeCard(data);

        GameUI.Instance.usedAction();

        PlayerHandUi.Instance.renderHand(Mainscript.main.getActivePlayer());

        exitPopUp();
    }

    public void removeCard(CardData data, Player player) //nomet karti
    {
        player.playerCardList.removeCard(data);

        exitPopUp();
    }

    public void cureDiseasePopUp(string color) //izārstē slimību
    {
        Debug.LogWarning("aaaaa" + color);
        exitPopUp();
        PopUpCardManager.Instance.pickCardsToCureDisease(color);
    }

    public void flyToResearchStation(CityData city) //pārlido uz pilsētu ar izpētes staciju
    {
        exitPopUp();
        PopUpButtonManager.Instance.showCitysWithReserchStation(city);
    }

    public void trasportTo(CityData city) //transportē uz izvēlēto pilsētu
    {
        Player player = Mainscript.main.getActivePlayer();
        player.moveToCity(city);

        exitPopUp();
    }

    public void clearCubes(CityData city, int count) //noņem kubiciņus no pilsētas
    {
        city.removeCubes(count);

        Mainscript.main.playerTurnCount -= count;
        GameUI.Instance.updateMoveCount();

        exitPopUp();
    }

    public void clearAllCubes(CityData city) //noņem visus kubiciņus no pilsētas
    {
        int cubes = city.getCubes();
        city.removeCubes(cubes);

        GameUI.Instance.usedAction();

        if(DiseaseMarkers.Instance.getDiseaseProgress(city.color) == DiseaseColorProgress.Cured)
        {
           DiseaseMarkers.Instance.checkForExtinctDisease(city.color); 
        }
        
        exitPopUp();
    }

    public void quietNight(CardData data, Player player) //izmanto klusās nakts notikumu
    {
        PlayerDeck.quietNight = true;

        player.playerCardList.removeCard(data);
        exitPopUp();
    }

    public void resilientPopulation(CardData data, Player player) //izmanto populācijas pretošanās notikumu
    {
        exitPopUp();

        PopUpButtonManager.Instance.showAllDiscardDiseaseCards(data, player);
    }

    public void discardDisease(CityData CityToRemove, CardData data, Player player) //noņem infekcijas kārti
    {
        player.playerCardList.removeCard(data);
        DiseaseDeck.usedInfectionDeck.Remove(CityToRemove);
        exitPopUp();
    }

    public void governmentGrant(CardData data, Player player) //izmanto valdības subsīdijasnotikumu
    {
        Debug.Log("Nospied pilsētu");
        Mainscript.main.waitingForCityClick = true;
        Mainscript.main.waitingForCityClickAction = "BuildStation";

        player.playerCardList.removeCard(data);
        exitPopUp();
    }

    public void airLift(CardData data, Player player) //izmanto gaisa transportu
    { 
        exitPopUp();

        PopUpButtonManager.Instance.showAllPlayers(data, player);
    }

    public void airLift2(Player playerList, CardData data, Player player) //gaisa transporta otrā daļa
    {
        PlayerDeck.airLiftPlayer = playerList;

        Debug.Log("Nospied pilsētu");
        Mainscript.main.waitingForCityClick = true;
        Mainscript.main.waitingForCityClickAction = "AirLift";

        player.playerCardList.removeCard(data);
        exitPopUp();
    }

    public void endTurnButton()// beidz gājienu
    {
        Player player = Mainscript.main.getActivePlayer();
        if(player.playerCardList.playerCardCount > 7)
        {

            ActionLog.Instance.addEntry("Spēlētājam ir par daudz kārtis", Color.red);
            PopUpCardManager.Instance.maxCardLimit(player);
        }
        else if(PickUpCard.playerTookCards)
        {
            if(Mainscript.main.inMiddleOfAcion())
            {
                Debug.LogWarning("Esi vidu citai darbībai");
                return;
            }
            
            PickUpCard.playerTookCards = false;
            Mainscript.main.nextTurn();
        }
        else
        {
            ActionLog.Instance.addEntry("Lai beigtu gājienu, sākuma japaceļ kārtis", Color.grey);
        }
    }

    public void closeButtonForButtonPanal() //aizver pogu pop-up
    {
        PopUpButtonManager.Instance.hideInfo();
    }

    public void closeButtonForCardPanal() //aizver kāršu pop-up
    {
        PopUpCardManager.Instance.hideInfo();
    }

    public void confirmButton() //apstiprināšanas poga
    {
        int neededCardCount = 5;

        Player player = Mainscript.main.getActivePlayer();
        if (player.playerRole == PlayerRole.Scientist)
        {
            neededCardCount = 4;
        }

        if (SelectionManager.selectedCards.Count == neededCardCount)
        {
            string color = SelectionManager.selectedCards[0].cardsCityData.color;

            if (DiseaseMarkers.diseaseColorDict.ContainsKey(color))
            {
                DiseaseMarkers.diseaseColorDict[color].curedDisease();
                DiseaseMarkers.Instance.checkForExtinctDisease(color);
            }

            foreach (var card in SelectionManager.selectedCards)
            {
                player.playerCardList.removeCard(card.cardData);
            }

            PlayerHandUi.Instance.renderHand(player);

            GameUI.Instance.usedAction();

            PopUpCardManager.Instance.hideInfo();

            foreach (var playerInList in Mainscript.main.playersList)
            {
                playerInList.removeCubesFromCurrentCity();
            }
        }
        else
        {
            Debug.LogWarning("Need to select 5 cards or 4 if Scientist to cure disease");
            ActionLog.Instance.addEntry("Vajag 5 kārtis lai izārstētu vai 4 ja ir zinātnieks",Color.red);
        }
    }
}

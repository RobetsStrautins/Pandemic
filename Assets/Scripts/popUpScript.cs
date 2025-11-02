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

        Player player = Mainscript.main.getActivePlayer();
        player.playerCardList.removeCards(card.myNode);

        exitPopUp();
    }

    public void flyAnywhere(PlayerCard card)
    {
        Debug.Log("Nospied pilsetu");
        Mainscript.main.waitingForCityClick = true;

        Player player = Mainscript.main.getActivePlayer();
        player.playerCardList.removeCards(card.myNode);

        exitPopUp();
    }

    public void makereRearchStation(PlayerCard card)
    {
        card.cityCard.buildResearchStation();

        Player player = Mainscript.main.getActivePlayer();
        player.playerCardList.removeCards(card.myNode);

        exitPopUp();
    }

    public void flyToResearchStation(CityData city)
    {
        exitPopUp();
        Mainscript.main.loadReserchOpcions(city);
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
    
}

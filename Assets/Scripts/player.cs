using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerId;
    public PlayerRole playerRole;

    private int startingCityId = 3;
    public CityData city;

    public PlayerCardList playerCardList = new PlayerCardList();
    public Color playerColor;

    private float playerXOffset = 0;
    private float playerYOffset = 0;

    public void Init()
    {
        switch (playerId)
        {
            case 0:
                playerXOffset = -0.1f;
                playerYOffset = 0.1f;
                break;
            case 1:
                playerXOffset = 0.1f;
                playerYOffset = 0.1f;
                break;
            case 2:
                playerXOffset = -0.1f;
                playerYOffset = -0.1f;
                break;
            case 3:
                playerXOffset = 0.1f;
                playerYOffset = -0.1f;
                break;
            default:
                playerXOffset = 0;
                playerYOffset = 0;
                break;
        }

        if (CitySpawner.cityMap.ContainsKey(startingCityId))
        {
            city = CitySpawner.cityMap[startingCityId];
            transform.position = new Vector3(city.Xcord + playerXOffset, city.Ycord + playerYOffset, -1);
        }

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            playerColor = getColorForRole(playerRole);
            sr.color = playerColor;
        }

        if (playerRole == PlayerRole.QuarantineSpecialist)
        {
            Mainscript.main.putCitysUnderQuarantine(this);
        }
    }

    public void moveToCityIfConnected(CityData pressedCity)
    {
        if (city.connectedCity.Contains(pressedCity.id))
        {
            city = pressedCity;
            transform.position = new Vector3(city.Xcord + playerXOffset, city.Ycord + playerYOffset, -1);
            Mainscript.main.playerTurnCount -= 1;
            removeCubesFromCurrentCity();
        }
    }

    public void moveToCity(CityData pressedCity)
    {
        city = pressedCity;
        transform.position = new Vector3(city.Xcord + playerXOffset, city.Ycord + playerYOffset, -1);
        Mainscript.main.playerTurnCount -= 1;
        removeCubesFromCurrentCity();
    }

    public void removeCubesFromCurrentCity()//medic special ability
    {
        if (playerRole == PlayerRole.Medic && DiseaseMarkers.Instance.getDiseaseProgress(city.color) == DiseaseColorProgress.Cured)
        {
            city.removeCubes(city.getCubes());
        }
    }

    private Color getColorForRole(PlayerRole role)
    {
        return role switch
        {
            PlayerRole.Medic => new Color(1f, 0.5f, 0f),
            PlayerRole.Scientist => Color.white,
            PlayerRole.QuarantineSpecialist => new Color(0f, 0.5f, 0f),
            PlayerRole.Researcher => new Color(0.6f, 0.3f, 0.1f),
            PlayerRole.OperationsExpert => Color.green,
            _ => Color.gray
        };
    }

}
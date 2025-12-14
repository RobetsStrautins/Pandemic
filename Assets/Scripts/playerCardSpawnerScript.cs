using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCardSpawnerScript : MonoBehaviour
{
    public GameObject playerCityCardPrefab;

    public GameObject playerBonesCardPrefab;
    private GameObject playerCardParent;

    public static PlayerCardSpawnerScript Instance;

    public void Awake()
    {
        Instance = this;
    }

    public void showPlayersHand(Player player)
    {
        clearPlayerHand();

        float firstCardCordX = -7.5f;
        float firstCardCordY = -4;

        CardNode current = player.playerCardList.first;
        int i = 0;

        playerCardParent = new GameObject("Speletaja " + (player.playerId+1)+ " kartis");

        while (current != null)
        {
            if(current.data.Type == CardType.City)
            {
                GameObject cardObj = Instantiate(playerCityCardPrefab);
                cardObj.transform.parent = playerCardParent.transform;
                PlayerCityCard newCard = cardObj.GetComponent<PlayerCityCard>();
                newCard.Init(current);

                cardObj.transform.localPosition = new Vector3(firstCardCordX + 1.8f * i, firstCardCordY, 0f);
            }
            else if(current.data.Type == CardType.Bonus)
            {
                GameObject cardObj = Instantiate(playerBonesCardPrefab);
                cardObj.transform.parent = playerCardParent.transform;
                playerBonesCard newCard = cardObj.GetComponent<playerBonesCard>();
                newCard.Init(current);

                cardObj.transform.localPosition = new Vector3(firstCardCordX + 1.8f * i, firstCardCordY, 0f);
            }
            i++;
            current = current.next;
        }
    }

    public void clearPlayerHand()
    {
        if (playerCardParent != null)
        {
            Object.Destroy(playerCardParent);
            playerCardParent = null;
        }
    }
}


public class CardNode
{
    public CardNode prev = null;
    public CardNode next = null;

    public CardData data;
}

public class PlayerCardList
{
    public CardNode first = null;
    public CardNode last = null;
    public int playerCardCount = 0;

    public void newNodeCard(CardData data)
    {
        CardNode node = new CardNode { data = data };

        if (first == null)
        {
            first = node;
            last = node;
        }
        else
        {
            last.next = node;
            node.prev = last;
            last = node;
        }
        playerCardCount++;
    }
    
    public void removeCard(CardNode node)
    {
        if (first == node && last == node)
        {
            first = null;
            last = null;
        }
        else
        {
            if (node.prev == null)
            {
                first = node.next;
            }
            else node.prev.next = node.next;

            if (node.next == null)
            {
                last = node.prev;
            }
            else node.next.prev = node.prev;
        }
        
        PlayerCardSpawnerScript.Instance.showPlayersHand(Mainscript.main.getActivePlayer());   
        playerCardCount--;
    }
}
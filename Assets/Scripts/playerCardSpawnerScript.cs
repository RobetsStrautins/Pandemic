using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCardSpawnerScript : MonoBehaviour
{
    public GameObject playerCardPrefab;
    private GameObject playerCardParent;


    public void givePlayerCard(Player player)
    {
        int cityId = Random.Range(1, 7);
        CityData randomCity = CitySpawner.cityMap[cityId];

        player.playerCardList.newNodeCard(randomCity);
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
            GameObject cardObj = Instantiate(playerCardPrefab);
            cardObj.transform.parent = playerCardParent.transform;
            PlayerCard newCard = cardObj.GetComponent<PlayerCard>();
            newCard.Init(current);

            cardObj.transform.localPosition = new Vector3(firstCardCordX + 1.8f * i, firstCardCordY, 0f);

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

    public void RemoveCard(Player player, PlayerCard cardToRemove)
    {
        player.playerCardList.removeCard(cardToRemove.myNode, cardToRemove);

        Object.Destroy(cardToRemove.gameObject);

        uppdateCardPos();
    }

    private void uppdateCardPos()
    {
        float firstCardCordX = -7.5f;
        float firstCardCordY = -4f;

        int i = 0;

        foreach (Transform child in playerCardParent.transform)
        {
            child.localPosition = new Vector3(firstCardCordX + 1.8f * i, firstCardCordY, 0f);
            i++;
        }
    }
}

public class CardNode
    {
        public CardNode prev = null;
        public CardNode next = null;

        public CityData cityCard;

        //public BounsCard Bcard;
    }

public class PlayerCardList
{
    public CardNode first = null;
    public CardNode last = null;
    public int playerCardCount = 0;

    public void newNodeCard(CityData city)
    {
        CardNode node = new CardNode { cityCard = city };

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

    public void removeCard(CardNode node, PlayerCard playerCard)
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
            
        playerCard.myNode = null;
        GameObject.Destroy(playerCard.gameObject);
        playerCardCount--;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCardSpawnerScript : MonoBehaviour
{
    public GameObject playerCardPrefab;

    public void givePlayerCard(Player player)
    {
        int cityId = Random.Range(1, 7);
        CityData randomCity = CitySpawner.cityMap[cityId];

        GameObject cardObj = Instantiate(playerCardPrefab);
        PlayerCard newCard = cardObj.GetComponent<PlayerCard>();
        newCard.Init(randomCity);

        player.playerCardList.addCards(newCard);
    }
}

public class CardNode
{
    public CardNode prev = null;
    public CardNode next = null;
    public PlayerCard card;

    //public BounsCard Bcard;
}

public class PlayerCardList
{
    public CardNode first = null;
    public CardNode last = null;
    private float firstCardCordX = -7.5f;
    private float firstCardCordY = -4;
    private int playerCardCount = 0;

    public void addCards(PlayerCard card)
    {
        CardNode node = new CardNode { card = card };
        card.myNode = node;

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

    public void removeCards(CardNode node)
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
            
        node.card.myNode = null;
        GameObject.Destroy(node.card.gameObject);
        playerCardCount--;
        //showCardPos();
    }

    private void showCardPos()
    {
        CardNode current = first;
        int i = 0;

        while (current != null)
        {
            current.card.transform.localPosition = new Vector3(firstCardCordX + 1.8f * i, firstCardCordY);
            i++;
            current = current.next;
        }
    }

    private void disableCardPos()
    {
        CardNode current = first;

        while (current != null)
        {
            Object.Destroy(current.card.gameObject);
            current = current.next;
        }
    }

}

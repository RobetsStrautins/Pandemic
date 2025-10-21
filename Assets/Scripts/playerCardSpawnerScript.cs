using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCardScript : MonoBehaviour
{
    public GameObject playerCardPrefab;

    private int playerCardCount = 0;
    private int maxCardLimit = 7;

    private PlayerCardList playerCardList = new PlayerCardList();

    public void givePlayerCard()
    {
        playerCardCount += 1;

        int cityId = Random.Range(1, 7);
        CityData randomCity = CitySpawner.cityMap[cityId];

        GameObject cardObj = Instantiate(playerCardPrefab);
        PlayerCard newCard = cardObj.GetComponent<PlayerCard>();
        newCard.Init(randomCity);

        playerCardList.addCards(newCard);
    }
}

public class CardNode
{
    public CardNode prev = null;
    public CardNode next = null;
    public PlayerCard card;
}

public class PlayerCardList
{
    public CardNode first = null;
    public CardNode last = null;
    private float firstCardCordX = -7.5f;
    private float firstCardCordY = -4;
    private int playerCardCount= 0;

    public void addCards(PlayerCard card)
    {
        CardNode node = new CardNode { card = card };
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
        node.card.transform.localPosition = new Vector3(firstCardCordX + 1.8f * playerCardCount, firstCardCordY);
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
    }
}

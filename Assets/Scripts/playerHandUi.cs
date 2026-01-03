using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandUi : MonoBehaviour
{
    public static PlayerHandUi Instance;

    public GameObject playerCityCardPrefab;
    public GameObject playerBonesCardPrefab;
    private GameObject playerCardParent;

    public void Awake() //konstruktors
    {
        Instance = this;
    }

    public void renderHand(Player player) //atjauno spēlētāja roku
    {
        clearPlayerHand();

        float firstCardCordX = -7.75f;
        float firstCardCordY = -4;

        int i = 0;

        playerCardParent = new GameObject("Speletaja " + (player.playerId+1)+ " kartis");

       foreach (CardData cardData in player.playerCardList.getAllCards())
        {
            if(cardData.Type == CardType.City)
            {
                GameObject cardObj = Instantiate(playerCityCardPrefab);
                cardObj.transform.parent = playerCardParent.transform;
                PlayerCityCard newCard = cardObj.GetComponent<PlayerCityCard>();
                newCard.Init(cardData);

                cardObj.transform.localPosition = new Vector3(firstCardCordX + 1.55f * i, firstCardCordY, 0f);
            }
            else if(cardData.Type == CardType.Event)
            {
                GameObject cardObj = Instantiate(playerBonesCardPrefab);
                cardObj.transform.parent = playerCardParent.transform;
                PlayerBonesCard newCard = cardObj.GetComponent<PlayerBonesCard>();
                newCard.Init(cardData);

                cardObj.transform.localPosition = new Vector3(firstCardCordX + 1.55f * i, firstCardCordY, 0f);
            }
            i++;
        }
    }

    public void clearPlayerHand() //notīra spēlētāja roku
    {
        if (playerCardParent != null)
        {
            Destroy(playerCardParent);
            playerCardParent = null;
        }
    }
}

public class PlayerCardList
{
    private class CardNode
    {
        public CardNode prev = null;
        public CardNode next = null;
        public CardData data;
    }
    
    private CardNode first = null;
    private CardNode last = null;
    public int playerCardCount = 0;

    public void newNodeCard(CardData data) //pievieno jaunu kārti spēlētāja karšu sarakstam
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
    
    public void removeCard(CardData data) //atraida un noņem kārti no spēlētāja karšu saraksta
    {
        CardNode current = first;

        while (current != null)
        {
            if (current.data == data)
            {
                removeNode(current);
                return;
            }
            current = current.next;
        }
    }

    private void removeNode(CardNode node) //noņem kārti no spēlētāja karšu saraksta
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
        
        PlayerHandUi.Instance.renderHand(Mainscript.main.getActivePlayer());   
        playerCardCount--;
    }

    public IEnumerable<CardData> getAllCards() //atgriež visas kārtis spēlētāja karšu sarakstā
    {
        CardNode current = first;
        while (current != null)
        {
            yield return current.data;  
            current = current.next;
        }
    }
}
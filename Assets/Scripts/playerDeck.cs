using System.Collections.Generic;
using UnityEngine;

public static class PlayerDeck
{
    public static int epidemicCardCount = 4;
    public static int playerCount = 4;

    private static List<CardData> deck = new List<CardData>();
    private static List<CardData> usedDeck = new List<CardData>();

    public static IReadOnlyList<CardData> testDeck => deck;
    public static IReadOnlyList<CardData> testUsedDeck => usedDeck;
    public static bool quietNight = false;
    public static Player airLiftPlayer;

    public static void SetupBeforeEpidemicCard()
    {
        deck.Clear();
        usedDeck.Clear();
        
        foreach (var city in CitySpawner.cityMap.Values)
        {
            deck.Add(new PlayerCityCardData(city));
        }

        createBonusCards();

        shuffle(deck);
    }

    public static void SetupAfterEpidemicCard()
    {
        int baseStackSize = deck.Count / epidemicCardCount;
        int extraCards = deck.Count % epidemicCardCount;
        int currentIndex = 0;
        
        for (int i = 0; i < epidemicCardCount; i++)
        {
            int stackSize = baseStackSize + (i < extraCards ? 1 : 0);

            int insertIndex = Random.Range(currentIndex, currentIndex + stackSize + 1);

            deck.Insert(insertIndex, new EpidemicCardData("Epidemic"));

            currentIndex += stackSize + 1;
        }
    }

    public static void draw(Player player)
    {
        if (deck.Count == 0)
        {
            GameUI.Instance.gameLost("(Beidzas kartis)");
            return;
        }

        if(deck[0].Type == CardType.Epidemic)
        {
            usedDeck.Add(deck[0]);
            deck.Remove(deck[0]);
            DiseaseDeck.epidemic();
        }
        else
        {
            player.playerCardList.newNodeCard(deck[0]);
            usedDeck.Add(deck[0]);
            deck.Remove(deck[0]);
        }
    }

    private static void shuffle(List<CardData> list)
    {
        int cardCount = list.Count;
        while (cardCount > 1)
        {
            cardCount--;
            int swap = Random.Range(0, cardCount + 1);
            (list[swap], list[cardCount]) = (list[cardCount], list[swap]); 
        }
    }

    private static void createBonusCards()
    {
        deck.Add(new BonusCardData(
            BonusCardType.ValdibasSubsidija,
            "VALDĪBAS SUBSĪDIJA",
            "Ieliec izpētes staciju jebkurā pilsētā (nav nepieciešama plisētas kārts)."
        ));

        deck.Add(new BonusCardData(
            BonusCardType.KlusaNakts,
            "KLUSA NAKTS",
            "Tiek izlaists nākamais “Inficē pilsētas” solis (netiek atvērtas infekciju kārtis)."
        ));

        deck.Add(new BonusCardData(
            BonusCardType.PopulacijasPretosanas,
            "POPULĀCIJAS PRETOŠANĀS",
            "Izņem no spēles vienu infekcijas kārti, kas atrodas izlietoto infekcijas kāršu kaudzē. Šo kārti drīkst izspēlēt starp soļiem “Inficēt” un “Pastiprināties” epidēmijas laikā."
        ));

        deck.Add(new BonusCardData(
            BonusCardType.GaisaTransportas,
            "GAISA TRANSPORTAS",
            "Pārvieto jebkuru kauliņu uz jebkuru pilsētu. Pārvietojot cita spēlētaju kauliņu, vispirms ir jāsaņem atļauja."
        ));
    }
}

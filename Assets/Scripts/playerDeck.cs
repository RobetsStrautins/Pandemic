using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : MonoBehaviour
{
    public static PlayerDeck Instance;

    public int epidemicCardCount = 4;

    public List<CardData> deck = new List<CardData>();
    public List<CardData> usedDeck = new List<CardData>();

    public List<CityData> infectionDeck = new List<CityData>();
    public List<CityData> usedInfectionDeck = new List<CityData>();

    public bool quietNight = false;
    public Player airLiftPlayer;
    void Awake()
    {
        Instance = this;
    }

    public void SetupBeforeEpidemicCard()
    {
        deck.Clear();
        usedDeck.Clear();

        foreach (var city in CitySpawner.cityMap.Values)
        {
            deck.Add(new PlayerCityCardData(city));
        }

        createBonuesCards();

        shuffle(deck);
    }

    public void SetupAfterEpidemicCard()
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

    public void Draw(Player player)
    {
        if (deck.Count == 0)
        {
            GameOverScript.Instance.GameLost();
            return;
        }

        if(deck[0].Type == CardType.Epidemic)
        {
            usedDeck.Add(deck[0]);
            deck.Remove(deck[0]);
            DiseaseDeck.Instance.epidemic();
        }
        else
        {
            player.playerCardList.newNodeCard(deck[0]);
            usedDeck.Add(deck[0]);
            deck.Remove(deck[0]);
        }
    }

    public void shuffle(List<CardData> list)
    {
        int cardCount = list.Count;
        while (cardCount > 1)
        {
            cardCount--;
            int swap = Random.Range(0, cardCount + 1);
            (list[swap], list[cardCount]) = (list[cardCount], list[swap]); 
        }
    }

    private void createBonuesCards()
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

        deck.Add(new BonusCardData(
            BonusCardType.Prognoze,
            "PROGNOZE",
            "Pacel un aplūko 6 virsējas kārtis no infekcijas kāršu kaudzītes, pēc tam sakārto tās jebkādā secībā un noliec infekcijas kāršu kaudzītes virspusē."
        ));
    }
}

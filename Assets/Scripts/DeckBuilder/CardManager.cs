using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [HideInInspector]
    static List<Card> cardList = DeckLoader.cardList;
    static List<CaptainCard> captainCardList = DeckLoader.captainCardList;
    public FactionButton noButton;
    public FactionButton niButton;
    public FactionButton moButton;
    public FactionButton scButton;

    public CardScrollList northernScrollList;
    public CardScrollList nilfgaardScrollList;
    public CardScrollList monstersScrollList;
    public CardScrollList scoiataelScrollList;

    public CaptainCardsView northernCCView;
    public CaptainCardsView nilfgaardCCView;
    public CaptainCardsView monstersCCView;
    public CaptainCardsView scoiataelCCView;

    List<Card> northern = new List<Card>();
    List<Card> nilfgaard = new List<Card>();
    List<Card> monsters = new List<Card>();
    List<Card> scoiatael = new List<Card>();

    List<CaptainCard> northernCaptain = new List<CaptainCard>();
    List<CaptainCard> nilfgaardCaptain = new List<CaptainCard>();
    List<CaptainCard> monstersCaptain = new List<CaptainCard>();
    List<CaptainCard> scoiataelCaptain = new List<CaptainCard>();


    void Start()
    {
        SortCards();
    }

    void SortCards()
    {
        foreach(Card card in cardList)
        {
            if (card.faction == Faction.Neutral)
            {
                northern.Add(card);
                nilfgaard.Add(card);
                monsters.Add(card);
                scoiatael.Add(card);
            }
            else if (card.faction == Faction.Northern)
            {
                northern.Add(card);
            }
            else if (card.faction == Faction.Nilfgaard)
            {
                nilfgaard.Add(card);
            }
            else if (card.faction == Faction.Monsters)
            {
                monsters.Add(card);
            }
            else if (card.faction == Faction.Scoiatael)
            {
                scoiatael.Add(card);
            }
        }

        foreach (CaptainCard cCard in captainCardList)
        {
            if (cCard.faction == Faction.Monsters)
            {
                monstersCaptain.Add(cCard);
            }
            else if (cCard.faction == Faction.Scoiatael)
            {
                scoiataelCaptain.Add(cCard);
            }
            else if (cCard.faction == Faction.Northern)
            {
                northernCaptain.Add(cCard);
            }
            else if (cCard.faction == Faction.Nilfgaard)
            {
                nilfgaardCaptain.Add(cCard);
            }
        }

        northernScrollList.GetFactionCards(northern);
        nilfgaardScrollList.GetFactionCards(nilfgaard);
        monstersScrollList.GetFactionCards(monsters);
        scoiataelScrollList.GetFactionCards(scoiatael);

        northernCCView.GetCaptainCards(northernCaptain);
        nilfgaardCCView.GetCaptainCards(nilfgaardCaptain);
        monstersCCView.GetCaptainCards(monstersCaptain);
        scoiataelCCView.GetCaptainCards(scoiataelCaptain);
    }
}

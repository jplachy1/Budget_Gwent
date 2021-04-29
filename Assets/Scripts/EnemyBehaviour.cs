using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public GameHandler gh;
    public GetCards  gc;
    Card cardToPlay;
    List<Card> cards = new List<Card>();

    void Start()
    {
        cards = GetDeck();
    }

    void Update()
    {
        Play();
    }

    void Play()
    {
        if (gh.turn == false)
        {
            cardToPlay = PickCard();
            cards.Remove(cardToPlay);
            PlaceCard(cardToPlay);
        }
    }

    Card PickCard()
    {
        Card candidate;
        
        candidate = cards[Random.Range(0, cards.Count)];
        return candidate;
    }

    List<Card> GetDeck()
    {
        return gc.holderCards;
    }

    void PlaceCard(Card _card)
    {
        Debug.Log("Placing " + _card.name);
        GameObject cardGO = GameObject.Find("CardHolder En/" + _card.name);
        string cardRank = _card.rank.ToString();
        if (_card.rank != Rank.Weather & _card.ability != Ability.Spy)
        {
            cardRank = "Rank" + cardRank + " En";
        }
        else if (_card.rank == Rank.Weather)
        {
            cardRank = "Rank" + cardRank;
        }
        else if (_card.ability == Ability.Spy)
        {
            cardRank = "Rank" + cardRank + " P";
        }

        RankBehaviour rank = GameObject.Find(cardRank).GetComponent<RankBehaviour>();
        cardGO.GetComponent<CardBehaviour>().PlaceCard(rank);
    }



}

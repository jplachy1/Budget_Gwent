using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    List<Card> deck = new List<Card>();
    public bool isPlayer;
    void Start()
    {
        if (isPlayer)
        {
            deck = PickDecksPlayButton.playerDeck;
        }
        else
        {
            deck = PickDecksPlayButton.enemyDeck;
        }

        SetCards();
        deck = Shuffle();
    }

    List<Card> Shuffle()
    {
        int[] shuffledInts = new int[deck.Count];
        List<Card> shuffledCards = new List<Card>();

        for (int i=0; i < deck.Count; i++)
        {
            shuffledInts[i] = i;
        }
        for (int i=0; i < deck.Count; i++)
        {
            int temp;
            int x = shuffledInts[Random.Range(0,deck.Count)];
            int y = shuffledInts[Random.Range(0,deck.Count)];
            temp = shuffledInts[x];
            shuffledInts[x] = shuffledInts[y];
            shuffledInts[y] = temp;
        }

        foreach (int n in shuffledInts)
        {
            shuffledCards.Add(deck[n]);
        }

        return shuffledCards;
    }

    public Card DrawCard()
    {
        Card cardToRemove = deck[0];
        deck.RemoveAt(0);
        return cardToRemove;
    }

    void SetCards()
    {
        foreach (Card card in deck)
        {
            card.rankDmg = card.baseDmg;
        }
    }
}

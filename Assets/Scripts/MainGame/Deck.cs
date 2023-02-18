using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Deck : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public bool isPlayer;
    void Start()
    {
        if (isPlayer)
        {
            deck = PickDecksPlayButton.playerDeck;
            //deck = Resources.LoadAll<Card>("Cards").ToList();
        }
        else
        {
            deck = PickDecksPlayButton.enemyDeck;
            //deck = Resources.LoadAll<Card>("Cards").ToList();
        }
        deck = Shuffle();
    }

    List<Card> Shuffle()
    {
        int[] shuffledInts = new int[deck.Count];
        List<Card> shuffledCards = new List<Card>();

        for (int i = 0; i < deck.Count; i++)
        {
            shuffledInts[i] = i;
        }
        for (int i = 0; i < deck.Count; i++)
        {
            int temp;
            int x = shuffledInts[Random.Range(0, deck.Count)];
            int y = shuffledInts[Random.Range(0, deck.Count)];
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
        if (deck.Count > 0)
        {
            Card cardToRemove = deck[0];
            deck.RemoveAt(0);
            return cardToRemove;
        }
        return null;
    }

    public void GetCardBack(Card _card)
    {
        deck.Add(_card);
        deck = Shuffle();
    }

    public List<Card> GetMusterCards(string[] _group)
    {
        List<Card> musters = new List<Card>();

        foreach (Card card in deck)
        {
            foreach (string musterID in _group)
            {
                if (card.Id == musterID)
                {
                    musters.Add(card);
                }
            }
        }
        return musters;
    }


    // public GameObject GetMusterCards()
    // {

    // }
}

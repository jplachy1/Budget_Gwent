using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GetCards : MonoBehaviour
{
    // List<Card> allCards = new List<Card>();

    public GameObject cardPrefab;
    GameObject cardGO;
    public GameObject cardHolder;
    public ShowCardCount showCardCount;
    static List<Card> deck = LoadScene.deck;
    [HideInInspector]
    public List<Card> holderCards = new List<Card>();
    

    void Start()
    {
        deck = Shuffle(deck);
        for (int i=0; i < 10; i++)
        {
            holderCards.Add(deck[0]);
            deck.RemoveAt(0);
        }

        holderCards = holderCards.OrderBy(o => o.baseDmg).ThenBy(o => o.name).ToList();


        foreach(Card card in holderCards)
        {
            DrawCard(card);
        }


        showCardCount.DisplayCardCount(deck.Count);

    }

    Card SetCard(Card _card)
    {
        Card _newCard = Card.CreateInstance<Card>();

        _newCard.name = _card.name;
        _newCard.baseDmg = _card.baseDmg;
        _newCard.rank = _card.rank;
        _newCard.isHero = _card.isHero;
        _newCard.artwork = _card.artwork;
        _newCard.ability = _card.ability;
        _newCard.faction = _card.faction;
        _newCard.rankDmg = _card.baseDmg;

        return _newCard;
    }

    public void DrawCard(Card _card)
    {
        cardGO = Instantiate(cardPrefab, gameObject.transform);
        cardGO.name = _card.name;
        if (_card.rank == Rank.Weather)
        {
            cardGO.transform.GetChild(1).gameObject.SetActive(false);
            cardGO.transform.GetChild(2).gameObject.SetActive(false);
        }
        cardGO.GetComponent<CardBehaviour>().card = SetCard(_card);
    }

    List<Card> Shuffle(List<Card> _deck)
    {
        int[] shuffledInts = new int[_deck.Count];
        List<Card> shuffledCards = new List<Card>();

        for (int i=0; i < _deck.Count; i++)
        {
            shuffledInts[i] = i;
        }
        for (int i=0; i < _deck.Count; i++)
        {
            int temp;
            int x = shuffledInts[Random.Range(0,_deck.Count)];
            int y = shuffledInts[Random.Range(0,_deck.Count)];
            temp = shuffledInts[x];
            shuffledInts[x] = shuffledInts[y];
            shuffledInts[y] = temp;
        }

        foreach (int n in shuffledInts)
        {
            shuffledCards.Add(_deck[n]);
        }

        return shuffledCards;
        
    }
}
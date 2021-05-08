﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CardHolder : MonoBehaviour
{
    public GameObject cardPrefab;
    GameObject cardGO;
    [HideInInspector]
    public List<Card> cards;
    public Deck deck;
    

    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            cards.Add(deck.DrawCard());
        }
        cards = cards.OrderBy(o => o.baseDmg).ThenBy(o => o.name).ToList();

        for (int i = 0; i < 10; i++)
        {
            MakeCard(cards[i], i);
        }

        Debug.Log(gameObject.name + cards.Count);
    }

    public void MakeCard(Card _card, int position)
    {     
        cardGO = Instantiate(cardPrefab, gameObject.transform);
        cardGO.transform.SetSiblingIndex(position);
        cardGO.name = _card.name;
        if (_card.rank == Rank.Weather)
        {
            // Special cards don't need the damage text
            cardGO.transform.GetChild(1).gameObject.SetActive(false);
            cardGO.transform.GetChild(2).gameObject.SetActive(false);
        }
        cardGO.GetComponent<CardBehaviour>().card = _card;
    }

    List<Card> GetDeck()
    {
        if (gameObject.name == "CardHolder P")
        {
            return PickDecksPlayButton.playerDeck;
        }
        else if (gameObject.name == "CardHolder En")
        {
            return PickDecksPlayButton.enemyDeck;
        }

        return null;
    }

    public void DrawSpyCard()
    {
        // Card _card;
        // for (int i = 0; i < 2; i++)
        // {
        //     _card = deck[Random.Range(0,deck.Count)];
        //     holderCards.Add(_card);
        //     deck.Remove(_card);
        //     holderCards = holderCards.OrderBy(o => o.baseDmg).ThenBy(o => o.name).ToList();
            

        //     DrawCard(_card, holderCards.IndexOf(_card));
        // }
    }

    public void RemoveCard(Card _card)
    {
        //holderCards.RemoveAll(c => c.name == _card.name);
    }

    public void ResizeDeck()
    {
        // if(holderCards.Count > 10)
        // {
        //     RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        //     GridLayoutGroup gridLayoutGroup = gameObject.GetComponent<GridLayoutGroup>();
        //     float width = rectTransform.sizeDelta.x;
        //     gridLayoutGroup.cellSize = new Vector2(width / holderCards.Count, gridLayoutGroup.cellSize.y);
        // }
    }
}
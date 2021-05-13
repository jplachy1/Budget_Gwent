using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CardHolder : MonoBehaviour
{
    public GameObject cardPrefab;
    GameObject cardGO;
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
    }

    void MakeCard(Card _card, int position)
    {     
        cardGO = Instantiate(cardPrefab, gameObject.transform);
        cardGO.transform.SetSiblingIndex(position);
        cardGO.name = _card.name;
        if (_card.rank == Rank.Weather | _card.rank == Rank.Decoy)
        {
            // Special cards don't need the damage text
            cardGO.transform.GetChild(1).gameObject.SetActive(false);
            cardGO.transform.GetChild(2).gameObject.SetActive(false);
        }
        cardGO.GetComponent<CardBehaviour>().card = SetCard(_card);
        ResizeDeck();
    }

    Card SetCard(Card _card)
    {
        Card newCard = Card.CreateInstance<Card>();
        newCard.ID = _card.ID;
        newCard.name = _card.name;
        newCard.artwork = _card.artwork;
        newCard.baseDmg = _card.baseDmg;
        newCard.rankDmg = _card.baseDmg;
        newCard.rank = _card.rank;
        newCard.isHero = _card.isHero;
        newCard.faction = _card.faction;
        newCard.ability = _card.ability;
        return newCard;
    }

    public void DrawSpyCard()
    {
        Card _card;
        for (int i = 0; i < 2; i++)
        {
            _card = deck.DrawCard();
            if(_card != null)
            {
                cards.Add(_card);
                cards = cards.OrderBy(o => o.baseDmg).ThenBy(o => o.name).ToList();
                MakeCard(_card, cards.IndexOf(_card));
            }
            
        }
    }

    public void RemoveCard(Card _card)
    {
        //holderCards.RemoveAll(c => c.name == _card.name);
    }

    public void GetCard(GameObject CardGO)
    {
        Card _card = CardGO.GetComponent<CardBehaviour>().card;
        cards.Add(_card);
        cards = cards.OrderBy(o => o.baseDmg).ThenBy(o => o.name).ToList();

        CardGO.transform.SetParent(gameObject.transform);
        CardGO.transform.SetSiblingIndex(cards.IndexOf(_card));
        CardGO.GetComponent<CardBehaviour>().card.rankDmg = CardGO.GetComponent<CardBehaviour>().card.baseDmg;
        cards.Add(CardGO.GetComponent<CardBehaviour>().card);
    }

    void ResizeDeck()
    {
        if(cards.Count > 10)
        {
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            GridLayoutGroup gridLayoutGroup = gameObject.GetComponent<GridLayoutGroup>();
            float width = rectTransform.sizeDelta.x;
            gridLayoutGroup.cellSize = new Vector2(width / cards.Count, gridLayoutGroup.cellSize.y);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GetCards : MonoBehaviour
{
    public GameObject cardPrefab;
    GameObject cardGO;
    public GameObject cardHolder;
    public ShowCardCount showCardCount;
    static List<Card> deck;
    [HideInInspector]
    public List<Card> holderCards = new List<Card>();
    

    void Start()
    {
        deck = GetDeck();
        deck = Shuffle(deck);
        for (int i=0; i < 10; i++)
        {
            holderCards.Add(deck[0]);
            deck.RemoveAt(0);
        }

        holderCards = holderCards.OrderBy(o => o.baseDmg).ThenBy(o => o.name).ToList();

        for (int i = 0; i < holderCards.Count; i++)
        {   
            DrawCard(holderCards[i], i);
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

    public void DrawCard(Card _card, int position)
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
        Card _card;
        for (int i = 0; i < 2; i++)
        {
            _card = deck[Random.Range(0,deck.Count)];
            holderCards.Add(_card);
            deck.Remove(_card);
            holderCards = holderCards.OrderBy(o => o.baseDmg).ThenBy(o => o.name).ToList();
            

            DrawCard(_card, holderCards.IndexOf(_card));
        }
    }

    public void RemoveCard(Card _card)
    {
        holderCards.RemoveAll(c => c.name == _card.name);
        Debug.Log(gameObject.name + " "  + holderCards.Count.ToString());
    }

    public void ResizeDeck()
    {
        if(holderCards.Count > 10)
        {
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            GridLayoutGroup gridLayoutGroup = gameObject.GetComponent<GridLayoutGroup>();
            float width = rectTransform.sizeDelta.x;
            gridLayoutGroup.cellSize = new Vector2(width / holderCards.Count, gridLayoutGroup.cellSize.y);
        }
    }
}
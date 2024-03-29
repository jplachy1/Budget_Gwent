﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GraveyardBehaviour : MonoBehaviour, IPointerClickHandler
{
    public List<Card> cards = new List<Card>();
    public Deck deck;
    public Text cardCountText;
    public GameObject cardView;
    public GameObject gCardPrefab;
    public GameHandler gh;
    public CardHolder cardHolder;
    public bool player;

    [HideInInspector]
    public bool swap = false;

    GameObject cardViewContent;
    int swapped = 0;
    bool medicCall = false;
    Card medicCard = null;

    void Start()
    {
        cardViewContent = cardView.transform.GetChild(0).GetChild(0).gameObject;
        //Vector2 cardSize = cardViewContent.GetComponent<GridLayoutGroup>().cellSize;

        InitialCardSpawn();

        if (!player)
        {

            foreach (Card card in cards)
            {
                gh.SpawnCard(card, cardHolder.gameObject);
                cardHolder.cards.Add(card);
            }

            foreach (Transform child in cardViewContent.transform)
            {
                Destroy(child.gameObject);
            }


            cardView.SetActive(false);
            cards.Clear();

            swap = true;

            return;
        }
    }

    void Update()
    {
        if (swapped > 1 & !swap)
        {
            foreach (Card card in cards)
            {
                gh.SpawnCard(card, cardHolder.gameObject);
                cardHolder.cards.Add(card);
            }

            foreach (Transform child in cardViewContent.transform)
            {
                Destroy(child.gameObject);
            }

            cardView.SetActive(false);
            cards.Clear();

            swap = true;
        }
    }

    /// <summary>
    /// Draw 10 cards from the deck and put them into the graveyard
    /// </summary>
    private void InitialCardSpawn()
    {
        cardView.SetActive(true);
        for (int i = 0; i < 10; i++)
        {
            Card card = deck.DrawCard();
            GameObject cardGO = SpawnGraveyardCard(card, gh.GetCardPosition(cards, card));
            cardGO.GetComponent<Button>().onClick.AddListener(delegate { SwapCards(cardGO); });
        }
    }

    void SwapCards(GameObject _cardGO)
    {
        deck.GetCardBack(_cardGO.GetComponent<CardBehaviour>().card);
        int position = _cardGO.transform.GetSiblingIndex();
        cards.RemoveAll(x => x.Id == _cardGO.GetComponent<CardBehaviour>().card.Id);
        Destroy(_cardGO);
        Card _card = deck.DrawCard();
        GameObject cardGO = SpawnGraveyardCard(_card, position);
        cardGO.GetComponent<Button>().onClick.AddListener(delegate { SwapCards(cardGO); });
        swapped++;
    }

    void StartGame()
    {

    }

    GameObject SpawnGraveyardCard(Card _card, int position)
    {
        cards.Add(_card);

        GameObject _cardGO = Instantiate(gCardPrefab, cardViewContent.transform);
        _cardGO.transform.SetSiblingIndex(position);
        _cardGO.name = _card.Name;
        if (_card.Rank == Rank.Weather | _card.Rank == Rank.Decoy | _card.Rank == Rank.Horn)
        {
            // Special cards don't need the damage text
            _cardGO.transform.GetChild(1).gameObject.SetActive(false);
            _cardGO.transform.GetChild(2).gameObject.SetActive(false);
        }
        _cardGO.GetComponent<CardBehaviour>().card = gh.SetCard(_card);
        _cardGO.GetComponent<CardBehaviour>().isMovable = false;

        return _cardGO;
    }

    public void MoveToGraveyard(GameObject CardGO, GameObject RankGO)
    {
        Card _card = CardGO.GetComponent<CardBehaviour>().card;
        if (RankGO.TryGetComponent<RankBehaviour>(out RankBehaviour rankBehaviour))
        {
            rankBehaviour.cards.Remove(_card);
        }
        else if (RankGO.TryGetComponent<WeatherBehaviour>(out WeatherBehaviour weatherBehaviour))
        {
            weatherBehaviour.cards.Remove(_card);
        }

        GameObject GraveyardCard = SpawnGraveyardCard(_card, gh.GetCardPosition(cards, _card));
        Destroy(CardGO);
        //CardGO.transform.SetParent(cardView.transform.GetChild(0).GetChild(0));
        cards.Add(_card);
    }

    public void ShowCards(bool onlyUnits = false)
    {
        if (onlyUnits)
        {
            foreach (Transform child in cardViewContent.transform)
            {
                Card _card = child.gameObject.GetComponent<CardBehaviour>().card;
                if (_card.Rank == Rank.Weather || _card.Rank == Rank.Decoy || _card.Rank == Rank.Horn)
                {
                    child.gameObject.SetActive(false);
                }
                else
                {
                    child.GetComponent<Button>().onClick.AddListener(delegate { MedicClick(child.gameObject); });
                }
            }
        }
        cardView.SetActive(true);
    }

    void MedicClick(GameObject Button)
    {
        Card _card = Button.GetComponent<CardBehaviour>().card;
        GameObject cardGO = Instantiate(gh.cardPrefab, GameObject.Find("WaitingRank").transform);
        cardGO.name = _card.Name;
        if (_card.Rank == Rank.Weather | _card.Rank == Rank.Decoy | _card.Rank == Rank.Horn)
        {
            // Special cards don't need the damage text
            cardGO.transform.GetChild(1).gameObject.SetActive(false);
            cardGO.transform.GetChild(2).gameObject.SetActive(false);
        }
        cardGO.GetComponent<CardBehaviour>().card = gh.SetCard(_card);
        cardGO.GetComponent<CardBehaviour>().isMovable = false;

        gh.medicCard = cardGO;
        gh.medicCardChosen = true;

        cardView.SetActive(false);
    }

    public void OnPointerClick(PointerEventData ped)
    {
        if (cardView.activeInHierarchy == false)
        {
            ShowCards();
        }
    }

    public void GetMedicCard()
    {
        if (AreUnitsInGraveyard())
        {
            ShowCards(GameHandler.UNITS);
        }
    }

    private bool AreUnitsInGraveyard()
    {
        foreach (Card card in cards)
        {
            if (card.IsUnit) return true;
        }

        return false;
    }
}

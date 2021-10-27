using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    public bool turn;

    public CardHolder playerHolder;
    public CardHolder enemyHolder;

    public Deck playerDeck;
    public Deck enemyDeck;

    public GraveyardBehaviour playerGraveyard;
    public GraveyardBehaviour enemyGraveyard;

    public RankBehaviour RankCloseP;
    public RankBehaviour RankRangedP;
    public RankBehaviour RankSiegeP;
    public RankBehaviour RankCloseEn;
    public RankBehaviour RankRangedEn;
    public RankBehaviour RankSiegeEn;

    public GameObject cardPrefab;

    public string[][] musters = new string[][]
    {
        new string[] {"S8", "S9", "S10"},
        new string[] {"S12", "S13", "S14"},
        new string[] {"S19", "S20", "S21"},
        new string[] {"M01", "M02", "M03", "M04"},
        new string[] {"M30", "M31", "M32"},
        new string[] {"M34", "M35", "M36", "M37", "M38"},
        new string[] {"M20", "M21", "M22"},
        new string[] {"M08", "M09", "M10"},
    };
    [HideInInspector]
    public GameObject medicCard;
    [HideInInspector]
    public bool medicCardChosen = false;

    public const bool UNITS = true;
    public static bool showingCardDetails = false;

    bool medicBreak = false;

    void Start()
    {
        turn = true;
    }

    void Update()
    {
        if (medicCardChosen)
        {
            PlaceCard(GetRank(medicCard.GetComponent<CardBehaviour>().card), medicCard);
            medicCardChosen = false;
        }
    }

    void PlaceCard(RankBehaviour rank, GameObject CardGO)
    {
        Card card = CardGO.GetComponent<CardBehaviour>().card;

        rank.cards.Add(card);

        if (card.ability == Ability.Spy)
        {
            if (turn)
            {
                playerHolder.DrawSpyCard();
            }
            else
            {
                enemyHolder.DrawSpyCard();
            }
        }
        else if (card.ability == Ability.Muster)
        {
            List<Card> musterCards = new List<Card>();
            List<GameObject> musterCardGOs = new List<GameObject>();
            string[] musterGroup = GetMusterGroup(card);
            int position = 0;

            if (turn)
            {
                musterCardGOs = playerHolder.GetMusterCards(musterGroup);
                if (musterCardGOs.Count > 0)
                {
                    foreach (GameObject _cardGO in musterCardGOs)
                    {
                        Card _card = _cardGO.GetComponent<CardBehaviour>().card;

                        playerHolder.cards.RemoveAll(x => x.ID == _card.ID);
                        GameObject cardRank = GameObject.Find("Rank" + _card.rank.ToString() + " P");
                        cardRank.GetComponent<RankBehaviour>().cards.Add(_card);

                        position = GetCardPosition(cardRank.GetComponent<RankBehaviour>().cards, card);
                        _cardGO.GetComponent<CardBehaviour>().isMovable = false;
                        _cardGO.transform.SetParent(cardRank.transform);
                        _cardGO.transform.SetSiblingIndex(position);

                    }
                }

                musterCards = playerDeck.GetMusterCards(musterGroup);
                if (musterCards.Count > 0)
                {
                    foreach (Card _card in musterCards)
                    {
                        playerDeck.deck.RemoveAll(x => x.ID == _card.ID);
                        GameObject cardRank = GameObject.Find("Rank" + _card.rank.ToString() + " P");
                        cardRank.GetComponent<RankBehaviour>().cards.Add(_card);
                        SpawnCard(_card, cardRank, false);
                    }
                }

            }
            else
            {
                musterCardGOs = enemyHolder.GetMusterCards(musterGroup);
                if (musterCardGOs.Count > 0)
                {
                    foreach (GameObject _cardGO in musterCardGOs)
                    {
                        Card _card = _cardGO.GetComponent<CardBehaviour>().card;

                        enemyHolder.cards.RemoveAll(x => x.ID == _card.ID);
                        GameObject cardRank = GameObject.Find("Rank" + _card.rank.ToString() + " En");
                        cardRank.GetComponent<RankBehaviour>().cards.Add(_card);

                        position = GetCardPosition(cardRank.GetComponent<RankBehaviour>().cards, card);
                        _cardGO.GetComponent<CardBehaviour>().isMovable = false;
                        _cardGO.transform.SetParent(cardRank.transform);
                        _cardGO.transform.SetSiblingIndex(position);
                    }
                }

                musterCards = enemyDeck.GetMusterCards(musterGroup);
                if (musterCards.Count > 0)
                {
                    foreach (Card _card in musterCards)
                    {
                        enemyDeck.deck.RemoveAll(x => x.ID == _card.ID);
                        GameObject cardRank = GameObject.Find("Rank" + _card.rank.ToString() + " En");
                        cardRank.GetComponent<RankBehaviour>().cards.Add(_card);
                        SpawnCard(_card, cardRank, false);
                    }
                }
            }

            if (musterCards.Count > 0)
            {
                foreach (Card _card in musterCards)
                {

                }
            }
        }
        else if (card.ability == Ability.Medic)
        {
            if (turn)
            {
                playerGraveyard.GetMedicCard();
                medicBreak = true;
                return;
            }
            else
            {
                //enemy code ...
            }
        }

        ResizeField(rank.gameObject, rank.cards.Count);
        ResizeField(playerHolder.gameObject, playerHolder.cards.Count);
        rank.RankSum();
    }

    void PlaceCard(WeatherBehaviour weather, GameObject CardGO)
    {
        Card card = CardGO.GetComponent<CardBehaviour>().card;

        if (weather.cards.Find(c => c.name == card.name))
        {
            if (turn)
            {
                playerGraveyard.MoveToGraveyard(CardGO, weather.gameObject);
            }
            else
            {
                enemyGraveyard.MoveToGraveyard(CardGO, weather.gameObject);
            }
        }
        else
        {
            if (card.ability == Ability.Close)
            {
                weather.Close(card);
            }
            else if (card.ability == Ability.Ranged)
            {
                weather.Ranged(card);
            }
            else if (card.ability == Ability.Siege)
            {
                weather.Siege(card);
            }
            else if (card.ability == Ability.Clear)
            {
                while (weather.transform.childCount > 0)
                {
                    playerGraveyard.MoveToGraveyard(weather.transform.GetChild(0).gameObject, weather.gameObject);
                }
                weather.Clear();
            }
            else if (card.ability == Ability.Scorch)
            {
                if (turn)
                {
                    playerGraveyard.MoveToGraveyard(CardGO, weather.gameObject);
                }
                else
                {
                    enemyGraveyard.MoveToGraveyard(CardGO, weather.gameObject);
                }

                weather.Scorch();
            }
        }
        //cardHolder.RemoveCard(card);
    }

    public void PlaceCard(GameObject RankGO, GameObject cardGO)
    {
        Card cardToPlace = cardGO.GetComponent<CardBehaviour>().card;
        cardGO.transform.SetParent(RankGO.transform);
        cardGO.GetComponent<CardBehaviour>().isMovable = false;

        if (turn)
        {
            playerHolder.cards.RemoveAll(x => x.ID == cardToPlace.ID);
        }
        else
        {
            enemyHolder.cards.RemoveAll(x => x.ID == cardToPlace.ID);
        }

        if (cardToPlace.rank != Rank.Weather & cardToPlace.rank != Rank.Horn)
        {
            PlaceCard(RankGO.GetComponent<RankBehaviour>(), cardGO);
        }
        else if (cardToPlace.rank == Rank.Weather)
        {
            PlaceCard(RankGO.GetComponent<WeatherBehaviour>(), cardGO);
        }
        else if (cardToPlace.rank == Rank.Horn)
        {
            RankGO.GetComponent<HornBehaviour>().Horn();
        }

        if (medicBreak)
        {
            medicBreak = false;
            return;
        }


        //cardGO.transform.SetSiblingIndex(GetCardPosition(rankBehaviour.cards, cardToPlace));


        turn = !turn;
    }

    public void DecoyCard(GameObject DecoyGO, GameObject CardGO, GameObject RankGO)
    {
        CardBehaviour cardBehaviour = CardGO.GetComponent<CardBehaviour>();
        RankBehaviour rankBehaviour = RankGO.GetComponent<RankBehaviour>();
        if (turn)
        {
            playerHolder.GetCard(CardGO);
        }
        else
        {
            enemyHolder.GetCard(CardGO);
        }

        DecoyGO.GetComponent<CardBehaviour>().isMovable = false;
        rankBehaviour.cards.RemoveAll(x => x.ID == cardBehaviour.card.ID);
        rankBehaviour.RankSum();
        DecoyGO.transform.SetParent(RankGO.transform);

        turn = !turn;
    }

    public void SpawnCard(Card _card, GameObject parent, bool movable = true)
    {
        int position = 0;
        if (parent.TryGetComponent<CardHolder>(out CardHolder cardHolder))
        {
            position = GetCardPosition(cardHolder.cards, _card);
        }
        else if (parent.TryGetComponent<RankBehaviour>(out RankBehaviour rankBehaviour))
        {
            position = GetCardPosition(rankBehaviour.cards, _card);
        }


        GameObject cardGO = Instantiate(cardPrefab, parent.transform);
        cardGO.transform.SetSiblingIndex(position);
        cardGO.name = _card.name;
        if (_card.rank == Rank.Weather | _card.rank == Rank.Decoy | _card.rank == Rank.Horn)
        {
            // Special cards don't need the damage text
            cardGO.transform.GetChild(1).gameObject.SetActive(false);
            cardGO.transform.GetChild(2).gameObject.SetActive(false);
        }
        cardGO.GetComponent<CardBehaviour>().card = SetCard(_card);

        if (!movable)
        {
            cardGO.GetComponent<CardBehaviour>().isMovable = false;
        }
    }

    public GameObject InitialSpawn(Card _card, GameObject parent)
    {
        int position = 0;
        if (parent.TryGetComponent<CardHolder>(out CardHolder cardHolder))
        {
            position = GetCardPosition(cardHolder.cards, _card);
        }
        else if (parent.TryGetComponent<RankBehaviour>(out RankBehaviour rankBehaviour))
        {
            position = GetCardPosition(rankBehaviour.cards, _card);
        }

        GameObject cardGO = Instantiate(cardPrefab, parent.transform);
        cardGO.transform.SetSiblingIndex(position);
        cardGO.name = _card.name;
        if (_card.rank == Rank.Weather | _card.rank == Rank.Decoy | _card.rank == Rank.Horn)
        {
            // Special cards don't need the damage text
            cardGO.transform.GetChild(1).gameObject.SetActive(false);
            cardGO.transform.GetChild(2).gameObject.SetActive(false);
        }
        cardGO.GetComponent<CardBehaviour>().card = SetCard(_card);
        cardGO.GetComponent<CardBehaviour>().isMovable = false;

        return cardGO;

        // if (!movable)
        // {
        //     cardGO.GetComponent<CardBehaviour>().isMovable = false;
        // }
    }

    public Card SetCard(Card _card)
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
        newCard.largeArtwork = _card.largeArtwork;
        return newCard;
    }

    public string[] GetMusterGroup(Card _card)
    {
        List<string> group = new List<string>();

        foreach (string[] musterGroup in musters)
        {
            if (musterGroup.Contains<string>(_card.ID))
            {
                return musterGroup;
            }
        }
        return null;
    }

    public int GetCardPosition(List<Card> _cards, Card _card)
    {
        _cards = _cards.OrderBy(o => o.baseDmg).ThenBy(o => o.name).ToList();
        for (int i = 0; i < _cards.Count; i++)
        {
            if (_cards[i].ID == _card.ID)
            {
                return i;
            }
        }
        return 0;
    }

    public void ResizeField(GameObject field, int cardCount)
    {
        GridLayoutGroup gridLayoutGroup = field.GetComponent<GridLayoutGroup>();
        if (cardCount > 10)
        {
            RectTransform rectTransform = field.GetComponent<RectTransform>();
            float width = rectTransform.sizeDelta.x;
            gridLayoutGroup.cellSize = new Vector2(width / (cardCount + (cardCount - 10) / 9f), gridLayoutGroup.cellSize.y);
        }
        else
        {
            gridLayoutGroup.cellSize = new Vector2(90, gridLayoutGroup.cellSize.y);
        }
    }

    public GameObject GetRank(Card _card)
    {
        string cardRank = _card.rank.ToString();
        if (turn)
        {
            if (_card.rank != Rank.Weather & _card.ability != Ability.Spy)
            {
                cardRank = "Rank" + cardRank + " P";
            }
            else if (_card.rank == Rank.Weather)
            {
                cardRank = "Rank" + cardRank;
            }
            else if (_card.ability == Ability.Spy)
            {
                cardRank = "Rank" + cardRank + " En";
            }
        }
        else
        {
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
        }


        return GameObject.Find(cardRank);
    }


}


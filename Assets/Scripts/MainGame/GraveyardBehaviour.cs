using System.Collections;
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

    GameObject cardViewContent;
    int swapped = 0;
    bool swap = false;

    void Start()
    {
        cardViewContent = cardView.transform.GetChild(0).GetChild(0).gameObject;
        //Vector2 cardSize = cardViewContent.GetComponent<GridLayoutGroup>().cellSize;

        cardView.SetActive(true);
        for (int i = 0; i < 10; i++)
        {                   
            Card card = deck.DrawCard();
            GameObject cardGO = SpawnGraveyardCard(card, gh.GetCardPosition(cards, card));
            cardGO.GetComponent<Button>().onClick.AddListener(delegate{SwapCards(cardGO);});
        }
        
    }

    void Update()
    {
        if (swapped > 1 & !swap)
        {
            Debug.Log("ccc");
            foreach(Card card in cards)
            {
                gh.SpawnCard(card, cardHolder.gameObject);
                cardHolder.cards.Add(card);
            }
            cardView.SetActive(false);
            swap = true;
        }
    }

    void SwapCards(GameObject _cardGO)
    {
        deck.GetCardBack(_cardGO.GetComponent<CardBehaviour>().card);
        int position = _cardGO.transform.GetSiblingIndex();
        cards.RemoveAll(x => x.ID == _cardGO.GetComponent<CardBehaviour>().card.ID);
        Destroy(_cardGO);
        Card _card = deck.DrawCard();
        GameObject cardGO = SpawnGraveyardCard(_card, position);
        cardGO.GetComponent<Button>().onClick.AddListener(delegate{SwapCards(cardGO);});
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
        _cardGO.name = _card.name;
        if (_card.rank == Rank.Weather | _card.rank == Rank.Decoy | _card.rank == Rank.Horn)
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

        CardGO.transform.SetParent(cardView.transform.GetChild(0).GetChild(0));
        cards.Add(_card);
    }

    public void ShowCards()
    {
        cardView.SetActive(true);
    }

    public void OnPointerClick(PointerEventData ped)
    {
        if (cardView.activeInHierarchy == false)
        {
            ShowCards();    
        }
    }
}

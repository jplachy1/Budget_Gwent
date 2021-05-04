using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardBehaviour : MonoBehaviour,IPointerEnterHandler,IPointerDownHandler,IPointerUpHandler
{
    GameHandler gh;
    bool mouseOver;
    bool mouseClicked;
    bool aboveRank = false;
    bool isMovable = true;
    GameObject cardObject;
    GameObject rankGO;
    Vector3 dist;
    Vector3 cardPos;
    public Card card;
    public GetCards getCards;
    int siblingIndex;

    void Start()
    {
        SetCardArtwork();
        gh = GameObject.Find("GameManager").GetComponent<GameHandler>();
    }

    void Update()
    {
        OnHover();
        OnMouseDown();
        ShowDamage();
    }

    void OnHover()
    {
        if(mouseOver)
        {
            
        }
    }

    void OnMouseDown()
    {
        if (mouseClicked & isMovable & gh.turn)
        {
            /* Card movement */
            gameObject.transform.position = Input.mousePosition - dist;
            
            
        }
    }

    void SetCardArtwork()
    {
        GameObject childImage = gameObject.transform.GetChild(0).gameObject;
        childImage.GetComponent<Image>().sprite = card.artwork;
    }

    public void PlaceCard(RankBehaviour rank)
    {
        GetCards getCards = gameObject.transform.parent.gameObject.GetComponent<GetCards>();
        if (card.ability == Ability.Spy)
        {
            getCards.DrawSpyCard();
        }
        
        getCards.RemoveCard(card);
        getCards.ResizeDeck();
        gh.turn = !gh.turn;
        gameObject.transform.SetParent(rank.transform);
        isMovable = false;


        if (card.rank != Rank.Weather)
        {
            rank.cardsInRank.Add(card);
            rank.RankSum();
        }
        else
        {
            if (card.ability == Ability.Close)
            {
                AddWeatherCard("RankClose");
            }
            else if (card.ability == Ability.Ranged)
            {
                AddWeatherCard("RankRanged");
            }
            else if (card.ability == Ability.Siege)
            {
                AddWeatherCard("RankSiege");
            }
            else if (card.ability == Ability.Clear)
            {
                RemoveWeatherCards();
            }
        }     

    }

    void ShowDamage()
    {
        GameObject childText = gameObject.transform.GetChild(2).gameObject;
        childText.GetComponent<Text>().text = card.rankDmg.ToString();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        rankGO = col.gameObject;

        if (isCardPlaceable(rankGO))
        {
            aboveRank = true;
        }
        else
        {
            aboveRank = false;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
         
    }

    public void OnPointerEnter(PointerEventData eventData) 
    {
        mouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData) 
    {
        mouseOver = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        mouseClicked = true;
        dist = Input.mousePosition - gameObject.transform.position;

        if (isMovable)
        {
            cardPos = gameObject.transform.position;
            transform.parent.gameObject.GetComponent<GridLayoutGroup>().enabled = false;
            siblingIndex = gameObject.transform.GetSiblingIndex();
            gameObject.transform.SetSiblingIndex(9);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        mouseClicked = false;
        if (aboveRank && isMovable)
        {
            transform.parent.gameObject.GetComponent<GridLayoutGroup>().enabled = true;
            PlaceCard(rankGO.GetComponent<RankBehaviour>());
        }

        if (isMovable)
        {
            gameObject.transform.SetSiblingIndex(siblingIndex);
            gameObject.transform.position = cardPos;
            transform.parent.gameObject.GetComponent<GridLayoutGroup>().enabled = true;
        }
    }

    bool isCardPlaceable(GameObject rank)
    {
        string rankName = rank.name.ToString();
        string cardRankName = card.rank.ToString();
        if (card.ability != Ability.Spy && card.rank != Rank.Agile && card.rank != Rank.Weather)
        {
            if (rankName.Contains(cardRankName) && rankName.Contains("P"))
            {
                return true;
            }
        }
        else if(card.ability == Ability.Spy)
        {
            if (rankName.Contains(cardRankName) && rankName.Contains("En"))
            {
                return true;
            }
        }
        else if (card.rank == Rank.Agile)
        {
            if ((rankName.Contains("Ranged") || rankName.Contains("Close")) && rankName.Contains("P"))
            {
                return true;
            }
        }
        else if (card.rank == Rank.Weather)
        {
            if (rankName.Contains("Weather"))
            {
                return true;
            }
        }

        return false;
    }

    void AddWeatherCard(string nameRank)
    {
        GameObject rank = GameObject.Find(nameRank + " P");
        RankBehaviour rankBehaviour = rank.GetComponent<RankBehaviour>();
        rankBehaviour.cardsInRank.Add(card);
        rankBehaviour.RankSum();

        rank = GameObject.Find(nameRank + " En");
        rankBehaviour = rank.GetComponent<RankBehaviour>();
        rankBehaviour.cardsInRank.Add(card);
        rankBehaviour.RankSum(); 
    }

    void RemoveWeatherCards()
    {
        string[] rankNames = {"RankClose P", "RankRanged P", "RankSiege P", "RankClose En", "RankRanged En", "RankSiege En"};
        GameObject rank;
        RankBehaviour rankBehaviour;

        foreach (string rankName in rankNames)
        {
            rank = GameObject.Find(rankName);
            rankBehaviour = rank.GetComponent<RankBehaviour>();
            rankBehaviour.Clear();
            rankBehaviour.RankSum();
        }
    }
}
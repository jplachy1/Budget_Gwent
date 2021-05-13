using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [HideInInspector]
    public bool turn;

    public CardHolder playerHolder;
    public CardHolder enemyHolder;

    public GraveyardBehaviour playerGraveyard;
    public GraveyardBehaviour enemyGraveyard;

    public RankBehaviour RankCloseP;
    public RankBehaviour RankRangedP;
    public RankBehaviour RankSiegeP;
    public RankBehaviour RankCloseEn;
    public RankBehaviour RankRangedEn;
    public RankBehaviour RankSiegeEn;

    void Start()
    {
        turn = true;
    }

    void PlaceCard(RankBehaviour rank, GameObject CardGO)
    {
        Card card = CardGO.GetComponent<CardBehaviour>().card;

        rank.cards.Add(card);
        rank.RankSum();

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

        //cardHolder.RemoveCard(card);
        //cardHolder.ResizeDeck();
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
                while(weather.transform.childCount > 0)
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
        cardGO.transform.SetParent(RankGO.transform);
        cardGO.GetComponent<CardBehaviour>().isMovable = false;

        if (cardGO.GetComponent<CardBehaviour>().card.rank != Rank.Weather)
        {
            PlaceCard(RankGO.GetComponent<RankBehaviour>(), cardGO);
        }
        else
        {
            PlaceCard(RankGO.GetComponent<WeatherBehaviour>(), cardGO);
        }

        turn = !turn;
    }

    public void DecoyCard(GameObject DecoyGO,GameObject CardGO, GameObject RankGO)
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

    // GetCards GetCardHolder()
    // {
    //     if (turn)
    //     {
    //         return GameObject.Find("CardHolder P").GetComponent<GetCards>();
    //     }
    //     else
    //     {
    //         return GameObject.Find("CardHolder En").GetComponent<GetCards>();
    //     }
    // }
}

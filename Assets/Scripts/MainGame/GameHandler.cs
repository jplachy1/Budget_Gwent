using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [HideInInspector]
    public bool turn;
    void Start()
    {
        turn = true;
    }

    void PlaceCard(RankBehaviour rank, GameObject CardGO)
    {
        //GetCards cardHolder = GetCardHolder();
        Card card = CardGO.GetComponent<CardBehaviour>().card;

        rank.cardsInRank.Add(card);
        rank.RankSum();

        if (card.ability == Ability.Spy)
        {
            //cardHolder.DrawSpyCard();
        }

        //cardHolder.RemoveCard(card);
        //cardHolder.ResizeDeck();
    }

    void PlaceCard(WeatherBehaviour weather, GameObject CardGO)
    {
        //GetCards cardHolder = GetCardHolder();
        Card card = CardGO.GetComponent<CardBehaviour>().card;

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
            weather.Clear();
        }

        //cardHolder.RemoveCard(card);
    }

    public void PlaceCard(GameObject rankGO, GameObject cardGO)
    {
        turn = !turn;
        cardGO.transform.SetParent(rankGO.transform);
        cardGO.GetComponent<CardBehaviour>().isMovable = false;

        if (cardGO.GetComponent<CardBehaviour>().card.rank != Rank.Weather)
        {
            PlaceCard(rankGO.GetComponent<RankBehaviour>(), cardGO);
        }
        else
        {
            PlaceCard(rankGO.GetComponent<WeatherBehaviour>(), cardGO);
        }
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

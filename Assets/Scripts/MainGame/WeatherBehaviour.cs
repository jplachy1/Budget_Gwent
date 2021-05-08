using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherBehaviour : MonoBehaviour
{
    List<Card> cards = new List<Card>();
    public RankBehaviour RankCloseP;
    public RankBehaviour RankRangedP;
    public RankBehaviour RankSiegeP;
    public RankBehaviour RankCloseEn;
    public RankBehaviour RankRangedEn;
    public RankBehaviour RankSiegeEn;

    public void Close(Card _card)
    {
        cards.Add(_card);

        RankCloseP.cardsInRank.Add(_card);
        RankCloseEn.cardsInRank.Add(_card);

        RankCloseP.RankSum();
        RankCloseEn.RankSum();
    }

    public void Ranged(Card _card)
    {
        cards.Add(_card);
        
        RankRangedP.cardsInRank.Add(_card);
        RankRangedEn.cardsInRank.Add(_card);

        RankRangedP.RankSum();
        RankRangedEn.RankSum();
    }

    public void Siege(Card _card)
    {
        cards.Add(_card);

        RankSiegeP.cardsInRank.Add(_card);
        RankSiegeEn.cardsInRank.Add(_card);

        RankSiegeP.RankSum();
        RankSiegeEn.RankSum();
    }

    public void Clear()
    {
        foreach (Card card in cards)
        {
            RankCloseP.cardsInRank.Remove(card);
            RankCloseEn.cardsInRank.Remove(card);
            RankRangedP.cardsInRank.Remove(card);
            RankRangedEn.cardsInRank.Remove(card);
            RankSiegeP.cardsInRank.Remove(card);
            RankSiegeEn.cardsInRank.Remove(card);
        }
        
        RankCloseP.weathered = false;
        RankCloseEn.weathered = false;
        RankRangedP.weathered = false;
        RankRangedEn.weathered = false;
        RankSiegeP.weathered = false;
        RankSiegeEn.weathered = false;

        RankCloseP.RankSum();
        RankCloseEn.RankSum();
        RankRangedP.RankSum();
        RankRangedEn.RankSum();
        RankSiegeP.RankSum();
        RankSiegeEn.RankSum();

        cards.Clear();
    }

}

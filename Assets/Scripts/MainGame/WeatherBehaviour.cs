using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherBehaviour : MonoBehaviour
{
    public List<Card> cards = new List<Card>();
    public RankBehaviour RankCloseP;
    public RankBehaviour RankRangedP;
    public RankBehaviour RankSiegeP;
    public RankBehaviour RankCloseEn;
    public RankBehaviour RankRangedEn;
    public RankBehaviour RankSiegeEn;

    public GraveyardBehaviour playerGraveyard;
    public GraveyardBehaviour enemyGraveyard;

    public void Close(Card _card)
    {
        cards.Add(_card);

        RankCloseP.weathered = true;
        RankCloseEn.weathered = true;

        RankCloseP.RankSum();
        RankCloseEn.RankSum();
    }

    public void Ranged(Card _card)
    {
        cards.Add(_card);
        
        RankRangedP.weathered = true;
        RankRangedEn.weathered = true;

        RankRangedP.RankSum();
        RankRangedEn.RankSum();
    }

    public void Siege(Card _card)
    {
        cards.Add(_card);

        RankSiegeP.weathered = true;
        RankSiegeEn.weathered = true;

        RankSiegeP.RankSum();
        RankSiegeEn.RankSum();
    }

    public void Clear()
    {
        
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
    }

    public void Scorch()
    {
        List<Card> cardsToScorch = GetScorchableCards();

        foreach (Card _card in cardsToScorch)
        {
            if (RankCloseP.cards.Contains(_card))
            {
                GameObject _CardGO = GameObject.Find("RankClose P/" + _card.Name); 
                playerGraveyard.MoveToGraveyard(_CardGO, RankCloseP.gameObject);
                RankCloseP.RankSum();
            }
            else if (RankRangedP.cards.Contains(_card))
            {
                GameObject _CardGO = GameObject.Find("RankRanged P/" + _card.Name); 
                playerGraveyard.MoveToGraveyard(_CardGO, RankRangedP.gameObject);
                RankRangedP.RankSum();
            }
            else if (RankSiegeP.cards.Contains(_card))
            {
                GameObject _CardGO = GameObject.Find("RankSiege P/" + _card.Name); 
                playerGraveyard.MoveToGraveyard(_CardGO, RankSiegeP.gameObject);
                RankSiegeP.RankSum();
            }
            else if (RankCloseEn.cards.Contains(_card))
            {
                GameObject _CardGO = GameObject.Find("RankClose En/" + _card.Name); 
                enemyGraveyard.MoveToGraveyard(_CardGO, RankCloseEn.gameObject);
                RankCloseEn.RankSum();
            }
            else if (RankRangedEn.cards.Contains(_card))
            {
                GameObject _CardGO = GameObject.Find("RankRanged En/" + _card.Name); 
                enemyGraveyard.MoveToGraveyard(_CardGO, RankRangedEn.gameObject);
                RankRangedEn.RankSum();
            }
            else if (RankSiegeEn.cards.Contains(_card))
            {
                GameObject _CardGO = GameObject.Find("RankSiege En/" + _card.Name); 
                enemyGraveyard.MoveToGraveyard(_CardGO, RankSiegeEn.gameObject);
                RankSiegeEn.RankSum();
            }
        }
    }

    List<Card> GetScorchableCards()
    {
        List<Card> ranksBiggestCards = new List<Card>();
        List<Card> boardsBiggestCards = new List<Card>();
        int max;

        if (GetRanksBiggestCard(RankCloseP) != null)
        {
            ranksBiggestCards.AddRange(GetRanksBiggestCard(RankCloseP));
        }
        if (GetRanksBiggestCard(RankCloseEn) != null)
        {
            ranksBiggestCards.AddRange(GetRanksBiggestCard(RankCloseEn));
        }
        if (GetRanksBiggestCard(RankRangedP) != null)
        {
            ranksBiggestCards.AddRange(GetRanksBiggestCard(RankRangedP));
        }
        if (GetRanksBiggestCard(RankRangedEn) != null)
        {
            ranksBiggestCards.AddRange(GetRanksBiggestCard(RankRangedEn));
        }
        if (GetRanksBiggestCard(RankSiegeP) != null)
        {
            ranksBiggestCards.AddRange(GetRanksBiggestCard(RankSiegeP));
        }
        if (GetRanksBiggestCard(RankSiegeEn) != null)
        {
            ranksBiggestCards.AddRange(GetRanksBiggestCard(RankSiegeEn));
        }

        max = ranksBiggestCards[0].RankDmg;

        for (int i = 0; i < ranksBiggestCards.Count; i++)
        {
            if (ranksBiggestCards[i].RankDmg > max)
            {
                max = ranksBiggestCards[i].RankDmg;
            }
        }

        for (int i = 0; i < ranksBiggestCards.Count; i++)
        {
            if (ranksBiggestCards[i].RankDmg == max)
            {
                boardsBiggestCards.Add(ranksBiggestCards[i]);
            }
        }
        return boardsBiggestCards;

    }

    List<Card> GetRanksBiggestCard(RankBehaviour _rankBehaviour)
    {
        List<Card> _cards = _rankBehaviour.cards;
        List<Card> _biggestCards = new List<Card>();
        if (_cards.Count > 0)
        {
            int max = _cards[0].RankDmg;
            for (int i = 0; i < _cards.Count; i++)
            {
                if (_cards[i].RankDmg > max)
                {
                    max = _cards[i].RankDmg;
                }
            }

            for (int i = 0; i < _cards.Count; i++)
            {
                if (_cards[i].RankDmg == max)
                {
                    _biggestCards.Add(_cards[i]);
                }
            }
            return _biggestCards;
        }
        return null;
    }
}

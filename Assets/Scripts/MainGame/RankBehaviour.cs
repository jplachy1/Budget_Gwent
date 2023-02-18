using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class RankBehaviour : MonoBehaviour
{
    public List<Card> cards = new List<Card>();
    public Text rankDamageText;
    int rankDamage;
    bool horned = false;
    public bool globalHorned = false;
    public bool weathered = false;

    void Start()
    {
        rankDamageText.text = "0";
    }

    void DisplayDamage()
    {
        rankDamage = 0;
        foreach (Card card in cards)
        {
            if (card.Rank != Rank.Weather)
                rankDamage += card.RankDmg;
        }

        rankDamageText.text = rankDamage.ToString();
    }

    public void RankSum()
    {
        List<Card> bonders = new List<Card>();
        int moralers = 0;

        foreach (Card card in cards)
        {
            card.RankDmg = card.BaseDmg;
        }

        foreach (Card card in cards)
        {
            if (card.Ability == Ability.Bond)
            {
                bonders.Add(card);
            }
            else if (card.Ability == Ability.Morale)
            {
                moralers++;
            }
            else if (card.Ability == Ability.Horn)
            {
                horned = true;
            }
            else if (card.Rank == Rank.Weather)
            {
                weathered = true;
            }
        }

        if (weathered)
        {
            foreach (Card card in cards)
            {
                if (card.IsHero == false & card.Rank != Rank.Weather)
                {
                    card.RankDmg = 1;
                }
            }
        }


        if (bonders.Count > 1)
        {
            Bond(bonders);
        }


        if (moralers > 0)
        {
            Morale(moralers);
        }

        if ((horned & !globalHorned) || (!horned & globalHorned))
        {
            foreach (Card card in cards)
            {
                if (card.IsHero == false & card.Ability != Ability.Horn)
                {
                    card.RankDmg = card.RankDmg * 2;
                }
            }
        }
        if (horned & globalHorned)
        {
            foreach (Card card in cards)
            {
                if (card.Ability == Ability.Horn)
                {
                    card.RankDmg = card.BaseDmg * 2;
                }
            }
        }

        DisplayDamage();
        ResizeDeck();
    }

    void Bond(List<Card> _bonders)
    {
        List<Card> toBond = new List<Card>();
        _bonders = _bonders.OrderBy(o => o.Name).ToList();
        Card prev = _bonders[0];

        for (int i = 0; i < _bonders.Count; i++)
        {
            if (_bonders[i].Name == prev.Name)
            {
                toBond.Add(_bonders[i]);
                prev = _bonders[i];
            }
            else
            {
                if (toBond.Count > 1)
                {
                    if (horned)
                    {
                        if (weathered)
                        {
                            foreach (Card _card in toBond)
                            {
                                _card.RankDmg = (1 * toBond.Count) * 2;
                            }
                        }
                        else
                        {
                            foreach (Card _card in toBond)
                            {
                                _card.RankDmg = (_card.BaseDmg * toBond.Count) * 2;
                            }
                        }
                    }
                    else
                    {
                        if (weathered)
                        {
                            foreach (Card _card in toBond)
                            {
                                _card.RankDmg = 1 * toBond.Count;
                            }
                        }
                        else
                        {
                            foreach (Card _card in toBond)
                            {
                                _card.RankDmg = _card.BaseDmg * toBond.Count;
                            }
                        }

                    }

                }
                else if (toBond.Count == 1 & horned)
                {
                    if (weathered)
                    {
                        toBond[0].RankDmg = 1 * 2;
                    }
                    else
                    {
                        toBond[0].RankDmg = toBond[0].BaseDmg * 2;
                    }
                }
                toBond = new List<Card>();
                toBond.Add(_bonders[i]);
                prev = _bonders[i];
            }
        }

        if (toBond.Count > 1)
        {
            if (horned)
            {
                if (weathered)
                {
                    foreach (Card _card in toBond)
                    {
                        _card.RankDmg = (1 * toBond.Count) * 2;
                    }
                }
                else
                {
                    foreach (Card _card in toBond)
                    {
                        _card.RankDmg = (_card.BaseDmg * toBond.Count) * 2;
                    }
                }
            }
            else
            {
                if (weathered)
                {
                    foreach (Card _card in toBond)
                    {
                        _card.RankDmg = 1 * toBond.Count;
                    }
                }
                else
                {
                    foreach (Card _card in toBond)
                    {
                        _card.RankDmg = _card.BaseDmg * toBond.Count;
                    }
                }
            }

        }
        else if (toBond.Count == 1 & horned)
        {
            toBond[0].RankDmg = toBond[0].BaseDmg * 2;
        }
    }

    void Morale(int _moralers)
    {
        foreach (Card card in cards)
        {
            if (!card.IsHero && card.Ability != Ability.Morale && card.Rank != Rank.Weather)
            {
                card.RankDmg += _moralers;
            }
            if (card.Ability == Ability.Morale)
            {
                card.RankDmg += _moralers - 1;
            }
        }
    }

    public int RankScore()
    {
        return rankDamage;
    }

    void ResizeDeck()
    {
        if (cards.Count > 10)
        {
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            GridLayoutGroup gridLayoutGroup = gameObject.GetComponent<GridLayoutGroup>();
            float width = rectTransform.sizeDelta.x;
            gridLayoutGroup.cellSize = new Vector2(width / cards.Count, gridLayoutGroup.cellSize.y);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public GameHandler gh;
    public CardHolder cardHolder;
    Card cardToPlay;

    void Start()
    {
        
    }

    void Update()
    {
        Play();
    }

    void Play()
    {
        if (gh.turn == false)
        {
            cardToPlay = PickCard();

            GameObject cardObject = GameObject.Find("CardHolder En/" + cardToPlay.name);
            //cardHolder.cards.RemoveAll(c => c.ID == cardToPlay.ID);
            Debug.Log("Placing Card " + cardToPlay.name);
            gh.PlaceCard(GetRank(cardToPlay), cardObject);
        }
    }

    Card PickCard()
    {
        Card candidate;
        
        candidate = cardHolder.cards[Random.Range(0, cardHolder.cards.Count)];
        return candidate;
    }


    GameObject GetRank(Card _card)
    {
        GameObject cardGO = GameObject.Find("CardHolder En/" + _card.name);
        string cardRank = _card.rank.ToString();
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

        return GameObject.Find(cardRank);
    }



}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CardHolder : MonoBehaviour
{
    public GameHandler gh;
    public List<Card> cards;
    public Deck deck;


    void Start()
    {
        // for (int i = 0; i < 10; i++)
        // {
        //     Card card = deck.DrawCard();
        //     cards.Add(card);
        //     gh.SpawnCard(card, gameObject);
        // }
    }

    public void DrawSpyCard()
    {
        Card _card;
        for (int i = 0; i < 2; i++)
        {
            _card = deck.DrawCard();
            if (_card != null)
            {
                cards.Add(_card);
                gh.SpawnCard(_card, gameObject);
            }
        }
    }

    public void GetCard(GameObject CardGO)
    {
        Card _card = CardGO.GetComponent<CardBehaviour>().card;
        cards.Add(_card);
        cards = cards.OrderBy(o => o.BaseDmg).ThenBy(o => o.Name).ToList();

        CardGO.transform.SetParent(gameObject.transform);
        CardGO.transform.SetSiblingIndex(cards.IndexOf(_card));
        CardGO.GetComponent<CardBehaviour>().card.RankDmg = CardGO.GetComponent<CardBehaviour>().card.BaseDmg;
        cards.Add(CardGO.GetComponent<CardBehaviour>().card);
    }

    public List<GameObject> GetMusterCards(string[] _group)
    {
        List<GameObject> musters = new List<GameObject>();

        foreach (Transform CardGO in transform)
        {
            Card card = CardGO.GetComponent<CardBehaviour>().card;
            foreach (string musterID in _group)
            {
                if (card.Id == musterID)
                {
                    musters.Add(CardGO.gameObject);
                }
            }
        }
        return musters;
    }
}
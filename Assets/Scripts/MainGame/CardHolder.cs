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
        for (int i = 0; i < 10; i++)
        {
            cards.Add(deck.DrawCard());
        }
        cards = cards.OrderBy(o => o.baseDmg).ThenBy(o => o.name).ToList();

        for (int i = 0; i < 10; i++)
        {
            gh.SpawnCard(cards[i], gameObject, i);
        }
    }

    public void DrawSpyCard()
    {
        Card _card;
        for (int i = 0; i < 2; i++)
        {
            _card = deck.DrawCard();
            if(_card != null)
            {
                cards.Add(_card);
                cards = cards.OrderBy(o => o.baseDmg).ThenBy(o => o.name).ToList();
                gh.SpawnCard(_card, gameObject, cards.IndexOf(_card));
            }
            
        }
    }

    public void GetCard(GameObject CardGO)
    {
        Card _card = CardGO.GetComponent<CardBehaviour>().card;
        cards.Add(_card);
        cards = cards.OrderBy(o => o.baseDmg).ThenBy(o => o.name).ToList();

        CardGO.transform.SetParent(gameObject.transform);
        CardGO.transform.SetSiblingIndex(cards.IndexOf(_card));
        CardGO.GetComponent<CardBehaviour>().card.rankDmg = CardGO.GetComponent<CardBehaviour>().card.baseDmg;
        cards.Add(CardGO.GetComponent<CardBehaviour>().card);
    }

    void ResizeDeck()
    {
        if(cards.Count > 10)
        {
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            GridLayoutGroup gridLayoutGroup = gameObject.GetComponent<GridLayoutGroup>();
            float width = rectTransform.sizeDelta.x;
            gridLayoutGroup.cellSize = new Vector2(width / cards.Count, gridLayoutGroup.cellSize.y);
        }
    }

    public List<GameObject> GetMusterCards(string[] _group)
    {
        List<GameObject> musters = new List<GameObject>();

        foreach (Transform CardGO in transform)
        {
            Card card = CardGO.GetComponent<CardBehaviour>().card;
            foreach (string musterID in _group)
            {
                if (card.ID == musterID)
                {
                    musters.Add(CardGO.gameObject);
                }
            }
        }
        return musters;
    }

    


}
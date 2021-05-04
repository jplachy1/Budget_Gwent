using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDecks : MonoBehaviour
{
    public bool isShowable;
    static List<List<string>> decks = DeckLoader.decks;
    static List<Card> cardList = DeckLoader.cardList;
    public GameObject deckPrefab;

    void Start()
    {
        List<Card> deck = new List<Card>();
        if (decks != null)
        {
            foreach(List<string> cardNames in decks)
            {
                deck = GetDeck(cardNames);
                GameObject deckGO = Instantiate(deckPrefab, gameObject.transform);
                deckGO.transform.GetChild(1).gameObject.GetComponent<Text>().text = cardNames[0];
                if (isShowable)
                {
                    deckGO.GetComponent<DeckButton>().GetDeck(deck);
                }
                else
                {
                    deckGO.GetComponent<DeckToPlay>().GetDeck(deck);
                }
            }
        }
    }

    List<Card> GetDeck(List<string> deckNames)
    {
        List<Card> _deck = new List<Card>();
        for (int i = 1; i < deckNames.Count; i++)
        {
            Card card = cardList.Find(x => x.name == deckNames[i]);
            _deck.Add(card);
        }
        return _deck;
    }

}

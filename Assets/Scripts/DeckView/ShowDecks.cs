using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class ShowDecks : MonoBehaviour
{
    public bool isShowable;
    SaveData save = new SaveData();
    static List<List<string>> decks = new List<List<string>>();
    static List<Card> cardList = new List<Card>();
    public GameObject deckPrefab;

    void Start()
    {
        decks = save.Load("decks.xd");
        cardList = Resources.LoadAll<Card>("Cards").ToList();
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
            Card card = cardList.Find(x => x.ID == deckNames[i]);
            _deck.Add(card);
        }
        return _deck;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class ShowDecks : MonoBehaviour
{
    public bool isShowable;
    SaveData save = new SaveData();
    static List<List<string>> decks = DeckLoader.decks;
    static List<Card> cardList = DeckLoader.cardList;
    static List<CaptainCard> captainCardList = DeckLoader.captainCardList;
    public GameObject deckPrefab;

    void Start()
    {
        //decks = save.Load("decks.xd");
        //cardList = Resources.LoadAll<Card>("Cards").ToList();
        List<Card> deck = new List<Card>();
        CaptainCard captainCard;

        if (decks != null)
        {
            foreach (List<string> cardNames in decks)
            {
                deck = GetDeck(cardNames);
                captainCard = GetCaptainCard(cardNames.Last());
                GameObject deckGO = Instantiate(deckPrefab, gameObject.transform);
                deckGO.transform.GetChild(1).gameObject.GetComponent<Text>().text = cardNames[0];
                if (isShowable)
                {
                    deckGO.GetComponent<DeckButton>().GetDeck(deck);
                    deckGO.GetComponent<DeckButton>().captainCard = captainCard;
                }
                else
                {
                    deckGO.GetComponent<DeckToPlay>().GetDeck(deck);
                    deckGO.GetComponent<DeckToPlay>().captainCard = captainCard;
                }
            }
        }
    }

    List<Card> GetDeck(List<string> deckNames)
    {
        List<Card> _deck = new List<Card>();
        for (int i = 1; i < deckNames.Count - 1; i++)
        {
            Card card = cardList.Find(x => x.Id == deckNames[i]);
            _deck.Add(card);
        }
        return _deck;
    }

    CaptainCard GetCaptainCard(string captainCardName)
    {
        return captainCardList.Find(x => x.ID == captainCardName);
    }

}

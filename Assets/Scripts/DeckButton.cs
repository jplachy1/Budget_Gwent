using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckButton : MonoBehaviour
{
    bool isShowable;
    ShowDeckCards showDeckCards;
    List<Card> deck = new List<Card>();
    Button deckButton;

    void Start()
    {
        showDeckCards = GameObject.Find("Content").GetComponent<ShowDeckCards>();
        deckButton = gameObject.transform.GetChild(0).GetComponent<Button>();
        deckButton.onClick.AddListener(ShowCards);
    }

    public void GetDeck(List<Card> _deck)
    {
        deck = _deck;
    }

    void ShowCards()
    {
        showDeckCards.RefreshDisplay(deck);
    }


}

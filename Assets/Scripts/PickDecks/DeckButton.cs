using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckButton : MonoBehaviour
{
    bool isShowable;
    ShowDeckCards showDeckCards;
    ShowCaptainCard showCaptainCard;
    List<Card> deck = new List<Card>();
    public CaptainCard captainCard;
    Button deckButton;

    void Start()
    {
        showDeckCards = GameObject.Find("Content").GetComponent<ShowDeckCards>();
        showCaptainCard = GameObject.Find("CaptianCardView").GetComponent<ShowCaptainCard>();
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
        showCaptainCard.RefreshDisplay(captainCard);
    }


}

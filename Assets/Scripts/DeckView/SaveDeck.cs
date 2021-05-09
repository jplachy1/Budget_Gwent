using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveDeck : MonoBehaviour
{
    public CardScrollList cardScrollList;
    public Text deckName;
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(HandleClick);
    }

    void HandleClick()
    {
        SaveData sd = new SaveData();
        List<string> cardIDs = new List<string>();
        List<Card> deck = cardScrollList.cardList;

        cardIDs.Add(deckName.text);
        foreach(Card card in deck)
        {
            cardIDs.Add(card.ID);
        }

        sd.Save(cardIDs);
    }
}

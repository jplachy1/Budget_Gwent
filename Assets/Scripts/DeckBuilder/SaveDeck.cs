using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveDeck : MonoBehaviour
{
    public GameObject captainCardsHolder;
    public GameObject deckViewHolder;
    public Text deckName;
    public CaptainCardsView ccView;
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(HandleClick);
    }

    void HandleClick()
    {
        SaveData sd = new SaveData();
        List<string> cardIDs = new List<string>();
        List<Card> deck = GetScrollList().cardList;

        cardIDs.Add(deckName.text);
        foreach (Card card in deck)
        {
            cardIDs.Add(card.ID);
        }

        sd.Save(cardIDs);
    }

    CardScrollList GetScrollList()
    {
        foreach (Transform transform in deckViewHolder.transform)
        {
            if (transform.gameObject.activeSelf) return transform.gameObject.GetComponentInChildren<CardScrollList>();
        }
        return null;
    }

    string GetCaptainCardID()
    {
        foreach (Transform transform in captainCardsHolder.transform)
        {
            if (transform.gameObject.activeSelf) return transform.gameObject.GetComponentInChildren<CaptainCardsView>().pickedCaptainCardID;
        }
        return null;
    }


}

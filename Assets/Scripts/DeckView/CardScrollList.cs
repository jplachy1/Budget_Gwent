using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CardScrollList : MonoBehaviour
{
    [HideInInspector] public List<Card> cardList;
    public GameObject cardPrefab;
    public CardScrollList otherScrollList; 
    public bool main;
    public SimpleObjectPool cardObjectPool;

    void Start()
    {
        // if (main)
        // {
        //     cardList = Resources.LoadAll<Card>("Cards").ToList();
        //     cardList = cardList.OrderBy(o => o.baseDmg).ToList();
        //     SetupHeight();
        //     AddCardButtons();
        // }
        // else 
        // {
        //     cardList = new List<Card>();
        // }
    }

    public void GetFactionCards(List<Card> _cardList)
    {
        cardList = _cardList;
        SetupHeight();
        RefreshDisplay();
        
    }
    public void RefreshDisplay()
    {
        RemoveCardButtons();
        AddCardButtons();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            while(transform.childCount > 0)
            {
                GameObject toRemove = transform.GetChild(0).gameObject;
                Destroy(toRemove);
            }
            
        }
    }

    void AddCardButtons()
    {
        foreach (Card card in cardList)
        {
            GameObject cardGO = cardObjectPool.GetObject();
            cardGO.transform.SetParent(gameObject.transform);
            cardGO.GetComponent<Image>().sprite = card.artwork;
            cardGO.GetComponent<CardButton>().Setup(card, this);
            cardGO.GetComponent<RectTransform>().localScale = new Vector3(1f,1f,1f);
        }
    }

    public void RemoveCardButtons()
    {
        while (transform.childCount > 0) 
        {
            GameObject toRemove = transform.GetChild(0).gameObject;
            cardObjectPool.ReturnObject(toRemove);
        }
    }
    
    void AddCard(Card cardToAdd, CardScrollList cardList)
    {
        cardList.cardList.Add(cardToAdd);
    }

    void RemoveCard(Card cardToRemove, CardScrollList cardList)
    {
        for (int i = cardList.cardList.Count - 1; i >= 0; i--) 
        {
            if (cardList.cardList[i] == cardToRemove)
            {
                cardList.cardList.RemoveAt(i);
            }
        }
    }

    public void TransferCard(Card card)
    {
        AddCard(card, otherScrollList);
        RemoveCard(card, this);

        RefreshDisplay();
        otherScrollList.RefreshDisplay();
    }

    void SetupHeight()
    {
        int cardCount = cardList.Count;
        float height = 180f;
        float contentHeight = (cardCount / 3) * height;
        Vector2 currentHeight = gameObject.GetComponent<RectTransform>().sizeDelta;
        Vector2 vectorHeight = new Vector2(currentHeight.x, contentHeight);
        gameObject.GetComponent<RectTransform>().sizeDelta = vectorHeight;
    }

}
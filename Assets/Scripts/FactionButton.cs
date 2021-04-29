using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FactionButton : MonoBehaviour
{
    public CardScrollList cardScrollList;
    List<Card> allFactionCards = new List<Card>();

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(HandleClick);
    }

    public void AddFaction(List<Card> cards)
    {
        allFactionCards = cards;
    }

    public void HandleClick()
    {
        cardScrollList.GetFactionCards(allFactionCards);        
    } 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptainCardsView : MonoBehaviour
{
    List<CaptainCard> captainCards;
    [HideInInspector]
    public string pickedCaptainCardID;
    void Start()
    {

    }

    void ShowCaptainCards()
    {
        for (int i = 0; i < 4; i++)
        {
            transform.GetChild(i).GetComponent<Image>().sprite = captainCards[i].artwork;
            transform.GetChild(i).GetComponent<CaptainCardPick>().captainCard = captainCards[i];
        }
    }

    public void GetCaptainCards(List<CaptainCard> _captainCards)
    {
        captainCards = _captainCards;
        ShowCaptainCards();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraveyardBehaviour : MonoBehaviour
{
    public List<Card> cards = new List<Card>();
    public Text cardCountText; 

    void Update()
    {
        cardCountText.text = cards.Count.ToString();
    }

    public void MoveToGraveyard(GameObject CardGO, GameObject RankGO)
    {
        Card _card = CardGO.GetComponent<CardBehaviour>().card;
        if (RankGO.TryGetComponent<RankBehaviour>(out RankBehaviour rankBehaviour))
        {
            rankBehaviour.cards.Remove(_card);
        }
        else if (RankGO.TryGetComponent<WeatherBehaviour>(out WeatherBehaviour weatherBehaviour))
        {
            weatherBehaviour.cards.Remove(_card);
        }

        CardGO.transform.SetParent(transform);
        cards.Add(_card);
    }
}

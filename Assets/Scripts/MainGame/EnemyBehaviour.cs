using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public GameHandler gh;
    public CardHolder cardHolder;
    Card cardToPlay;

    void Start()
    {
        
    }

    void Update()
    {
        Play();
    }

    void Play()
    {
        if (gh.turn == false)
        {
            cardToPlay = PickCard();

            GameObject cardObject = GameObject.Find("CardHolder En/" + cardToPlay.name);
            //cardHolder.cards.RemoveAll(c => c.ID == cardToPlay.ID);
            Debug.Log("Placing Card " + cardToPlay.name);
            gh.PlaceCard(gh.GetRank(cardToPlay), cardObject);
        }
    }

    Card PickCard()
    {
        Card candidate;
        
        candidate = cardHolder.cards[Random.Range(0, cardHolder.cards.Count)];
        return candidate;
    }
}

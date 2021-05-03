using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckToPlay : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    GameObject output;

    void Start()
    {
        if (gameObject.transform.parent.name == "Panel P")
        {
            output = GameObject.Find("Player");
        }
        else
        {
            output = GameObject.Find("Enemy");
        }
        gameObject.GetComponent<Button>().onClick.AddListener(HandleClick);
    }

    public void GetDeck(List<Card> _deck)
    {   
        deck = _deck;
    }

    public void HandleClick()
    {
        if (output.transform.childCount == 0)
        {
            Instantiate(gameObject, output.transform);
        }
        else
        {
            Destroy(output.transform.GetChild(0).gameObject);
            Instantiate(gameObject, output.transform);
        }
        
    }
}

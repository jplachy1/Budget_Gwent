using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickDecksPlayButton : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public static List<Card> playerDeck = new List<Card>();
    public static CaptainCard playerCaptainCard;
    public static List<Card> enemyDeck = new List<Card>();
    public static CaptainCard enemyCaptainCard;
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(Play);
    }

    void Play()
    {
        playerDeck = player.GetComponentInChildren<DeckToPlay>().deck;
        playerCaptainCard = player.GetComponentInChildren<DeckToPlay>().captainCard;
        enemyDeck = enemy.GetComponentInChildren<DeckToPlay>().deck;
        enemyCaptainCard = enemy.GetComponentInChildren<DeckToPlay>().captainCard;

        Loader.Load(Loader.Scene.MainGame);
    }
}

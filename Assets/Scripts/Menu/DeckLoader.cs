using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckLoader : MonoBehaviour
{
    SaveData saveData = new SaveData();
    public static List<List<string>> decks = new List<List<string>>();
    public static List<Card> cardList;
    public static List<CaptainCard> captainCardList;
    void Start()
    {
        cardList = Resources.LoadAll<Card>("Cards").ToList();
        cardList = cardList.OrderBy(o => o.BaseDmg).ToList();
        captainCardList = Resources.LoadAll<CaptainCard>("Captain Cards").ToList();
        decks = saveData.Load("decks.xd");
    }

}

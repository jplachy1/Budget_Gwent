using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckLoader : MonoBehaviour
{
    SaveData saveData = new SaveData();
    public static List<List<string>> decks = new List<List<string>>();
    public static List<Card> cardList;
    void Start()
    {
        cardList = Resources.LoadAll<Card>("Cards").ToList();
        cardList = cardList.OrderBy(o => o.baseDmg).ToList();
        decks = saveData.Load("decks.xd");
    }

}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [HideInInspector]
    public List<Card> cardList;
    public FactionButton noButton;
    public FactionButton niButton;
    public FactionButton moButton;
    public FactionButton scButton;

    List<Card> northern = new List<Card>();
    List<Card> nilfgaard = new List<Card>();
    List<Card> monsters = new List<Card>();
    List<Card> scoiatael = new List<Card>();

    void Start()
    {
        cardList = Resources.LoadAll<Card>("Cards").ToList();
        cardList = cardList.OrderBy(o => o.baseDmg).ToList();

        SortCards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SortCards()
    {
        foreach(Card card in cardList)
        {
            if (card.faction == Faction.Neutral)
            {
                northern.Add(card);
                nilfgaard.Add(card);
                monsters.Add(card);
                scoiatael.Add(card);
            }
            else if (card.faction == Faction.Northern)
            {
                northern.Add(card);
            }
            else if (card.faction == Faction.Nilfgaard)
            {
                nilfgaard.Add(card);
            }
            else if (card.faction == Faction.Monsters)
            {
                monsters.Add(card);
            }
            else if (card.faction == Faction.Scoiatael)
            {
                scoiatael.Add(card);
            }
        }

        noButton.AddFaction(northern);
        niButton.AddFaction(nilfgaard);
        moButton.AddFaction(monsters);
        scButton.AddFaction(scoiatael);
    }
}

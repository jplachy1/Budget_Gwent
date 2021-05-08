using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Ability {None, Bond, Morale, Scorch, Spy, Medic, Agile, Muster, Horn, Close, Ranged, Siege, Clear};
public enum Faction {Neutral, Nilfgaard, Northern, Scoiatael, Monsters};
public enum Rank {Close, Ranged, Siege, Agile, Horn, Weather};
[CreateAssetMenu(fileName = "New Card", menuName = "Card")]

[System.Serializable]
public class Card : ScriptableObject
{
    
    public new string name;
    public Sprite artwork;
    public int baseDmg;
    [HideInInspector]
    public int rankDmg;
    public Rank rank;
    public bool isHero;
    public Faction faction;
    public Ability ability;
}


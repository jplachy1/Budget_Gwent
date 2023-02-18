using UnityEngine;


public enum Ability { None, Bond, Morale, Scorch, Spy, Medic, Agile, Muster, Horn, Close, Ranged, Siege, Clear };
public enum Faction { Neutral, Nilfgaard, Northern, Scoiatael, Monsters };
public enum Rank { Close, Ranged, Siege, Agile, Horn, Weather, Decoy };

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
[System.Serializable]
public class Card : ScriptableObject
{
    public string Id;

    public string Name;

    public Sprite Artwork;

    public Sprite LargeArtwork;

    public int BaseDmg;

    [HideInInspector]
    public int RankDmg;

    public Rank Rank;

    public bool IsHero;

    public Faction Faction;

    public Ability Ability;

    public bool IsUnit
    {
        get
        {
            return Rank == Rank.Close || Rank == Rank.Ranged || Rank == Rank.Siege || Rank == Rank.Agile;
        }
    }
}


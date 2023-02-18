using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Captain Card")]

[System.Serializable]
public class CaptainCard : ScriptableObject
{
    public string ID;
    public new string name;
    public string surname;
    public Sprite artwork;
    public Sprite largeArtwork;
    [TextArea(1, 15)]
    public string description;
    public Faction faction;

}


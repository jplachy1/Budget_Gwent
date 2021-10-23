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
    [TextArea(1,15)]
    public string descrption;
    public Faction faction;

}


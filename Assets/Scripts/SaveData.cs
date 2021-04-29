using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveData
{
    
    public void Save(List<Card> data)
    {
        List<List<string>> decks = new List<List<string>>();
        List<string> cardNames = new List<string>();
        foreach (Card card in data)
        {
            cardNames.Add(card.name);
        }
        decks.Add(cardNames);
        decks.Add(cardNames);
        using (Stream stream = File.Open("save.bin", FileMode.Create))
        {
            var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

            bformatter.Serialize(stream, decks);
        }
    }

    public void Load(string filename)
    {
        List<string> cardNames = new List<string>();
        List<List<string>> decks = new List<List<string>>();
        using (Stream stream = File.Open(filename, FileMode.Open))
        {
            var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

            decks = bformatter.Deserialize(stream) as List<List<string>>;
        }

        foreach (string card in cardNames)
        {
            Debug.Log(card);
        }
    }

    private string ListToText(List<Card> list)
    {
        string result = "";
        foreach(Card card in list)
        {
            Debug.Log(card.artwork);
        }
        return result;
    }
}

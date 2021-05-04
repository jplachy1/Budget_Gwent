using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveData
{
    
    public void Save(List<string> data)
    {
        DeckLoader.decks.Add(data);

        using (Stream stream = File.Open("decks.xd", FileMode.Create))
        {
            var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

            bformatter.Serialize(stream, DeckLoader.decks);
        }
    }

    public List<List<string>> Load(string filename)
    {
        if (File.Exists(filename))
        {
            using (Stream stream = File.Open(filename, FileMode.Open))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                return bformatter.Deserialize(stream) as List<List<string>>;
            }
        }
        return null;
    }


}

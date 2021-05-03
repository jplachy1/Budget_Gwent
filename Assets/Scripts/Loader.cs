using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene {MainGame, DeckView, DeckBuilder, Menu, PickDecks};

    public static void Load(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
}

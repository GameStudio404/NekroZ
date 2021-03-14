using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    private Dictionary<string, Player> players;

    public GameData(Dictionary<string, Player> p)
    {
        players = p;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private string name;
    private GameObject player;
    private Dictionary<string, GameObject> players = new Dictionary<string, GameObject>();

    public void addPlayers(string name, GameObject player)
    {
        players.Add(name, player);
    }

    public Dictionary<string, GameObject> getPlayers()
    {
        return players;
    }

    public GameObject getPlayer(string name)
    {
        Debug.Log(name);
        return players[name];
    }

    public Player(string newName, GameObject newPlayer)
    {
        name = newName;
        player = newPlayer;
        Debug.Log($"Player {name} created");
        addPlayers(name, player);
    }

    public GameObject getPlayer()
    {
        return player;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private string firstName;
    private Vector3 positions;
    private Sprite sprite;
    //    private Dictionary<string, GameObject> players = new Dictionary<string, GameObject>();

    public Player(string newName, Vector3 pos, Sprite s)
    {
        firstName = newName;
        positions = pos;
        sprite = s;
        Debug.Log($"Player {firstName} created");
    }

    public string GetName()
    {
        return firstName;
    }

    public Vector3 GetPositions()
    {
        return positions;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    public void SetName(string n)
    {
        firstName = n;
    }

    public void SetPositions(Vector3 p)
    {
        positions = p;
    }

    public void SetSprite(Sprite s)
    {
        sprite = s;
    }

    //public void addPlayers(string name, GameObject player)
    //{
    //    players.Add(name, player);
    //}

    //public Dictionary<string, GameObject> getPlayers()
    //{
    //    return players;
    //}

    //public GameObject getPlayer(string name)
    //{
    //    Debug.Log(name);
    //    return players[name];
    //}
}

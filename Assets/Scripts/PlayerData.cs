using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    private string name;
    private Vector3 positions;
    private Sprite sprite;

    public PlayerData(Player player)
    {
        name = player.GetName();
        positions = player.GetPositions();
        sprite = player.GetSprite();
    }
}

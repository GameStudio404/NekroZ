using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BackPackData
{
    public Dictionary<string, Material> backpack;

    public BackPackData(Dictionary<string, Material> bk)
    {
        backpack = bk;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Material
{
    public Sprite sprite = null;
    public string name = null;
    public int nb = 0;

    public Material(Sprite img, string newName, int newNb)
    {
        sprite = img;
        name = newName;
        nb = newNb;
        Debug.Log($"The {name} material has been created!");
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Material
{
    public int id = 0;
    public Sprite sprite = null;
    public string name = null;
    public int nb = 0;
    public int selected = 0;

    public Material(int newId, Sprite img, string newName, int newNb, int isSelected)
    {
        id = newId;
        sprite = img;
        name = newName;
        nb = newNb;
        selected = isSelected;
        Debug.Log($"The {name} material has been created!");
    }
}
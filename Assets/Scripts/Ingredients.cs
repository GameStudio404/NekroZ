using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeIngredient
{
    private Sprite sprite = null;
    private string name = null;
    private int nb = 0;

    public RecipeIngredient(Sprite img, string newName, int times)
    {
        sprite = img;
        name = newName;
        nb = times;
        Debug.Log($"Ingredient {name} created");
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    public string GetName()
    {
        return name;
    }

    public int GetNb()
    {
        return nb;
    }
}

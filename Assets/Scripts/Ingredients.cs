using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeIngredient
{
    public Sprite sprite = null;
    public string name = null;
    public int nb = 0;

    public RecipeIngredient(Sprite img, string newName, int times)
    {
        sprite = img;
        name = newName;
        nb = times;
        Debug.Log($"Ingredient {name} created");
    }
}

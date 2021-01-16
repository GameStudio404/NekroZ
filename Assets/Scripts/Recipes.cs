using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe
{
    public Sprite sprite = null;
    public string name = "Wood key";
    public List<RecipeIngredient> ingredients = null;
    public bool available = false;
    public int given = 0;

    public Recipe(Sprite img, string newName, List<RecipeIngredient> ri, bool isAvailable, int isGiven)
    {
        sprite = img;
        name = newName;
        ingredients = ri;
        available = isAvailable;
        given = isGiven;
        Debug.Log($"The {name} recipe has been created!");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe
{
    private Sprite sprite = null;
    private string name = "Wood key";
    private List<RecipeIngredient> ingredients = null;
    private bool available = false;
    private int given = 0;

    public Recipe(Sprite img, string newName, List<RecipeIngredient> ri, bool isAvailable, int isGiven)
    {
        sprite = img;
        name = newName;
        ingredients = ri;
        available = isAvailable;
        given = isGiven;
        Debug.Log($"The {name} recipe has been created!");
    }

    public string GetName()
    {
        return name;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    public List<RecipeIngredient> GetIngredients()
    {
        return ingredients;
    }

    public bool GetAvailable()
    {
        return available;
    }

    public int GetGiven()
    {
        return given;
    }

    public void SetGiven(int newVal)
    {
        given = newVal;
    }

    public void SetAvailable(bool newVal)
    {
        available = newVal;
    }

    public void SetSprite(Sprite newVal)
    {
        sprite = newVal;
    }
}

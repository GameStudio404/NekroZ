using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Recipe
{
    private string name = "Wood key";
    private List<RecipeIngredient> ingredients = null;
    private bool available = false;
    private int given = 0;

    public Recipe(string newName, List<RecipeIngredient> ri, bool isAvailable, int isGiven)
    {
        name = newName;
        ingredients = ri;
        available = isAvailable;
        given = isGiven;
    }

    public string GetName()
    {
        return name;
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
}

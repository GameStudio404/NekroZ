using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeIngredient
{
    public string name = null;

    public RecipeIngredient(string newName)
    {
        name = newName;
        Debug.Log($"Ingredient {name} created");
    }
}

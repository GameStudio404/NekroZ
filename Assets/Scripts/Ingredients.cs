using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RecipeIngredient
{
    private string name = null;
    private int nb = 0;

    public RecipeIngredient(string newName, int times)
    {
        name = newName;
        nb = times;
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

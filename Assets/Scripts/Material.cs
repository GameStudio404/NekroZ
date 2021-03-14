using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Material
{
    private int id = 0;
    private string name = null;
    private int nb = 0;
    private int selected = 0;
    private string refinedName = null;
    private int refiningTime = 0;
    private int refiningCost = 0;

    public Material(int newId, string newName, int newNb, int isSelected, string rName, int rTime, int rCost)
    {
        id = newId;
        name = newName;
        nb = newNb;
        selected = isSelected;
        refinedName = rName;
        refiningTime = rTime;
        refiningCost = rCost;
    }

    public int GetId()
    {
        return id;
    }

    public string GetName()
    {
        return name;
    }

    public int GetNb()
    {
        return nb;
    }

    public int GetSelected()
    {
        return selected;
    }

    public string GetRefinedName()
    {
        return refinedName;
    }

    public int GetRefiningTime()
    {
        return refiningTime;
    }

    public int GetRefiningCost()
    {
        return refiningCost;
    }

    public void SetId(int newVal)
    {
        id = newVal;
    }

    public void SetNb(int newVal)
    {
        nb = newVal;
    }

    public void SetSelected(int newVal)
    {
        selected = newVal;
    }
}
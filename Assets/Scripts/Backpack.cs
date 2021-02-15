using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack
{
    private Dictionary<string, Material> backpack;

    public Backpack()
    {
        backpack = new Dictionary<string, Material>();
        Debug.Log("Backpack initiated.");
    }

    public Dictionary<string, Material> GetBackpack()
    {
        return backpack;
    }
    
    public void AddMaterial(string id, Material material)
    {
        backpack.Add(id, material);
    }

    public void UpdateMaterial(string id, Material material)
    {
        backpack[id] = material;
    }

    public Material GetMaterial(string id)
    {
        return backpack[id];
    }
}

public class Material
{
    private int id = 0;
    private Sprite sprite = null;
    private string name = null;
    private int nb = 0;
    private int selected = 0;

    public Material(int newId, Sprite img, string newName, int newNb, int isSelected)
    {
        id = newId;
        sprite = img;
        name = newName;
        nb = newNb;
        selected = isSelected;
        Debug.Log($"The {name} material has been created!");
    }

    public int GetId()
    {
        return id;
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

    public int GetSelected()
    {
        return selected;
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
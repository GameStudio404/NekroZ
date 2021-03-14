using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Backpack : MonoBehaviour
{
    private Dictionary<string, Material> backpack;
    private GameObject BP, BB, BIT, panel, BC;
    private Sprite[] sprites;

    public Backpack(Dictionary<string, Material> bk, Sprite[] s)
    {
        backpack = bk;
        BP = GameObject.FindWithTag("BP");
        BB = GameObject.FindWithTag("BB");
        BIT = Resources.Load<GameObject>("utils/Templates/BIT/BIT");
        sprites = s;
        panel = GameObject.FindWithTag("Respawn");
        BC = GameObject.FindWithTag("BC");
    }

    public Dictionary<string, Material> GetBackpack()
    {
        return backpack;
    }

    public Material GetMaterial(string id)
    {
        return backpack[id];
    }

    public GameObject GetBP()
    {
        return BP;
    }

    public GameObject GetBB()
    {
        return BB;
    }

    public GameObject GetPanel() {
        return panel;
    }

    public Sprite[] GetSprites()
    {
        return sprites;
    }

    public void SetBackpack(Dictionary<string, Material> bk)
    {
        backpack = bk;
    }

    public void SetBP(GameObject bp)
    {
        BP = bp;
    }

    public void SetBB(GameObject bb)
    {
        BB = bb;
    }

    public void AddMaterial(string id, Material material)
    {
        backpack.Add(id, material);
    }

    public void UpdateMaterial(string id, Material material)
    {
        backpack[id] = material;
    }

    public void RemoveMaterial(string name)
    {
        backpack.Remove(name);
    }

    //display the Materials on the screen
    public void Generate(Dictionary<string, Sprite> s)
    {
        int i = 0;
        int j = 0;
        foreach (KeyValuePair<string, Material> entry in backpack)
        {
            //Debug.Log($"mat: {entry.Key}, nb: {entry.Value.GetNb()}");
            if (entry.Value.GetNb() > 0)
            {
                int diff = entry.Value.GetId() - (entry.Value.GetId() - i);
                TagHelper.AddTag($"BB{i}");
                var addButton = Instantiate(BIT);
                addButton.transform.SetParent(BC.transform);
                addButton.transform.tag = $"BB{i}";
                addButton.transform.name = $"BB{i}";
                addButton.transform.localScale = Vector3.one;
                addButton.transform.GetChild(0).GetComponentInChildren<Image>().sprite = s[entry.Value.GetName()];
                addButton.transform.GetChild(1).GetComponentInChildren<Text>().text = entry.Value.GetNb().ToString();
                addButton.transform.GetChild(2).GetComponentInChildren<Text>().text = entry.Value.GetSelected() > 0 ?
                    entry.Value.GetSelected().ToString() : "";
                addButton.transform.GetChild(3).GetComponentInChildren<Text>().text = entry.Value.GetRefiningTime().ToString();
                addButton.GetComponent<Button>().onClick.AddListener(
                        () => selection(entry, diff)
                    );
                i++;
            }
            j++;
        }
    }
    
    //display the number of selected materials
    private void selection(KeyValuePair<string, Material> Material, int id)
    {
        Material.Value.SetSelected(Material.Value.GetSelected() + 1);
        GameObject ob = GameObject.Find($"BB{id}");
        ob.transform.GetChild(2).GetComponentInChildren<Text>().text = Material.Value.GetSelected().ToString();
    }

    //update the backpack (on the screen) to display all the current Materials
    public void UpdateSelf(Dictionary<string, Sprite> s)
    {
        Clear();
        Generate(s);
    }

    //update the backpack (on the screen), to only display the required material for the recipe
    public void UpdateForRecipe(KeyValuePair<string, Recipe> recipe, Dictionary<string, Sprite> s)
    {
        BB.GetComponent<Image>().sprite = sprites[3];
        if (!panel.activeSelf)
            BB.GetComponent<Button>().onClick.Invoke();
        Clear();
        int j = 0;
        foreach (KeyValuePair<string, Material> Material in backpack)
        {
            if (j < recipe.Value.GetIngredients().Count)
            {
                if (Material.Key == recipe.Value.GetIngredients()[j].GetName())
                {
                    Material.Value.SetId(j);
                    var addButton = Instantiate(BIT);
                    addButton.transform.SetParent(BC.transform);
                    addButton.transform.tag = $"BB{j}";
                    addButton.transform.name = $"BB{j}";
                    addButton.transform.localScale = Vector3.one;
                    addButton.transform.GetChild(0).GetComponentInChildren<Image>().sprite = s[Material.Value.GetName()];
                    addButton.transform.GetChild(1).GetComponentInChildren<Text>().text = Material.Value.GetNb().ToString();
                    addButton.transform.GetChild(2).GetComponentInChildren<Text>().text = Material.Value.GetSelected() > 0 ?
                        Material.Value.GetSelected().ToString() : "";
                    addButton.transform.GetChild(3).GetComponentInChildren<Text>().text = Material.Value.GetRefiningTime().ToString();
                    addButton.GetComponent<Button>().onClick.AddListener(
                            () => selection(Material, Material.Value.GetId())
                        );
                    j++;
                }
            }
        }
    }

    //update the backpack(on the screen), to only display the refineable materials
    public void UpdateForRefine(Dictionary<string, Sprite> s)
    {
        BB.GetComponent<Image>().sprite = sprites[3];
        if (!panel.activeSelf)
            BB.GetComponent<Button>().onClick.Invoke();
        int i = 0;
        int j = 0;
        foreach (KeyValuePair<string, Material> entry in backpack)
        {
            var current = TagHelper.AddTag($"BB{i}") ? GameObject.FindWithTag($"BB{i}") : null;
            if (current != null)
            {
                int nb = int.Parse(current.transform.GetChild(1).GetComponentInChildren<Text>().text);
                int selected = int.Parse(current.transform.GetChild(2).GetComponentInChildren<Text>().text == ""? "0"
                    : current.transform.GetChild(2).GetComponentInChildren<Text>().text);
                if (nb == 0)
                    entry.Value.SetNb(0);
                else if (selected > 0)
                    entry.Value.SetNb(entry.Value.GetNb() - selected);
                Destroy(current);
            }
            if (entry.Value.GetRefinedName() != null && entry.Value.GetNb() > 0)
            {
                TagHelper.AddTag($"BB{j}");
                var addButton = Instantiate(BIT);
                addButton.transform.SetParent(BC.transform);
                addButton.transform.tag = $"BB{j}";
                addButton.transform.name = $"BB{j}";
                addButton.transform.localScale = Vector3.one;
                addButton.transform.GetChild(0).GetComponentInChildren<Image>().sprite = s[entry.Value.GetName()];
                addButton.transform.GetChild(1).GetComponentInChildren<Text>().text = entry.Value.GetNb().ToString();
                addButton.transform.GetChild(2).GetComponentInChildren<Text>().text = entry.Value.GetSelected() > 0 ?
                    entry.Value.GetSelected().ToString() : "";
                addButton.transform.GetChild(3).GetComponentInChildren<Text>().text = entry.Value.GetRefiningTime().ToString();
                addButton.GetComponent<Button>().onClick.AddListener(
                        () => selection(entry, entry.Value.GetId())
                    );
                j++;
            }
            i++;
        }
        Save();
    }

    //display on the stdio the content of the backpack
    public void Print()
    {
        foreach (KeyValuePair<string, Material> entry in backpack)
        {
            Debug.Log(entry.Key);
        }
    }

    //get the score of the requested backpack
    public int Score()
    {
        int count = 0;
        foreach (KeyValuePair<string, Material> entry in backpack)
        {
            count += (entry.Value.GetNb() + 1);
        }
        return count;
    }

    //remove every material in the backpack (only on the screen)
    public void Clear()
    {
        for (int i = 0; i < backpack.Count; i++)
        {
            if (TagHelper.AddTag($"BB{i}"))
                Destroy(GameObject.FindWithTag($"BB{i}"));
            else return;
        }
    }

    public void Display()
    {
        if (panel.activeSelf)
        {
            BB.GetComponent<Image>().sprite = sprites[0];
        }
        else
        {
            BB.GetComponent<Image>().sprite = sprites[3];
        }
        panel.transform.gameObject.SetActive(!panel.activeSelf);
    }

    public void Save()
    {
        SaveSystem.SaveBackPack(backpack);
    }

    public void Load()
    {
        BackPackData data = SaveSystem.LoadBackPack();
        if (data.backpack != null)
        {
            backpack = data.backpack;
        }
    }
}
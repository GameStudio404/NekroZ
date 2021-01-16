using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillData : MonoBehaviour
{
    public GameObject Modal;
    public GameObject BIT;
    public GameObject RLT;
    public GameObject contentR;
    public GameObject contentB;
    public GameObject contentC;
    public GameObject ChosenRecipeImage;
    public Dictionary<string, Sprite> ima = new Dictionary<string, Sprite>();
    public List<Sprite> ItemImages = new List<Sprite>();
    public List<Sprite> RecipesImages = new List<Sprite>();
    public List<Button> IngredientTable = new List<Button>();
    public List<List<RecipeIngredient>> ingredients = new List<List<RecipeIngredient>>();
    public Dictionary<string, Material> backpack = new Dictionary<string, Material>();
    public Dictionary<string, Recipe> recipes = new Dictionary<string, Recipe>();
    public GameObject Craft;
    static int j = 0;
    public GameObject addCraft;
    public List<string> matNames = new List<string>();

    void Start()
    {
        Modal = GameObject.FindWithTag("Workshop modal");
        Modal.gameObject.SetActive(true);
        Craft = GameObject.FindWithTag("Craft");
        Craft.gameObject.SetActive(false);
        List<string> names = new List<string>() {
            "Wood key",
            "Iron key",
            "Gold key"
        };
        List<int> matNb = new List<int>()
        {
            10,
            10,
            10,
            10,
            10
        };
        ingredients.Add(new List<RecipeIngredient>() {
            new RecipeIngredient(ItemImages[0], "wood" , 2)
        });
        ingredients.Add(new List<RecipeIngredient>() {
            new RecipeIngredient(ItemImages[2], "copper" , 2),
            new RecipeIngredient(ItemImages[3], "iron" , 1)
        });
        ingredients.Add(new List<RecipeIngredient>() {
            new RecipeIngredient(ItemImages[0], "wood" , 3),
            new RecipeIngredient(ItemImages[1], "coal" , 3),
            new RecipeIngredient(ItemImages[2], "copper" , 3),
            new RecipeIngredient(ItemImages[3], "iron" , 2),
            new RecipeIngredient(ItemImages[4], "gold" , 1)
        });
        int i = 0;
        while (i < names.Count)
        {
            var name = names[i];
            var sprite = RecipesImages[i];
            var tempI = new List<RecipeIngredient>();
            int j = 0;
            while (j < ingredients[i].Count)
            {
                tempI.Add(ingredients[i][j++]);
            }
            j = 0;
            recipes.Add(name, new Recipe(sprite, name, tempI, false, 0));
            i++;
        }
        i = 0;
        while (i < matNb.Count)
        {
            Material material = new Material(i, ItemImages[i], matNames[i], matNb[i], 0);
            backpack.Add(matNames[i++], material);
        }
        generateRecipes(recipes);
        generateBackpack(backpack, "BIT");
        Destroy(RLT);
    }

    void generateRecipes(Dictionary<string, Recipe> recipes)
    {
        foreach (var elem in recipes)
        {
            var addButton = Instantiate(RLT);
            addButton.transform.SetParent(contentR.transform);
            addButton.transform.localScale = Vector3.one;
            addButton.GetComponentInChildren<Text>().text = elem.Key;
            addButton.transform.GetChild(0).GetComponentInChildren<Image>().sprite = elem.Value.sprite;
            addButton.GetComponent<Button>().onClick.AddListener(
                    () => dispatch(elem)
                );
        }
    }

    void clearIngredientTable()
    {
        for (int i = 0; i < 9; i++)
        {
            GameObject ob = GameObject.FindWithTag($"RI{i}");
            ob.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
            ob.transform.GetChild(1).GetComponentInChildren<Text>().text = "";
            ob.transform.GetChild(2).GetComponentInChildren<Text>().text = "";
        }
    }

    void clearBackpack(int until)
    {
        int i = 1;
        GameObject ob;
        while (i < until)
        {
            ob = GameObject.FindWithTag($"BB{i}");
            Destroy(ob);
            i++;
        }
    }

    void updateBackpackForRecipe(Dictionary<string, Material> backpack, KeyValuePair<string, Recipe> recipe)
    {
        int i = 1;
        int j = 0;
        BIT = GameObject.FindWithTag("BB0");
        foreach (KeyValuePair<string, Material> Material in backpack)
        {
            GameObject ob = GameObject.FindWithTag($"BB{i}");
            Destroy(ob);
            if (j < recipe.Value.ingredients.Count)
            {
                if (Material.Key == recipe.Value.ingredients[j].name)
                {
                    Material.Value.id = j;
                    var addButton = Instantiate(BIT, transform);
                    addButton.transform.SetParent(contentB.transform);
                    addButton.transform.tag = ("BB" + j).ToString();
                    addButton.transform.localScale = Vector3.one;
                    addButton.transform.GetChild(0).GetComponentInChildren<Image>().sprite = Material.Value.sprite;
                    addButton.transform.GetChild(1).GetComponentInChildren<Text>().text = Material.Value.nb.ToString();
                    addButton.transform.GetChild(2).GetComponentInChildren<Text>().text = Material.Value.selected > 0 ?
                        Material.Value.selected.ToString() : "";
                    addButton.GetComponent<Button>().onClick.AddListener(
                            () => selection(Material, Material.Value.id)
                        );
                    j++;
                }
            }
            i++;
        }
        Destroy(BIT);
    }

    void generateBackpack(Dictionary<string, Material> backpack, string tag)
    {
        BIT = GameObject.FindWithTag(tag);
        int i = 0;
        foreach (var entry in backpack)
        {
            var addButton = Instantiate(BIT, transform);
            addButton.transform.SetParent(contentB.transform);
            addButton.transform.tag = ("BB" + i).ToString();
            addButton.transform.localScale = Vector3.one;
            addButton.transform.GetChild(0).GetComponentInChildren<Image>().sprite = entry.Value.sprite;
            addButton.transform.GetChild(1).GetComponentInChildren<Text>().text = entry.Value.nb.ToString();
            addButton.transform.GetChild(2).GetComponentInChildren<Text>().text = entry.Value.selected > 0 ?
                entry.Value.selected.ToString() : "";
            addButton.GetComponent<Button>().onClick.AddListener(
                    () => selection(entry, entry.Value.id)
                );
            i++;
        }
        Destroy(BIT);
    }

    public void closeModal()
    {
        Modal.gameObject.SetActive(false);
    }

    void selection(KeyValuePair<string, Material> Material, int id)
    {
        Material.Value.selected += 1;
        //backpack[Material.Key] = Material.Value;
        GameObject ob = GameObject.FindWithTag($"BB{id}");
        ob.transform.GetChild(2).GetComponentInChildren<Text>().text = Material.Value.selected.ToString();
    }

    void dispatch(KeyValuePair<string, Recipe> recipe)
    {
        Craft.gameObject.SetActive(false);
        Craft.GetComponent<Button>().onClick.AddListener(
                    () => craft(recipe, backpack)
                );
        Debug.Log($"{recipe.Value.name} recipe chosen.");
        updateBackpackForRecipe(backpack, recipe);
        ChosenRecipeImage = GameObject.FindWithTag("CR");
        ChosenRecipeImage.GetComponent<Image>().sprite = recipe.Value.sprite;
        for (int i = 0; i < 9; i++)
        {
            GameObject ob = GameObject.FindWithTag($"RI{i}");
            ob.transform.GetChild(0).GetComponentInChildren<Image>().sprite = i < recipe.Value.ingredients.Count ?
                recipe.Value.ingredients[i].sprite : null;
            ob.transform.GetChild(1).GetComponentInChildren<Text>().text = "";
            ob.transform.GetChild(2).GetComponentInChildren<Text>().text = i < recipe.Value.ingredients.Count ?
                recipe.Value.ingredients[i].nb.ToString() : "";
        }
        GameObject add = GameObject.FindWithTag("Add");
        add.GetComponent<Button>().onClick.AddListener(() => AddItem(backpack, recipe));
    }

    public void AddItem(Dictionary<string, Material> backpack, KeyValuePair<string, Recipe> recipe)
    {
        var i = 0;
        foreach (KeyValuePair<string, Material> Material in backpack)
        {
            if (Material.Value.selected > 0)
            {
                Debug.Log($"{Material.Value.selected} {Material.Value.name} transfered.");
                Material.Value.nb -= Material.Value.selected;
                GameObject ob = GameObject.FindWithTag($"BB{j}");
                GameObject go = GameObject.FindWithTag($"RI{j}");
                var given = go.transform.GetChild(1).GetComponentInChildren<Text>().text == "" ?
                    0 : int.Parse(go.transform.GetChild(1).GetComponentInChildren<Text>().text);
                var child = go.transform.GetChild(2).GetComponentInChildren<Text>().text;
                var missing = child == "" ?  0 : ((int.Parse(child) - Material.Value.selected) > 0 ? int.Parse(child) - Material.Value.selected : 0);
                ob.transform.GetChild(1).GetComponentInChildren<Text>().text = Material.Value.nb.ToString();
                ob.transform.GetChild(2).GetComponentInChildren<Text>().text = "";
                go.transform.GetChild(1).GetComponentInChildren<Text>().text = (given + Material.Value.selected).ToString();
                go.transform.GetChild(2).GetComponentInChildren<Text>().text = missing > 0 ? missing.ToString() : "";
                Material.Value.selected = 0;
                recipe.Value.given += missing == 0 ? 1 : 0;
                j++;
            }
            i++;
        }
        if (recipe.Value.given >= recipe.Value.ingredients.Count) {
            j = 0;
            Craft.gameObject.SetActive(true);
        }

    }
    public void RetrieveItem()
    {
        Debug.Log("RetrieveItem");
    }

    void craft(KeyValuePair<string, Recipe> recipe, Dictionary<string, Material> backpack)
    {
        int i = 0;
        int pos = backpack.Count;
        while (ItemImages[i].name != recipe.Key) i++;
        Material material = new Material(pos, ItemImages[i], matNames[i], 1, 0);
        backpack.Add(matNames[i], material);
        Craft.gameObject.SetActive(false);
        clearIngredientTable();
        ChosenRecipeImage = GameObject.FindWithTag("CR");
        ChosenRecipeImage.GetComponent<Image>().sprite = null;
        Debug.Log($"{recipe.Key} crafted!");
        clearBackpack(recipe.Value.ingredients.Count);
        generateBackpack(backpack, "BB0");
    }
}

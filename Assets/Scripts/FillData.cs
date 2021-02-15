using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillData : MonoBehaviour
{
    private GameObject Modal, CM, BIT, RLT, RC, BC, ChosenRecipeImage, Craft, addCraft;
    private Dictionary<string, Sprite> ItemImages = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> RecipesImages = new Dictionary<string, Sprite>();
    private List<Button> IngredientTable = new List<Button>();
    private List<List<RecipeIngredient>> ingredients = new List<List<RecipeIngredient>>();
    private Backpack backpack;
    private Dictionary<string, Recipe> recipes = new Dictionary<string, Recipe>();
    private KeyValuePair<string, Recipe> currentRecipe = new KeyValuePair<string, Recipe>();
    public static int j = 0;


    void Start()
    {
        RLT = GameObject.FindWithTag("RLT");
        BC = GameObject.FindWithTag("BC");
        Modal = GameObject.FindWithTag("Workshop modal");
        CM = GameObject.FindWithTag("CM");
        CM.GetComponent<Button>().onClick.AddListener(() => closeModal());
        backpack = new Backpack();
        fillBackpack();
        initSprites();
        Craft = GameObject.FindWithTag("Craft");
        //if (Craft)
        Craft.gameObject.SetActive(false);
        List<string> names = new List<string>() {
            "Wood key",
            "Iron key",
            "Gold key"
        };
        ingredients.Add(new List<RecipeIngredient>() {
            new RecipeIngredient(ItemImages["wood"], "wood" , 2)
        });
        ingredients.Add(new List<RecipeIngredient>() {
            new RecipeIngredient(ItemImages["copper"], "copper" , 2),
            new RecipeIngredient(ItemImages["iron"], "iron" , 1)
        });
        ingredients.Add(new List<RecipeIngredient>() {
            new RecipeIngredient(ItemImages["coal"], "coal" , 3),
            new RecipeIngredient(ItemImages["copper"], "copper" , 3),
            new RecipeIngredient(ItemImages["gold"], "gold" , 1),
            new RecipeIngredient(ItemImages["iron"], "iron" , 2),
            new RecipeIngredient(ItemImages["wood"], "wood" , 3)
        });
        for (int i = 0; i < names.Count;i++)
        {
            string name = names[i];
            Sprite sprite = RecipesImages[names[i]];
            List<RecipeIngredient> tempI = new List<RecipeIngredient>();
            for (int j = 0; j < ingredients[i].Count;j++)
                tempI.Add(ingredients[i][j]);
            recipes.Add(name, new Recipe(sprite, name, tempI, false, 0));
        }
        generateRecipes(recipes);
        Destroy(RLT);
    }

    private void initSprites()
    {
        Sprite[] recipes = Resources.LoadAll<Sprite>("Keys");
        for (int i = 0; i < recipes.Length; i++)
        {
            RecipesImages.Add(recipes[i].name, recipes[i]);
            ItemImages.Add(recipes[i].name, recipes[i]);
        }
        recipes = Resources.LoadAll<Sprite>("Materials");
        for (int i = 0; i < recipes.Length; i++)
        {
            ItemImages.Add(recipes[i].name, recipes[i]);
        }
    }

    private void fillBackpack()
    {
        for (int a = 0; a < 9; a++)
        {
            GameObject BB = GameObject.FindWithTag($"BB{a}");
            if (BB != null)
            {
                Sprite sprite = BB.transform.GetChild(0).GetComponentInChildren<Image>().sprite;
                string name = BB.transform.GetChild(0).GetComponentInChildren<Image>().sprite.name;
                string nb = BB.transform.GetChild(1).GetComponentInChildren<Text>().text;
                string selected = BB.transform.GetChild(2).GetComponentInChildren<Text>().text == "" ?
                    "0" : BB.transform.GetChild(2).GetComponentInChildren<Text>().text;
                backpack.AddMaterial(name, new Material(a, sprite, name, int.Parse(nb), int.Parse(selected)));
            }
        }
    }

    void generateRecipes(Dictionary<string, Recipe> recipes)
    {
        RC = GameObject.FindWithTag("RC");
        foreach (KeyValuePair<string, Recipe> elem in recipes)
        {
            var addButton = Instantiate(RLT);
            addButton.transform.SetParent(RC.transform);
            addButton.transform.localScale = Vector3.one;
            addButton.GetComponentInChildren<Text>().text = elem.Value.GetName();
            addButton.transform.GetChild(0).GetComponentInChildren<Image>().sprite = elem.Value.GetSprite();
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
        for (int i = 1; i < until; i++)
            Destroy(GameObject.FindWithTag($"BB{i}"));
    }

    void updateBackpackForRecipe(Backpack backpack, KeyValuePair<string, Recipe> recipe)
    {
        int i = 1;
        int j = 0;
        BIT = GameObject.FindWithTag("BB0");
        foreach (KeyValuePair<string, Material> Material in backpack.GetBackpack())
        {
            Destroy(GameObject.FindWithTag($"BB{i}"));
            if (j < recipe.Value.GetIngredients().Count)
            {
                if (Material.Key == recipe.Value.GetIngredients()[j].GetName())
                {
                    Material.Value.SetId(j);
                    var addButton = Instantiate(BIT, transform);
                    addButton.transform.SetParent(BC.transform);
                    addButton.transform.tag = ("BB" + j).ToString();
                    addButton.transform.name = ("BB" + j).ToString();
                    addButton.transform.localScale = Vector3.one;
                    addButton.transform.GetChild(0).GetComponentInChildren<Image>().sprite = Material.Value.GetSprite();
                    addButton.transform.GetChild(1).GetComponentInChildren<Text>().text = Material.Value.GetNb().ToString();
                    addButton.transform.GetChild(2).GetComponentInChildren<Text>().text = Material.Value.GetSelected() > 0 ?
                        Material.Value.GetSelected().ToString() : "";
                    addButton.GetComponent<Button>().onClick.AddListener(
                            () => selection(Material, Material.Value.GetId())
                        );
                    j++;
                }
            }
            i++;
        }
        Destroy(BIT);
    }

    void selection(KeyValuePair<string, Material> Material, int id)
    {
        Material.Value.SetSelected(Material.Value.GetSelected() + 1);
        GameObject ob = GameObject.FindWithTag($"BB{id}");
        ob.transform.GetChild(2).GetComponentInChildren<Text>().text = Material.Value.GetSelected().ToString();
    }

    void generateBackpack(Backpack backpack, string tag)
    {
        BIT = GameObject.FindWithTag(tag);
        int i = 0;
        BC = GameObject.FindWithTag("BC");
        foreach (var entry in backpack.GetBackpack())
        {
            var addButton = Instantiate(BIT, transform);
            addButton.transform.SetParent(BC.transform);
            addButton.transform.tag = ("BB" + i).ToString();
            addButton.transform.localScale = Vector3.one;
            addButton.transform.GetChild(0).GetComponentInChildren<Image>().sprite = entry.Value.GetSprite();
            addButton.transform.GetChild(1).GetComponentInChildren<Text>().text = entry.Value.GetNb().ToString();
            addButton.transform.GetChild(2).GetComponentInChildren<Text>().text = entry.Value.GetSelected() > 0 ?
                entry.Value.GetSelected().ToString() : "";
            addButton.GetComponent<Button>().onClick.AddListener(
                    () => selection(entry, entry.Value.GetId())
                );
            i++;
        }
        Destroy(BIT);
    }

    public void closeModal()
    {
        Modal.gameObject.SetActive(false);
    }

    void dispatch(KeyValuePair<string, Recipe> recipe)
    {
        Craft.gameObject.SetActive(false);
        currentRecipe = recipe;
        Craft.GetComponent<Button>().onClick.AddListener(
                    () => craft(backpack)
                );
        Debug.Log($"{recipe.Value.GetName()} recipe chosen.");
        updateBackpackForRecipe(backpack, recipe);
        ChosenRecipeImage = GameObject.FindWithTag("CR");
        ChosenRecipeImage.GetComponent<Image>().sprite = recipe.Value.GetSprite();
        for (int i = 0; i < 9; i++)
        {
            GameObject ob = GameObject.FindWithTag($"RI{i}");
            ob.transform.GetChild(0).GetComponentInChildren<Image>().sprite = i < recipe.Value.GetIngredients().Count ?
                recipe.Value.GetIngredients()[i].GetSprite() : null;
            ob.transform.GetChild(1).GetComponentInChildren<Text>().text = "";
            ob.transform.GetChild(2).GetComponentInChildren<Text>().text = i < recipe.Value.GetIngredients().Count ?
                recipe.Value.GetIngredients()[i].GetNb().ToString() : "";
        }
        GameObject add = GameObject.FindWithTag("Add");
        add.GetComponent<Button>().onClick.AddListener(() => AddItem(backpack, recipe));
    }

    private bool checkGiven()
    {
        for (int a = 0; a < 9; a++)
        {
            var BB = GameObject.FindWithTag($"RI{a}");
            if (BB != null)
            {
                string nb = BB.transform.GetChild(2).GetComponentInChildren<Text>().text == "" ?
                    "0" : BB.transform.GetChild(2).GetComponentInChildren<Text>().text;
                if (int.Parse(nb) > 0)
                    return false;
            }
        }
        return true;
    }

    public void AddItem(Backpack bk, KeyValuePair<string, Recipe> recipe)
    {
        foreach (KeyValuePair<string, Material> Material in bk.GetBackpack())
        {
            if (Material.Value.GetSelected() > 0)
            {
                j = Material.Value.GetId();
                Debug.Log($"{Material.Value.GetSelected()} {Material.Value.GetName()} transfered.");
                Material.Value.SetNb(Material.Value.GetNb() - Material.Value.GetSelected());
                GameObject ob = GameObject.FindWithTag($"BB{j}");
                GameObject go = GameObject.FindWithTag($"RI{j}");
                var given = go.transform.GetChild(1).GetComponentInChildren<Text>().text == "" ?
                    0 : int.Parse(go.transform.GetChild(1).GetComponentInChildren<Text>().text);
                var child = go.transform.GetChild(2).GetComponentInChildren<Text>().text;
                var missing = child == "" ? 0 : ((int.Parse(child) - Material.Value.GetSelected()) > 0 ? int.Parse(child) - Material.Value.GetSelected() : 0);
                ob.transform.GetChild(1).GetComponentInChildren<Text>().text = Material.Value.GetNb().ToString();
                ob.transform.GetChild(2).GetComponentInChildren<Text>().text = "";
                go.transform.GetChild(1).GetComponentInChildren<Text>().text = (given + Material.Value.GetSelected()).ToString();
                go.transform.GetChild(2).GetComponentInChildren<Text>().text = missing > 0 ? missing.ToString() : "";
                Material.Value.SetSelected(0);
                recipe.Value.SetGiven(recipe.Value.GetGiven() + missing == 0 ? 1 : 0);
            }
        }
        if (checkGiven())
            Craft.gameObject.SetActive(true);
    }

    //public void RetrieveItem()
    //{
    //    Debug.Log("RetrieveItem");
    //}

    void craft(Backpack backpack)
    {
        if (currentRecipe.Key == null)
            return;
        Material material = new Material(backpack.GetBackpack().Count,
            ItemImages[currentRecipe.Key], currentRecipe.Key, 1, 0);
        backpack.AddMaterial(currentRecipe.Key, material);
        Craft.gameObject.SetActive(false);
        clearIngredientTable();
        ChosenRecipeImage = GameObject.FindWithTag("CR");
        ChosenRecipeImage.GetComponent<Image>().sprite = null;
        Debug.Log($"{currentRecipe.Key} crafted!");
        clearBackpack(currentRecipe.Value.GetIngredients().Count);
        generateBackpack(backpack, "BB0");
        currentRecipe = new KeyValuePair<string, Recipe>();
    }
}

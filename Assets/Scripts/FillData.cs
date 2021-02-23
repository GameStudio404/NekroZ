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

    //retrieve the backpack and every necessary gameObjects, sprites, recipes
    //On the 1st day, God created the universe
    void Start()
    {
        RLT = GameObject.FindWithTag("RLT");
        BC = GameObject.FindWithTag("BC");
        Modal = GameObject.FindWithTag("Workshop modal");
        CM = GameObject.FindWithTag("CM");
        Craft = GameObject.FindWithTag("Craft");
        CM.GetComponent<Button>().onClick.AddListener(() => closeModal());
        backpack = new Backpack();
        fillBackpack();
        initSprites();
        List<string> names = new List<string>() {
            "Wood key",
            "Iron key",
            "Gold key"
        };
        ingredients.Add(new List<RecipeIngredient>() {
            new RecipeIngredient(ItemImages["refined wood"], "refined wood" , 2)
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
        Modal.gameObject.SetActive(false);
    }

    //interact with the modal
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.E))
        {
            Modal.gameObject.SetActive(!Modal.activeSelf);
        }
    }


    //retrieve the sprites for the recipes and materials
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

    //retieve backpack
    private void fillBackpack()
    {
        for (int a = 0; a < 9; a++)
        {
            GameObject BB = GameObject.FindWithTag($"BB{a}");
            if (BB != null)
            {
                Sprite sprite = BB.transform.GetChild(0).GetComponentInChildren<Image>().sprite;
                string name = sprite.name;
                string nb = BB.transform.GetChild(1).GetComponentInChildren<Text>().text;
                string selected = BB.transform.GetChild(2).GetComponentInChildren<Text>().text == "" ?
                    "0" : BB.transform.GetChild(2).GetComponentInChildren<Text>().text;
                backpack.AddMaterial(name, new Material(a, sprite, name, int.Parse(nb), int.Parse(selected),
                    $"refined {name}", 0));
            }
        }
    }

    //Generate and display available recipes
    void generateRecipes(Dictionary<string, Recipe> recipes)
    {
        RC = GameObject.FindWithTag("RC");
        int i = 1;
        int j = 0;
        RLT = GameObject.FindWithTag("RB0") == null ? GameObject.FindWithTag("RLT") : GameObject.FindWithTag("RB0");
        foreach (KeyValuePair<string, Recipe> elem in recipes)
        {
            GameObject obj = GameObject.FindWithTag($"RB{i}");
            if (obj)
                Destroy(obj);
            var addButton = Instantiate(RLT);
            addButton.transform.SetParent(RC.transform);
            addButton.transform.localScale = Vector3.one;
            addButton.transform.tag = $"RB{j}";
            addButton.transform.name = $"RB{j}";
            addButton.GetComponentInChildren<Text>().text = elem.Value.GetName();
            addButton.transform.GetChild(0).GetComponentInChildren<Image>().sprite = elem.Value.GetSprite();
            addButton.GetComponent<Button>().onClick.AddListener(
                    () => dispatch(elem)
                );
            i++;
            j++;
        }
        Destroy(RLT);
    }

    //clear the table after crafting or recipe selection
    void clearIngredientTable()
    {
        for (int i = 0; i < 9; i++)
        {
            GameObject ob = GameObject.FindWithTag($"RI{i}");
            ob.transform.GetChild(0).GetComponentInChildren<Image>().sprite =
            UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UIMask.psd");
            ob.transform.GetChild(1).GetComponentInChildren<Text>().text = "";
            ob.transform.GetChild(2).GetComponentInChildren<Text>().text = "";
        }
    }

    //remove every material in the backpack (only on the screen)
    void clearBackpack(int until)
    {
        for (int i = 1; i < until; i++)
            Destroy(GameObject.FindWithTag($"BB{i}"));
    }

    //update the backpack (on the screen), to only display the required material for the recipe
    void updateBackpackForRecipe(Backpack backpack, KeyValuePair<string, Recipe> recipe)
    {
        int i = 1;
        int j = 0;
        BIT = GameObject.FindWithTag("BB0");
        foreach (KeyValuePair<string, Material> Material in backpack.GetBackpack())
        {
            GameObject obj = GameObject.FindWithTag($"BB{i}");
            if (obj)
                Destroy(obj);
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

    //fully display the backpack on the screen
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

    //display the info about the chosen recipe
    //replace the ingredients table by the required ones
    void dispatch(KeyValuePair<string, Recipe> recipe)
    {
        currentRecipe = recipe;
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
        GameObject ret = GameObject.FindWithTag("Ret");
        ret.GetComponent<Button>().onClick.AddListener(() => RetrieveItem(recipe, 0));
    }

    public void closeModal()
    {
        Modal.gameObject.SetActive(false);
    }

    //check if all the required ingredients have been given
    private bool checkGiven()
    {
        for (int a = 0; a < 9; a++)
        {
            GameObject RI = GameObject.FindWithTag($"RI{a}");
            if (RI != null)
            {
                string nb = RI.transform.GetChild(2).GetComponentInChildren<Text>().text == "" ?
                    "0" : RI.transform.GetChild(2).GetComponentInChildren<Text>().text;
                if (int.Parse(nb) > 0)
                    return false;
            }
        }
        return true;
    }

    //display the number of selected material you want to tranfert to the crafting table
    void selection(KeyValuePair<string, Material> Material, int id)
    {
        Material.Value.SetSelected(Material.Value.GetSelected() + 1);
        GameObject ob = GameObject.FindWithTag($"BB{id}");
        ob.transform.GetChild(2).GetComponentInChildren<Text>().text = Material.Value.GetSelected().ToString();
    }

    //add the selected ingredient from the backpack
    //transfer them to the ingredients table
    //unlock the craft button if all the required ingredients are validated
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
        {
            Craft.GetComponent<Button>().onClick.AddListener(
                () => craft(backpack)
            );
            Craft.GetComponent<Image>().color = new Color32(166, 0, 0, 255);
        }
    }

    void reset(Backpack backpack, KeyValuePair<string, Recipe> recipe)
    {
        Craft.GetComponent<Image>().color = new Color32(70, 70, 70, 255);
        clearIngredientTable();
        ChosenRecipeImage = GameObject.FindWithTag("CR");
        ChosenRecipeImage.GetComponent<Image>().sprite =
            UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UIMask.psd");
        clearBackpack(currentRecipe.Value.GetIngredients().Count);
        generateBackpack(backpack, "BB0");
        currentRecipe = new KeyValuePair<string, Recipe>();
    }

    //retrieve the unnecessary amount of given materials
    //or all the given materials if th craft wasn't completed
    private void RetrieveItem(KeyValuePair<string, Recipe> recipe, int flag)
    {
        for (int a = 0; a < recipe.Value.GetIngredients().Count; a++)
        {
            GameObject RI = GameObject.FindWithTag($"RI{a}");
            GameObject BB = GameObject.FindWithTag($"BB{a}");
            if (BB != null && RI != null)
            {
                string material = RI.transform.GetChild(0).GetComponentInChildren<Image>().sprite.name;
                int required = recipe.Value.GetIngredients()[a].GetNb();
                int given = int.Parse(RI.transform.GetChild(1).GetComponentInChildren<Text>().text == "" ?
                    "0": RI.transform.GetChild(1).GetComponentInChildren<Text>().text);
                if (given > required && flag == 1)
                {
                    var diff = Math.Abs(required - given);
                    backpack.GetBackpack()[material].SetNb(
                            int.Parse(BB.transform.GetChild(1).GetComponentInChildren<Text>().text) + diff
                            );
                    Debug.Log($"{diff} {material} retrieved");
                }
                else if (flag == 0)
                {
                    backpack.GetBackpack()[material].SetNb(
                            int.Parse(BB.transform.GetChild(1).GetComponentInChildren<Text>().text) + given
                            );
                    Debug.Log($"{given} {material} retrieved");
                    reset(backpack, recipe);
                }
            }
        }
    }

    //craft the chosen itam from the recipe, update the backpack
    //God resets the universe
    void craft(Backpack backpack)
    {
        if (currentRecipe.Key == null)
            return;
        RetrieveItem(currentRecipe, 1);
        Material material = new Material(backpack.GetBackpack().Count,
            ItemImages[currentRecipe.Key], currentRecipe.Key, 1, 0, null, 0);
        backpack.AddMaterial(currentRecipe.Key, material);
        Debug.Log($"{currentRecipe.Key} crafted!");
        reset(backpack, currentRecipe);
    }
}

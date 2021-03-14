using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crafting : MonoBehaviour
{
    private GameObject Modal, CM, BIT, BC, CR, addCraft;
    private Dictionary<string, Sprite> ItemImages = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> RecipesImages = new Dictionary<string, Sprite>();
    private List<Button> IngredientTable = new List<Button>();
    private List<List<RecipeIngredient>> ingredients = new List<List<RecipeIngredient>>();
    private Backpack backpack;
    private Book book;
    private KeyValuePair<string, Recipe> currentRecipe = new KeyValuePair<string, Recipe>();
    private Sprite[] back;
    //public static int j = 0;
    //private List<string> names = new List<string>();

    //retrieve the backpack and every necessary gameObjects, sprites, recipes
    //On the 1st day, God created the universe
    void Start()
    {
        Modal = GameObject.FindWithTag("WM");
        CM = GameObject.FindWithTag("CM");
        CM.GetComponent<Button>().onClick.AddListener(() => closeModal());
        initSprites();
        initBackpack();
        initBook();
        generateRecipes();
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

    //void OnDisable()
    //{
        
    //}

    //retrieve the sprites for the recipes and materials
    private void initSprites()
    {
        Sprite[] recipes = Resources.LoadAll<Sprite>("Keys");
        for (int i = 0; i < recipes.Length; i++)
        {
            RecipesImages.Add(recipes[i].name, recipes[i]);
            //names.Add(recipes[i].name);
            ItemImages.Add(recipes[i].name, recipes[i]);
        }
        recipes = Resources.LoadAll<Sprite>("Materials");
        for (int i = 0; i < recipes.Length; i++)
        {
            ItemImages.Add(recipes[i].name, recipes[i]);
        }
        back = Resources.LoadAll<Sprite>("utils");
    }

    //retieve backpack
    private void initBackpack()
    {
        backpack = new Backpack(new Dictionary<string, Material>(), back);
        backpack.Load();
    }

    //retieve book
    private void initBook()
    {
        book = new Book(new Dictionary<string, Recipe>());
        book.Load();
        //Debug.Log(book.GetBook().Count);
        //ingredients.Add(new List<RecipeIngredient>() {
        //    new RecipeIngredient("refined wood" , 2)
        //});
        //foreach (KeyValuePair<string, Sprite> recipe in RecipesImages)
        //{
        //    string name = recipe.Key;
        //    Sprite sprite = recipe.Value;
        //    List<RecipeIngredient> tempI = new List<RecipeIngredient>();
        //    for (int j = 0; j < ingredients[0].Count; j++)
        //        tempI.Add(ingredients[0][j]);
        //    book.AddRecipe(name, new Recipe(name, tempI, true, 0));
        //}
        //book.Save();
    }

    //Generate and display available recipes
    void generateRecipes()
    {
        GameObject RC = GameObject.FindWithTag("RC");
        GameObject RLT = Resources.Load<GameObject>("utils/Templates/RTemplate/RLT/RLT");
        int j = 0;
        foreach (KeyValuePair<string, Recipe> recipe in book.GetBook())
        {
            if (recipe.Value.GetAvailable())
            {
                TagHelper.AddTag($"RB{j}");
                var addButton = Instantiate(RLT);
                addButton.transform.SetParent(RC.transform);
                addButton.transform.localScale = Vector3.one;
                addButton.transform.tag = $"RB{j}";
                addButton.transform.name = $"RB{j}";
                addButton.GetComponentInChildren<Text>().text = recipe.Value.GetName();
                addButton.transform.GetChild(0).GetComponentInChildren<Image>().sprite = RecipesImages[recipe.Key];
                addButton.GetComponent<Button>().onClick.AddListener(
                        () => dispatch(recipe)
                    );
                j++;
            }
        }
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

    //display the info about the chosen recipe
    //replace the ingredients table by the required ones
    void dispatch(KeyValuePair<string, Recipe> recipe)
    {
        currentRecipe = recipe;
        Debug.Log($"{recipe.Value.GetName()} recipe chosen.");
        backpack.UpdateForRecipe(recipe, ItemImages);
        CR = GameObject.FindWithTag("CR");
        CR.transform.GetChild(0).GetComponent<Image>().sprite = RecipesImages[recipe.Value.GetName()];
        CR.GetComponent<Image>().color = new Color32(166, 0, 0, 255);
        for (int i = 0; i < recipe.Value.GetIngredients().Count; i++)
        {
            GameObject ob = GameObject.FindWithTag($"RI{i}");
            ob.transform.GetChild(0).GetComponentInChildren<Image>().sprite = ItemImages[recipe.Value.GetIngredients()[i].GetName()];
            ob.transform.GetChild(1).GetComponentInChildren<Text>().text = "";
            ob.transform.GetChild(2).GetComponentInChildren<Text>().text = recipe.Value.GetIngredients()[i].GetNb().ToString();
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

    //add the selected ingredient from the backpack
    //transfer them to the ingredients table
    //unlock the craft button if all the required ingredients are validated
    public void AddItem(Backpack bk, KeyValuePair<string, Recipe> recipe)
    {
        foreach (KeyValuePair<string, Material> Material in bk.GetBackpack())
        {
            if (Material.Value.GetSelected() > 0)
            {
                int j = Material.Value.GetId();
                Debug.Log($"{Material.Value.GetSelected()} {Material.Value.GetName()} transfered.");
                GameObject ob = GameObject.FindWithTag($"BB{j}");
                GameObject go = GameObject.FindWithTag($"RI{j}");
                var given = go.transform.GetChild(1).GetComponentInChildren<Text>().text == "" ?
                    0 : int.Parse(go.transform.GetChild(1).GetComponentInChildren<Text>().text);
                var child = go.transform.GetChild(2).GetComponentInChildren<Text>().text;
                var missing = child == "" ? 0 : ((int.Parse(child) - Material.Value.GetSelected()) > 0 ? int.Parse(child) - Material.Value.GetSelected() : 0);
                ob.transform.GetChild(1).GetComponentInChildren<Text>().text = $"{Material.Value.GetNb() - Material.Value.GetSelected()}";
                ob.transform.GetChild(2).GetComponentInChildren<Text>().text = "";
                go.transform.GetChild(1).GetComponentInChildren<Text>().text = (given + Material.Value.GetSelected()).ToString();
                go.transform.GetChild(2).GetComponentInChildren<Text>().text = missing > 0 ? missing.ToString() : "";
                Material.Value.SetSelected(0);
                recipe.Value.SetGiven(recipe.Value.GetGiven() + missing == 0 ? 1 : 0);
            }
        }
        if (checkGiven())
        {
            CR.GetComponent<Button>().onClick.AddListener(
                () => craft(backpack)
            );
            CR.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }

    void reset(Backpack backpack, KeyValuePair<string, Recipe> recipe)
    {
        CR.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        clearIngredientTable();
        CR.transform.GetChild(0).GetComponent<Image>().sprite =
            UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UIMask.psd");
        backpack.Clear();
        backpack.Generate(ItemImages);
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
                if (given >= required && flag == 1)
                {
                    var diff = given - required;
                    Material mat = backpack.GetMaterial(material);
                    mat.SetNb(mat.GetNb() - (given - diff));
                    Debug.Log($"{diff} {material} retrieved");
                }
                else if (flag == 0)
                {
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
            currentRecipe.Key, 1, 0, null, 0, 0);
        backpack.AddMaterial(currentRecipe.Key, material);
        Debug.Log($"{currentRecipe.Key} crafted!");
        reset(backpack, currentRecipe);
    }
}

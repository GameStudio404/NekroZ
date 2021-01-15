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
    public GameObject ChosenRecipeImage;
    public Dictionary<string, Sprite> ima = new Dictionary<string, Sprite>();
    public List<Sprite> ItemImages = new List<Sprite>();
    public List<Sprite> RecipesImages = new List<Sprite>();
    public List<Button> IngredientTable = new List<Button>();
    public List<List<RecipeIngredient>> ingredients = new List<List<RecipeIngredient>>();
    public Dictionary<string, Material> backpack = new Dictionary<string, Material>();
    public Dictionary<string, Recipe> recipes = new Dictionary<string, Recipe>();
    public List<string> matNames = new List<string>()
        {
            "wood",
            "coal",
            "copper",
            "iron",
            "gold"
        };

    void Start()
    {
        Modal = GameObject.FindWithTag("Workshop modal");
        Modal.gameObject.SetActive(true);
        List<string> names = new List<string>() {
            "Wood key",
            "Iron key",
            "Nfc key"
        };
        List<int> matNb = new List<int>()
        {
            9,
            3,
            4,
            3,
            5
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
            i++;
            recipes.Add(name, new Recipe(sprite, name, tempI, false));
        }
        i = 0;
        while (i < matNames.Count)
        {
            Material material = new Material(i, ItemImages[i], matNames[i], matNb[i], 0);
            backpack.Add(matNames[i++], material);
        }
        generateRecipes(recipes);
        generateBackpack(backpack);
        Destroy(RLT);
        Destroy(BIT);

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

    void generateBackpack(Dictionary<string, Material> backpack)
    {
        BIT = GameObject.FindWithTag("BIT");
        Debug.Log(contentB.transform.childCount);
        if (contentB.transform.childCount > 1)
        {
            for (int j = 0; j < matNames.Count; j++)
            {
                Destroy(contentB.transform.GetChild(j));
            }
        }
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
    }

    public void closeModal()
    {
        Modal.gameObject.SetActive(false);
    }

    void selection(KeyValuePair<string, Material> Material, int id)
    {
        Material.Value.selected += 1;
        backpack[Material.Key] = Material.Value;
        GameObject ob = GameObject.FindWithTag($"BB{id}");
        ob.transform.GetChild(2).GetComponentInChildren<Text>().text = Material.Value.selected.ToString();
    }

    void dispatch(KeyValuePair<string, Recipe> recipe)
    {
        Debug.Log($"{recipe.Value.name} recipe chosen.");
        ChosenRecipeImage = GameObject.FindWithTag("CR");
        ChosenRecipeImage.GetComponent<Image>().sprite = recipe.Value.sprite;
        for (int i = 0; i < recipe.Value.ingredients.Count; i++)
        {
            GameObject ob = GameObject.FindWithTag($"RI{i}");
            ob.transform.GetChild(0).GetComponentInChildren<Image>().sprite = recipe.Value.ingredients[i].sprite;
            ob.transform.GetChild(1).GetComponentInChildren<Text>().text = (0).ToString();
            ob.transform.GetChild(2).GetComponentInChildren<Text>().text = recipe.Value.ingredients[i].nb.ToString();
        }
    }

}

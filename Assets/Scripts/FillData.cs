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
    public List<List<string>> ingredients = new List<List<string>>();
    public Dictionary<string, Material> backpack = new Dictionary<string, Material>();
    public Dictionary<string, Recipe> recipes = new Dictionary<string, Recipe>();

    void Start()
    {
        Modal = GameObject.FindWithTag("Workshop modal");
        Modal.gameObject.SetActive(true);
        List<string> names = new List<string>() {
            "Wood key",
            "Iron key",
            "Nfc key"
        };
        List<string> matNames = new List<string>()
        {
            "wood",
            "coal",
            "copper",
            "iron",
            "gold"
        };
        List<int> matNb = new List<int>()
        {
            9,
            3,
            4,
            3,
            5
        };
        ingredients.Add(new List<string>() {
            "Wood",
            "Wood"
        });
        ingredients.Add(new List<string>() {
            "Iron",
            "Copper"
        });
        ingredients.Add(new List<string>() {
            "Iron",
            "Aluminium",
            "Nfc ship"
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
                RecipeIngredient stuff = new RecipeIngredient(
                    ingredients[i][j++]);
                tempI.Add(stuff);
            }
            j = 0;
            i++;
            recipes.Add(name, new Recipe(sprite, name, tempI, false));
        }
        i = 0;
        while (i < matNames.Count)
        {
            Material material = new Material(ItemImages[i], matNames[i], matNb[i]);
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
        foreach (var entry in backpack)
        {
            var addButton = Instantiate(BIT, transform);
            addButton.transform.SetParent(contentB.transform);
            addButton.transform.localScale = Vector3.one;
            addButton.GetComponentInChildren<Text>().text = entry.Value.nb.ToString();
            addButton.transform.GetChild(0).GetComponentInChildren<Image>().sprite = entry.Value.sprite;
            addButton.GetComponent<Button>().onClick.AddListener(
                    () => { Debug.Log("backpack"); }
                );
        }
    }

    public void closeModal()
    {
        Modal.gameObject.SetActive(false);
    }

    void dispatch(KeyValuePair<string, Recipe> recipe)
    {
        ChosenRecipeImage = GameObject.FindWithTag("CR");
        ChosenRecipeImage.GetComponent<Image>().sprite = recipe.Value.sprite;
        Debug.Log($"{name} recipe chosen.");
    }

}

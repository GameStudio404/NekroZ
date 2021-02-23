using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Refining : MonoBehaviour
{
    public GameObject Modal, CM, GO;
    public Text fuse;
    private GameObject BIT, BC;
    private Backpack backpack;
    private Dictionary<string, Sprite> ItemImages = new Dictionary<string, Sprite>();
    private int toFuse;

    // Start is called before the first frame update
    void Start()
    {
        CM.GetComponent<Button>().onClick.AddListener(() => closeModal());
        BC = GameObject.FindWithTag("BC");
        BIT = GameObject.FindWithTag("BB0");
        backpack = new Backpack();
        toFuse = 0;
        fillBackpack();
        initSprites();
        Modal.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (toFuse > 0 && toFuse != int.Parse(fuse.text))
            refine(backpack, GO);
        toFuse = int.Parse(fuse.text);
        if (Input.GetKeyDown(KeyCode.E))
            Modal.gameObject.SetActive(!Modal.activeSelf);
    }

    //retrieve the sprites for the recipes and materials
    private void initSprites()
    {
        Sprite[] recipes = Resources.LoadAll<Sprite>("Keys");
        for (int i = 0; i < recipes.Length; i++)
        {
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

    //update the backpack (on the screen), display the new refined material
    void UpdateBackpack(Backpack bk, Material mat)
    {
        var exists = GameObject.FindWithTag($"BB{mat.GetId()}");
        if (exists)
           exists.transform.GetChild(1).GetComponentInChildren<Text>().text = mat.GetNb().ToString();
        else
        {
            var addButton = Instantiate(BIT, transform);
            addButton.transform.SetParent(BC.transform);
            addButton.transform.tag = ("BB" + mat.GetId()).ToString();
            addButton.transform.name = ("BB" + mat.GetId()).ToString();
            addButton.transform.localScale = Vector3.one;
            addButton.transform.GetChild(0).GetComponentInChildren<Image>().sprite = mat.GetSprite();
            addButton.transform.GetChild(1).GetComponentInChildren<Text>().text = mat.GetNb().ToString();
            addButton.transform.GetChild(2).GetComponentInChildren<Text>().text = mat.GetSelected() > 0 ?
                mat.GetSelected().ToString() : "";
        }
    }

    //refine selected material and add the new material to the backpack
    private void refine(Backpack bk, GameObject go)
    {
        Sprite sprite = go.transform.GetChild(0).GetComponent<Image>().sprite;
        if (!bk.GetBackpack().ContainsKey($"refined {sprite.name}"))
            bk.AddMaterial($"refined {sprite.name}",
                new Material(bk.GetBackpack().Count, ItemImages[$"refined {sprite.name}"], $"refined {sprite.name}", 1, 0, null, 0));
        else
        {
            Material mat = bk.GetMaterial($"refined {sprite.name}");
            mat.SetNb(mat.GetNb() + 1);
        }
        Debug.Log($"New: refined {sprite.name}");
        UpdateBackpack(backpack, bk.GetMaterial($"refined {sprite.name}"));
    }

    public void closeModal()
    {
        Modal.gameObject.SetActive(false);
    }

}

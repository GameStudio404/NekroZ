using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Refining : MonoBehaviour
{
    private GameObject Modal, CM, GO;
    public Text fuse;
    private GameObject BIT, BC;
    private Backpack backpack;
    private Dictionary<string, Sprite> ItemImages = new Dictionary<string, Sprite>();
    private int toFuse;
    private Sprite[] back;

    // Start is called before the first frame update
    void Start()
    {
        Modal = GameObject.FindWithTag("FM");
        GO = GameObject.FindWithTag("Fuse");
        CM = GameObject.FindWithTag("CM");
        CM.GetComponent<Button>().onClick.AddListener(() => closeModal());
        initSprites();
        initBackpack();
        toFuse = 0;
        Modal.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (toFuse > 0 && toFuse != int.Parse(fuse.text))
        {
            refine(backpack, GO);
        }
        toFuse = int.Parse(fuse.text);
        if (Input.GetKeyDown(KeyCode.E))
        {
            Modal.gameObject.SetActive(!Modal.activeSelf);
            backpack.UpdateForRefine(ItemImages);
        }
    }

    //retrieve the sprites for the recipes and materials
    private void initSprites()
    {
        Sprite[] recipes = Resources.LoadAll<Sprite>("Materials");
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

    //refine selected material and add the new material to the backpack
    private void refine(Backpack bk, GameObject go)
    {
        Sprite sprite = go.transform.GetChild(0).GetComponent<Image>().sprite;
        if (!bk.GetBackpack().ContainsKey($"refined {sprite.name}"))
            bk.AddMaterial($"refined {sprite.name}",
                new Material(bk.GetBackpack().Count, $"refined {sprite.name}", 1, 0, null, 0, 0));
        else
        {
            Material mat = bk.GetMaterial($"refined {sprite.name}");
            mat.SetNb(mat.GetNb() + 1);
        }
        Debug.Log($"New: refined {sprite.name}");
        backpack.UpdateForRefine(ItemImages);
    }

    public void closeModal()
    {
        backpack.UpdateSelf(ItemImages);
        Modal.gameObject.SetActive(false);
    }

}

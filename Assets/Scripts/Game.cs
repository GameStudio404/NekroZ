/*
 * Copyright (c) 2017 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * Notwithstanding the foregoing, you may not use, copy, modify, merge, publish, 
 * distribute, sublicense, create a derivative work, and/or sell copies of the 
 * Software in any work that is designed, intended, or marketed for pedagogical or 
 * instructional purposes related to programming, coding, application development, 
 * or information technology.  Permission for such use, copying, modification,
 * merger, publication, distribution, sublicensing, creation of derivative works, 
 * or sale is expressly withheld.
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public GameObject playerObj, BP;
    static Player player;
    private List<Sprite> ItemImages = new List<Sprite>();
    private Backpack backpack;
    private Sprite[] empty;


    private enum Scenes
    {
        Lab,
        Workshop,
        Storage,
        LivingRoom,
        Kitchen,
        Elevator,
    };

    void Start()
    {
        GameObject go = GameObject.FindWithTag("BB0");
        initSprites();
        initBackpack();
        playerObj = GameObject.FindWithTag("Player");
        player = new Player("Mike", playerObj);
        if (!go)
        {
            BP = GameObject.FindWithTag("Backpack panel");
            generateBackpack(backpack.GetBackpack(), "BIT");
        } else
        {
            Destroy(GameObject.FindWithTag("Backpack panel"));
            BP = GameObject.FindWithTag("BP");
        }
    }

    void Update()
    {
        if (playerObj.transform.localPosition.y <= 1)
        {
            player.getPlayer().transform.localPosition = new Vector3(player.getPlayer().transform.localPosition.x,
                player.getPlayer().transform.localPosition.y, -3);
        } else
        {
            player.getPlayer().transform.localPosition = new Vector3(player.getPlayer().transform.localPosition.x,
                player.getPlayer().transform.localPosition.y, -1);
        }
    }

    private void initSprites()
    {
        Sprite[] materials = Resources.LoadAll<Sprite>("Materials");
        Debug.Log(materials.Length);
        //empty = Resources.LoadAll<Sprite>("utils");
        for (var i = 0; i < materials.Length; i++)
        {
            ItemImages.Add(materials[i]);
        }
    }

    private void initBackpack()
    {
        backpack = new Backpack();
        //backpack.AddMaterial(ItemImages[0].name, new Material(0, ItemImages[0], ItemImages[0].name, 1, 0, $"refined {ItemImages[0].name}", 0));
        //backpack.AddMaterial(ItemImages[1].name, new Material(1, ItemImages[1], ItemImages[1].name, 1, 0, $"refined {ItemImages[1].name}", 0));
        int j = 0;
        for (int i = 0; i < ItemImages.Count; i++)
        {
            if (!ItemImages[i].name.Contains("refined") && !ItemImages[i].name.Contains("empty"))
                backpack.AddMaterial(ItemImages[i].name, new Material(j++, ItemImages[i], ItemImages[i].name,
                    10, 0, $"refined {ItemImages[i].name}", 0));
        }
    }

    void generateBackpack(Dictionary<string, Material> backpack, string tag)
    {
        GameObject BIT = GameObject.FindWithTag(tag);
        BP.transform.tag = "BP";
        BP = GameObject.FindWithTag("BP");
        int i = 0;
        GameObject contentB = GameObject.FindWithTag("BC");
        foreach (var entry in backpack)
        {
            var addButton = Instantiate(BIT, transform);
            addButton.transform.SetParent(contentB.transform);
            addButton.transform.tag = ("BB" + i).ToString();
            addButton.transform.name = ("BB" + i).ToString();
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
        //if (i < 8)
        //{
        //    while (i < 9)
        //    {
        //        var addButton = Instantiate(BIT, transform);
        //        addButton.transform.SetParent(contentB.transform);
        //        addButton.transform.tag = ("BB" + i).ToString();
        //        addButton.transform.name = ("BB" + i).ToString();
        //        addButton.transform.localScale = Vector3.one;
        //        addButton.transform.GetChild(0).GetComponentInChildren<Image>().sprite = empty[0];
        //        addButton.transform.GetChild(1).GetComponentInChildren<Text>().text = "";
        //        addButton.transform.GetChild(2).GetComponentInChildren<Text>().text = "";
        //        i++;
        //    }
        //}
        Destroy(BIT);

    }

    void selection(KeyValuePair<string, Material> Material, int id)
    {
        Material.Value.SetSelected(Material.Value.GetSelected() + 1);
        GameObject ob = GameObject.Find($"BB{id}");
        ob.transform.GetChild(2).GetComponentInChildren<Text>().text = Material.Value.GetSelected().ToString();
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        switch (collider2D.name)
        {
            case "Lab":
                Destroy(playerObj);
                DontDestroyOnLoad(BP);
                SceneManager.LoadScene(Scenes.Lab.ToString());
                player.getPlayer().transform.localPosition = new Vector3(0, -5, -3);
                break;
            case "Workshop":
                DontDestroyOnLoad(playerObj);
                DontDestroyOnLoad(BP);
                SceneManager.LoadScene(Scenes.Workshop.ToString());
                player.getPlayer().transform.localPosition = new Vector3(-2, 7, -1);
                break;
            case "Workshop f Storage":
                DontDestroyOnLoad(playerObj);
                DontDestroyOnLoad(BP);
                SceneManager.LoadScene(Scenes.Workshop.ToString());
                player.getPlayer().transform.localPosition = new Vector3(-2, -6, -3);
                break;
            case "Workshop f LivingRoom":
                DontDestroyOnLoad(playerObj);
                DontDestroyOnLoad(BP);
                SceneManager.LoadScene(Scenes.Workshop.ToString());
                player.getPlayer().transform.localPosition = new Vector3(15, -5, -3);
                break;
            case "Storage":
                DontDestroyOnLoad(playerObj);
                DontDestroyOnLoad(BP);
                SceneManager.LoadScene(Scenes.Storage.ToString());
                player.getPlayer().transform.localPosition = new Vector3(0, 7, -1);
                break;
            case "LivingRoom":
                DontDestroyOnLoad(playerObj);
                DontDestroyOnLoad(BP);
                SceneManager.LoadScene(Scenes.LivingRoom.ToString());
                player.getPlayer().transform.localPosition = new Vector3(-16, 4, -1);
                break;
            case "LivingRoom f Kitchen":
                DontDestroyOnLoad(playerObj);
                DontDestroyOnLoad(BP);
                SceneManager.LoadScene(Scenes.LivingRoom.ToString());
                player.getPlayer().transform.localPosition = new Vector3(-6, 7, -1);
                break;
            case "LivingRoom f Elevator":
                DontDestroyOnLoad(playerObj);
                DontDestroyOnLoad(BP);
                SceneManager.LoadScene(Scenes.LivingRoom.ToString());
                player.getPlayer().transform.localPosition = new Vector3(2, -6, -1);
                break;
            case "Kitchen":
                DontDestroyOnLoad(playerObj);
                DontDestroyOnLoad(BP);
                SceneManager.LoadScene(Scenes.Kitchen.ToString());
                player.getPlayer().transform.localPosition = new Vector3(-12, -6, -3);
                break;
            case "Elevator":
                DontDestroyOnLoad(playerObj);
                DontDestroyOnLoad(BP);
                SceneManager.LoadScene(Scenes.Elevator.ToString());
                player.getPlayer().transform.localPosition = new Vector3(0, 7, -1);
                break;
            default:
                break;
        }
    }

    public void SaveGame()
    {
        Save save = CreateSaveGameObject();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();

        Debug.Log("Game Saved");
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            Debug.Log("Game Loaded");
        }
        else
        {
            Debug.Log("No game saved!");
        }
    }


    public void SaveAsJSON()
    {
        Save save = CreateSaveGameObject();
        string json = JsonUtility.ToJson(save);

        Debug.Log("Saving as JSON: " + json);
    }

    private Save CreateSaveGameObject()
    {
        Save save = new Save();

        return save;
    }

}

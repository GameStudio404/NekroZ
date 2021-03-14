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

public class Game : MonoBehaviour
{
    private GameObject playerObj;
    private Player player;
    private Dictionary<string, Sprite> ItemImages = new Dictionary<string, Sprite>();
    private Backpack backpack;
    private Sprite[] empty;
    private Sprite[] back;

    void OnEnable()
    {
        Instantiate(Resources.Load<GameObject>("utils/Templates/BP/BP"));
    }

    void Start()
    {
        Instantiate(Resources.Load<GameObject>("utils/Templates/PlayerTemplate/Player"));
        playerObj = GameObject.FindWithTag("Player");
        player = new Player("Frank", playerObj.transform.localPosition, null);
        initSprites();
        initBackpack();
        backpack.Generate(ItemImages);
        backpack.GetBB().GetComponent<Button>().onClick.AddListener(
            () => backpack.Display()
        );
    }

    void Update()
    {
        if (playerObj.transform.localPosition.y <= 1)
        {
            playerObj.transform.localPosition = new Vector3(
                playerObj.transform.localPosition.x,
                playerObj.transform.localPosition.y,
                -3);
        }
        else
        {
            playerObj.transform.localPosition = new Vector3(
                playerObj.transform.localPosition.x,
                playerObj.transform.localPosition.y,
                -1);
        }
    }

    void OnDisable()
    {
        Backpack b = new Backpack(new Dictionary<string, Material>(), back);
        b.Load();
        //Debug.Log($"Current: {backpack.GetBackpack().Count}");
        //backpack.Print();
        //Debug.Log($"loaded: {b.GetBackpack().Count}");
        //b.Print();
        //Debug.Log($"Current: {backpack.Score()}, Loaded: {b.Score()}");
        if (backpack.Score() > b.Score())
            backpack.Save();
    }

    private void initSprites()
    {
        Sprite[] materials = Resources.LoadAll<Sprite>("Materials");
        for (var i = 0; i < materials.Length; i++)
        {
            ItemImages.Add(materials[i].name, materials[i]);
        }
        back = Resources.LoadAll<Sprite>("utils");
    }

    private void initBackpack()
    {
        Dictionary<string, Material> dic = new Dictionary<string, Material>();
        //dic.Add("wood", new Material(0, "wood", 1, 0, $"refined wood", 3, 0));
        //dic.Add("coal", new Material(1, "coal", 1, 0, $"refined coal", 5, 0));
        backpack = new Backpack(dic, back);
        backpack.Load();
        //backpack.GetMaterial("wood").SetNb(2);
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

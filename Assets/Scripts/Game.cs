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
    public GameObject door;
    public GameObject playerObj;
    static Player player;

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
        if (player == null) {
            playerObj = GameObject.FindWithTag("Player");
            player = new Player("Mike", playerObj);
        } else
        {
            playerObj = player.getPlayer("Mike");
            Debug.Log(playerObj);
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

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        Debug.Log($"x: {playerObj.transform.localPosition.x}," +
            $"y: {playerObj.transform.localPosition.y}," +
            $"z: {playerObj.transform.localPosition.z}");
        switch (collider2D.name)
        {
            case "Lab":
                Destroy(playerObj);
                SceneManager.LoadScene(Scenes.Lab.ToString());
                player.getPlayer().transform.localPosition = new Vector3(0, -5, -3);
                break;
            case "Workshop":
                DontDestroyOnLoad(playerObj);
                SceneManager.LoadScene(Scenes.Workshop.ToString());
                player.getPlayer().transform.localPosition = new Vector3(-2, 7, -1);
                break;
            case "Workshop f Storage":
                DontDestroyOnLoad(playerObj);
                SceneManager.LoadScene(Scenes.Workshop.ToString());
                player.getPlayer().transform.localPosition = new Vector3(-2, -6, -3);
                break;
            case "Workshop f LivingRoom":
                DontDestroyOnLoad(playerObj);
                SceneManager.LoadScene(Scenes.Workshop.ToString());
                player.getPlayer().transform.localPosition = new Vector3(15, -5, -3);
                break;
            case "Storage":
                DontDestroyOnLoad(playerObj);
                SceneManager.LoadScene(Scenes.Storage.ToString());
                player.getPlayer().transform.localPosition = new Vector3(0, 7, -1);
                break;
            case "LivingRoom":
                DontDestroyOnLoad(playerObj);
                SceneManager.LoadScene(Scenes.LivingRoom.ToString());
                player.getPlayer().transform.localPosition = new Vector3(-16, 4, -1);
                break;
            case "LivingRoom f Kitchen":
                DontDestroyOnLoad(playerObj);
                SceneManager.LoadScene(Scenes.LivingRoom.ToString());
                player.getPlayer().transform.localPosition = new Vector3(-6, 7, -1);
                break;
            case "LivingRoom f Elevator":
                DontDestroyOnLoad(playerObj);
                SceneManager.LoadScene(Scenes.LivingRoom.ToString());
                player.getPlayer().transform.localPosition = new Vector3(2, -6, -1);
                break;
            case "Kitchen":
                DontDestroyOnLoad(playerObj);
                SceneManager.LoadScene(Scenes.Kitchen.ToString());
                player.getPlayer().transform.localPosition = new Vector3(-12, -6, -3);
                break;
            case "Elevator":
                DontDestroyOnLoad(playerObj);
                SceneManager.LoadScene(Scenes.Elevator.ToString());
                player.getPlayer().transform.localPosition = new Vector3(0, 7, -1);
                break;
            default:
                Debug.Log("Other");
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

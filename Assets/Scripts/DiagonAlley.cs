using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DiagonAlley : MonoBehaviour
{
    private enum Scenes
    {
        Lab,
        Workshop,
        Storage,
        LivingRoom,
        Kitchen,
        Elevator,
    };

    private GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        switch (collider2D.name)
        {
            case "Lab":
                SceneManager.LoadScene(Scenes.Lab.ToString());
                player.transform.localPosition = new Vector3(0, -5, -3);
                break;
            case "Workshop":
                SceneManager.LoadScene(Scenes.Workshop.ToString());
                player.transform.localPosition = new Vector3(-2, 7, -1);
                break;
            case "Workshop f Storage":
                SceneManager.LoadScene(Scenes.Workshop.ToString());
                player.transform.localPosition = new Vector3(-2, -6, -3);
                break;
            case "Workshop f LivingRoom":
                SceneManager.LoadScene(Scenes.Workshop.ToString());
                player.transform.localPosition = new Vector3(15, -5, -3);
                break;
            case "Storage":
                SceneManager.LoadScene(Scenes.Storage.ToString());
                player.transform.localPosition = new Vector3(0, 7, -1);
                break;
            case "LivingRoom":
                SceneManager.LoadScene(Scenes.LivingRoom.ToString());
                player.transform.localPosition = new Vector3(-16, 4, -1);
                break;
            case "LivingRoom f Kitchen":
                SceneManager.LoadScene(Scenes.LivingRoom.ToString());
                player.transform.localPosition = new Vector3(-6, 7, -1);
                break;
            case "LivingRoom f Elevator":
                SceneManager.LoadScene(Scenes.LivingRoom.ToString());
                player.transform.localPosition = new Vector3(2, -6, -1);
                break;
            case "Kitchen":
                SceneManager.LoadScene(Scenes.Kitchen.ToString());
                player.transform.localPosition = new Vector3(-12, -6, -3);
                break;
            case "Elevator":
                SceneManager.LoadScene(Scenes.Elevator.ToString());
                player.transform.localPosition = new Vector3(0, 7, -1);
                break;
            default:
                break;
        }
    }
}

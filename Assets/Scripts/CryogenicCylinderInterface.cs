using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro;

public class CryogenicCylinderInterface : MonoBehaviour
{
    private InputField input;
    
    public void Start()
    {
        // Get input field
        input = GameObject.Find("InputField").GetComponent<InputField>();        
    }

    public void FreePlayer()
    {
            // Empty cryogenic cylinder
            Image cylinder = GameObject.Find("Cylinder").GetComponent<UnityEngine.UI.Image>();
            Image emptyCylinder = GameObject.Find("EmptyCylinder").GetComponent<UnityEngine.UI.Image>();
            cylinder.enabled = false;
            emptyCylinder.enabled = true;
    }

    public void CheckCode()
    {
        // Set secret code 
        String code = "4242";
        // Check if the code is correct
        if (input.text == code) {
            input.text = "Félicitations !";
            GameObject keypad = GameObject.Find("Containers");
            keypad.SetActive(false);
            // Display Success Message
            TextMeshPro successMsg = GameObject.Find("successMessage").GetComponent<TextMeshPro>();
            // successMsg.gameObject.SetActive(true);
            // Call FreePlayer in 5 seconds
            Invoke("FreePlayer", 5);
            // TODO : unlock character
            // quit interface
        } else {
            input.text = "ERREUR: veuillez réessayer !";
        }
    }

    public void OnMouseEnter()
    {
        // Get clicked key
        Button key = GetComponent<Button>();
        // Display it on screen
        input.text += key.GetComponentInChildren<Text>().text;
    }

    public void EraseValue()
    {
        // Delete last digit on the screen
        if (input.text.Length > 0)
            input.text = input.text.Remove(input.text.Length - 1, 1);
    }
}

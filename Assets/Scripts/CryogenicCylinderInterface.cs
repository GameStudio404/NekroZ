using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class CryogenicCylinderInterface : MonoBehaviour
{
    private InputField input;
    
    public void Start()
    {
        input = GameObject.Find("InputField").GetComponent<InputField>();        
    }

    public void CheckCode()
    {
        String code = "4242";
        if (input.text == code) {
            input.text = "Félicitations !";
            // change sprite
            // unlock character
            // quit interface
        } else {
            input.text = "Code faux veuillez réessayer !";
        }
    }

    public void OnMouseEnter()
    {
        // Get clicked key
        Button key = GetComponent<Button>();
        // Display it on screen
        // InputField input = GameObject.Find("InputField").GetComponent<InputField>();
        input.text += key.GetComponentInChildren<Text>().text;
    }

    public void EraseValue()
    {
        if (input.text.Length > 0)
            input.text = input.text.Remove(input.text.Length - 1, 1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class KeyBind : MonoBehaviour
{
    InputManager inputManager;
    public GameObject keyItemPrefab;
    public GameObject keyList;
    string buttonToRebind = null;
    Dictionary<string, Text> buttonToLabel;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = GameObject.FindObjectOfType<InputManager>();
        buttonToLabel = new Dictionary<string, Text>();

        //create one Button (ex : keyLeft) per button in inputManager
        string[] buttonNames = inputManager.GetButtonNames();
        
        for (int i = 0; i < buttonNames.Length; i++)
        {
            string bn;
            bn = buttonNames[i];

            GameObject go = (GameObject)Instantiate(keyItemPrefab);
           // go.transform.SetParent(keyList.transform);
            go.transform.localScale = Vector3.one;
            Text buttonNameText = go.transform.Find("keyLeft").GetComponent<Text>();
            buttonNameText.text = bn;
            Debug.Log(go.transform.Find("keyLeft/KeyName").GetComponent<Text>());
            Text keyNameText = go.transform.Find("keyLeft/KeyName").GetComponent<Text>();
            keyNameText.text = inputManager.GetKeyNameForButton(bn);
            buttonToLabel[bn] = keyNameText;
            Button keyBindButton = go.transform.Find("keyLeft").GetComponent<Button>();
            keyBindButton.onClick.AddListener(() => { StartRebindFor(bn); });
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonToRebind != null)
        {
            if (Input.anyKeyDown)
            {
                // which key was press down ?

                // Loop through all possible keys and see if it was pressed down
                foreach (KeyCode kc in Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(kc))
                    {
                        // Yes !
                        inputManager.SetButtonForKey(buttonToRebind, kc);
                        buttonToLabel[buttonToRebind].text = kc.ToString();
                        buttonToRebind = null; // We stop rebinding keys
                        break;
                    }
                }
            }
        }
    }

    void StartRebindFor(string buttonName)
    {
        Debug.Log("Start rebind for: " + buttonName);

        buttonToRebind = buttonName;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyBind : MonoBehaviour
{

    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    public TextMeshProUGUI left, right, up, down, jump, inventory, interact;
    private GameObject currentKey;
    private Color32 normal = new Color32(18, 40, 77, 255);
    private Color32 selected = new Color32(255, 217, 102, 255);

    public void SaveKeys() 
    {
        Debug.Log(keys);
    }
    // Start is called before the first frame update
    void Start()
    {
        keys.Add("Left", KeyCode.Q);
        keys.Add("Right", KeyCode.D);
        keys.Add("Up", KeyCode.Z);
        keys.Add("Down", KeyCode.S);
        keys.Add("Jump", KeyCode.Space);
        keys.Add("Inventory", KeyCode.A);
        keys.Add("Interact", KeyCode.E);

        up.text = keys["Up"].ToString();
        down.text = keys["Down"].ToString();
        left.text = keys["Left"].ToString();
        right.text = keys["Right"].ToString();
        jump.text = keys["Jump"].ToString();
        inventory.text = keys["Inventory"].ToString();
        interact.text = keys["Interact"].ToString();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keys["Up"]))
        {
            // Do a move
            Debug.Log("Up");
        }
        if (Input.GetKeyDown(keys["Down"]))
        {
            // Do a move
            Debug.Log("Down");
        }
        if (Input.GetKeyDown(keys["Left"]))
        {
            // Do a move
            Debug.Log("Left");
        }
        if (Input.GetKeyDown(keys["Right"]))
        {
            // Do a move
            Debug.Log("Right");
        }
        if (Input.GetKeyDown(keys["Jump"]))
        {
            // Do a move
            Debug.Log("Jump");
        }
        if (Input.GetKeyDown(keys["Inventory"]))
        {
            // Do a move
            Debug.Log("Inventory");
        }
        if (Input.GetKeyDown(keys["Interact"]))
        {
            // Do a move
            Debug.Log("Interact");
        }
    }

    void OnGUI()
    {
        if (currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                keys[currentKey.name] = e.keyCode;
                currentKey.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = e.keyCode.ToString();
                currentKey.GetComponent<Image>().color = normal;
                currentKey = null;
            }
        }
    }

    public void ChangeKey(GameObject clicked)
    {
        if (currentKey != null)
        {
            currentKey.GetComponent<Image>().color = normal;
        }
        currentKey = clicked;
        currentKey.GetComponent<Image>().color = selected;
    }

    public Dictionary<string, KeyCode> GetKeys() {
        return keys;
    }
}

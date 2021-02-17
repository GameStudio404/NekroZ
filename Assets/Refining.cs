using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Refining : MonoBehaviour
{
    public GameObject Modal, CM;

    // Start is called before the first frame update
    void Start()
    {
        CM.GetComponent<Button>().onClick.AddListener(() => closeModal());
        Modal.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Modal.gameObject.SetActive(!Modal.activeSelf);
        }
    }

    public void closeModal()
    {
        Debug.Log("lol");
        Modal.gameObject.SetActive(false);
    }

}

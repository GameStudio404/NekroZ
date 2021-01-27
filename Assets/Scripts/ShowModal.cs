using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowModal : MonoBehaviour
{
    public GameObject Modal;

    void Start()
    {
        Modal = GameObject.FindWithTag("Workshop modal");
        Modal.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Modal.gameObject.SetActive(!Modal.activeSelf);
        }
    }
}

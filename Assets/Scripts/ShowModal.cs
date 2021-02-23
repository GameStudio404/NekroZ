using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowModal : MonoBehaviour
{
    private GameObject Modal;

    void Start()
    {
        Modal = GameObject.FindWithTag("Workshop modal");
    }

    void Update()
    {
    }
}

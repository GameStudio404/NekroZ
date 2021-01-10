using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoCommandes : MonoBehaviour
{
    //public event EventHandler OnKeyDown;
    public GameObject Z, Q, S, D, canvas;
    public int TutoComplete;

    // Start is called before the first frame update
    void Start()
    {
        TutoComplete = 0;
        if (Z == null && Q == null && S == null && D == null)
        {
            Z = GameObject.FindWithTag("Z");
            Q = GameObject.FindWithTag("Q");
            S = GameObject.FindWithTag("S");
            D = GameObject.FindWithTag("D");
            canvas = GameObject.FindWithTag("Tuto canvas");
        }
        //OnKeyDown += testing_OnKeyDown;
    }

    void IsTutoComplete()
    {
        if (TutoComplete == 4)
        {
            Destroy(canvas);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Destroy(Z);
            TutoComplete += 1;
            IsTutoComplete();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            Destroy(Q);
            TutoComplete += 1;
            IsTutoComplete();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Destroy(S);
            TutoComplete += 1;
            IsTutoComplete();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Destroy(D);
            TutoComplete += 1;
            IsTutoComplete();
        }
    }
}

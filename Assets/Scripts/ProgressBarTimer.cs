using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

//[ExecuteInEditMode()]
public class ProgressBarTimer : MonoBehaviour
{
    public int begin, goal;
    public Image mask;
    public Text on, to, reach, fuse;
    private static float current;
    private float fill;
    private int toFuse;

    // Start is called before the first frame update
    void Start()
    {
        goal = 3;
        begin = 0;
    }

    // Update is called once per frame
    void Update()
    {
        toFuse = int.Parse(fuse.text);
        if (toFuse > 0)
        {
            on.text = $"{Math.Round(current)}s";
            reach.text = "";
            to.text = $"{goal}s";
            fill = current / (float)goal;
        }
        if (current < (float)goal)
        {
            current += Time.deltaTime;
            GetFill();
        }
    }

    //fill amount
    void GetFill()
    {
        if (current >= (float)goal)
        {
            toFuse -= 1;
            fuse.text = toFuse.ToString();
            if (toFuse > 0)
            {
                current = 0F;    
            }
            else
            {
                current = 0f;
                on.text = "";
                to.text = "";
                reach.text = "Done";
            }
        }
        mask.fillAmount = fill;
    }
}

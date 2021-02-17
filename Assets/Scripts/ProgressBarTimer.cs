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
        toFuse = int.Parse(fuse.text);
        goal = 3;
        begin = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (toFuse > 0)
        {
            on.text = $"{Math.Round(current)}s";
            to.text = $"{goal}s";
            fill = current / (float)goal;
        }
        //Debug.Log($"current: {Math.Round(current)}, current: {current}");
        if (current < (float)goal)
        {
            current += Time.deltaTime;
            GetFill();
        }
    }

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
                on.text = "";
                to.text = "";
                reach.text = "Done";
            }
        }
        mask.fillAmount = fill;
    }
}

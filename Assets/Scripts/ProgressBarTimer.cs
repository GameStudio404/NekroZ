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
    private int goal;
    private Image mask;
    private Text on, to, reach, fuse;
    private static float current;
    private float fill;
    private int toFuse;

    // Start is called before the first frame update
    void Start()
    {
        Image go = GetComponent<Image>();
        mask = go.transform.GetChild(0).GetComponent<Image>();
        on = go.transform.GetChild(1).GetComponent<Text>();
        to = go.transform.GetChild(2).GetComponent<Text>();
        reach = go.transform.GetChild(3).GetComponent<Text>();
        fuse = GameObject.FindWithTag("Fuse").transform.GetChild(1).GetComponent<Text>();
        //Debug.Log($"mask: {mask.name}, on: {on.text}, to: {to.text}, reach: {reach.text}, fuse: {fuse.name}");
    }

    // Update is called once per frame
    void Update()
    {
        toFuse = int.Parse(fuse.text);
        goal = int.Parse(GameObject.FindWithTag("Fuse").transform.GetChild(2).GetComponent<Text>().text);
        if (toFuse > 0)
        {
            on.text = $"{Math.Round(current)}s";
            reach.text = "";
            to.text = $"{goal}s";
            fill = current / (float)goal;
        }
        if (current < (float)goal && toFuse > 0)
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
                goal = 0;
                reach.text = "Done";
            }
        }
        mask.fillAmount = fill;
    }
}

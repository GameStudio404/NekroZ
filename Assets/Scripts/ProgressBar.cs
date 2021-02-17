using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    public int min, max, current;
    public Image mask;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        max = 100;
        min = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GetFill();
    }

    void GetFill()
    {
        float fill = (float)current / (float)max;
        if (fill <= .1)
            mask.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color32(255, 87, 0, 255);
        else if (fill <= .25)
            mask.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color32(255, 185, 0, 255);
        else if (fill <= .5)
            mask.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color32(255, 255, 0, 255);
        else
            mask.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color32(29, 255, 0, 255);
        mask.fillAmount = fill;
        text.text = $"{fill * 100}%";
    }
}

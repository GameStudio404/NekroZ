using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode()]
public class myFire : MonoBehaviour
{
    public GameObject fireplace;
    Sprite[] fires;
    static int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        fires = Resources.LoadAll<Sprite>("Fireplace");
    }

    // Update is called once per frame
    void Update()
    {
        System.Threading.Thread.Sleep(50);
        fireplace.transform.GetChild(0).GetComponentInChildren<Image>().sprite = fires[i++];
        if (i == fires.Length - 1)
            i = 0;
    }
}

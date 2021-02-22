using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IDropHandler
{
    public GameObject go;

    public void OnDrop(PointerEventData data)
    {
        if (data.pointerDrag != null)
            dispatch(data);
    }

    void Update()
    {
        int remaining = int.Parse(go.transform.GetChild(1).GetComponent<Text>().text);

        if (remaining == 0 && go.transform.GetChild(2).gameObject.active == true)
        {
            go.transform.GetChild(0).GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("utils")[1];
            go.transform.GetChild(2).gameObject.SetActive(false);
        }
        else if (remaining > 0)
            go.transform.GetChild(2).gameObject.SetActive(true);

    }

    private void dispatch(PointerEventData data)
    {
        go.transform.GetChild(0).GetComponent<Image>().sprite = data.pointerDrag.transform.GetChild(0).GetComponent<Image>().sprite;
        int total = int.Parse(data.pointerDrag.transform.GetChild(1).GetComponent<Text>().text);
        Debug.Log($"total: {data.pointerDrag.transform.GetChild(2).GetComponent<Text>().text}");
        int selected = int.Parse(data.pointerDrag.transform.GetChild(2).GetComponent<Text>().text != "" ? data.pointerDrag.transform.GetChild(2).GetComponent<Text>().text : "0");
        data.pointerDrag.transform.GetChild(2).GetComponent<Text>().text = "";
        data.pointerDrag.transform.GetChild(1).GetComponent<Text>().text = (total - selected).ToString();
        go.transform.GetChild(1).GetComponent<Text>().text = selected > 0 ? selected.ToString() : total.ToString();
    }
}

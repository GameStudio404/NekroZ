using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rt;
    private CanvasGroup cg;
    private Vector3 origin;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
        cg = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData data)
    {
        cg.blocksRaycasts = false;
        origin = rt.position;
    }

    //item follows the mouse
    public void OnDrag(PointerEventData data)
    {
        rt.anchoredPosition += data.delta * new Vector2((float)0.15, (float)0.8);
    }

    public void OnEndDrag(PointerEventData data)
    {
        cg.blocksRaycasts = true;
        rt.position = origin;
    }
}

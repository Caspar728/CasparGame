using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ZoomUi : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public float zoomSize;

    public void OnPointerEnter(PointerEventData pointerEventData)//固定格式，达成鼠标进入效果（悬浮）
    {
        transform.localScale = new Vector3(zoomSize, zoomSize, 1.0f);//卡牌变大代码：x轴，y轴，z轴
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        transform.localScale = Vector3.one;
    }
}
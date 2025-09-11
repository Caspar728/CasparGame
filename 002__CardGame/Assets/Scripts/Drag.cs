using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform parentToReturnTo = null;
    private CanvasGroup canvasGroup;
    private bool isOverDropZone = false; // 是否在放置区域上方
    private GameObject currentDropZone = null; // 当前所在的放置区域

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentToReturnTo = transform.parent;
        transform.SetParent(transform.root); // 设置为最顶层的Canvas，确保显示在最前
        canvasGroup.blocksRaycasts = false; // 阻止射线阻挡，允许检测下方的DropZone
        Debug.Log("拿起了卡牌");
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        // 实时检测是否在DropZone上方
        CheckUnderPointer(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; // 恢复射线阻挡

        if (isOverDropZone && currentDropZone != null)
        {
            // 如果在放置区域上方，则设置父对象为DropZone
            transform.SetParent(currentDropZone.transform);
            Debug.Log("卡牌放置在放置区域");
        }
        else
        {
            // 如果不在放置区域上方，则返回原位置
            transform.SetParent(parentToReturnTo);
            Debug.Log("卡牌返回原位置");
        }
        // 重置状态
        isOverDropZone = false;
        currentDropZone = null;
    }

    private void CheckUnderPointer(PointerEventData eventData)
    {
        // 创建一个列表存储射线检测结果
        List<RaycastResult> results = new List<RaycastResult>();
        // 在鼠标/指针位置进行射线检测
        EventSystem.current.RaycastAll(eventData, results);

        isOverDropZone = false;
        currentDropZone = null;

        // 遍历所有检测到的物体
        foreach (RaycastResult result in results)
        {
            // 检查物体是否有DropZone组件
            AIDropZone dz = result.gameObject.GetComponent<AIDropZone>();
            if (dz != null)
            {
                isOverDropZone = true;
                currentDropZone = result.gameObject;
                break; // 找到一个DropZone就退出循环
            }
        }
    }
}
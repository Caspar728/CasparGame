using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; // 如果需要处理UI元素，保留这行

public class AIDropZone : MonoBehaviour, IDropHandler
{
    // 当物体被放置到这个区域时调用
    public void OnDrop(PointerEventData eventData)
    {
        // 获取被拖拽的物体
        GameObject draggedItem = eventData.pointerDrag;
        if (draggedItem != null)
        {
            // 将被拖拽的物体的父节点设置为这个DropZone
            draggedItem.transform.SetParent(transform);

            // （可选）重置位置，使卡牌在DropZone内整齐放置
            Drag draggableComponent = draggedItem.GetComponent<Drag>();
            if (draggableComponent != null)
            {
                // 假设Draggable脚本里有一个方法来重置位置或进行对齐
                // draggableComponent.SnapToPosition();
                // 或者直接重置局部位置为零
                draggedItem.transform.localPosition = Vector3.zero;
            }
            else
            {
                // 如果没有额外的Draggable组件，也直接重置位置
                draggedItem.transform.localPosition = Vector3.zero;
            }

            Debug.Log("卡牌已放置在: " + gameObject.name);
        }
    }

    // （可选）添加其他方法，如OnPointerEnter、OnPointerExit来提供视觉反馈
}
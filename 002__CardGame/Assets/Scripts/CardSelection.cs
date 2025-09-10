using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class CardController : MonoBehaviour
{
    public Image selection;
    private void EnsureSelectionIsInitialized()
    {
        if (selection == null)
        {
            // 尝试从自身获取Image组件
            selection = GetComponent<Image>();

            // 如果自身没有，则尝试创建一个默认的Image
            if (selection == null)
            {
                GameObject selectionObj = new GameObject("SelectionIndicator");
                selectionObj.transform.SetParent(transform);
                selection = selectionObj.AddComponent<Image>();
                // 可以设置一个默认的颜色和大小
                selection.color = new Color(1, 1, 0, 0.5f); // 半透明白色
                selection.enabled = false;
                Debug.LogWarning("创建了默认的选择指示器，建议在Inspector中手动指定");
            }
        }
    }

    void Start()
    {
        EnsureSelectionIsInitialized();
    }

    void Update()
    {
        
    }

    public void ShowImage()    // 显示图片
    {
        EnsureSelectionIsInitialized();
        if (selection != null)
            selection.enabled = true;
    }
    public void HideImage()    // 隐藏图片
    {
        EnsureSelectionIsInitialized();
        if (selection != null)
            selection.enabled = false;
    }
    public void ToggleImage()    // 切换显示状态
    {
        selection.enabled = !selection.enabled;
    }
}
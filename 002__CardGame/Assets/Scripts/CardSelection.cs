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
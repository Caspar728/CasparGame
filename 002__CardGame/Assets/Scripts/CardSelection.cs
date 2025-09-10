using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    [Header("卡片控制")]
    public bool CardSelection = false; // 控制Image显示/隐藏的布尔值
    public Image targetImage; // 需要控制的Image组件引用

    private bool previousState; // 记录上一帧的状态，用于检测变化

    void Start()
    {
        // 确保有Image组件引用
        if (targetImage == null)
        {
            // 尝试自动获取Image组件
            targetImage = GetComponentInChildren<Image>(true);

            if (targetImage == null)
            {
                Debug.LogError("未找到Image组件，请手动分配或在子对象中添加Image组件");
            }
        }

        // 初始化状态
        previousState = CardSelection;
        UpdateImageVisibility();
    }

    void Update()
    {
        // 检查状态是否发生变化
        if (CardSelection != previousState)
        {
            UpdateImageVisibility();
            previousState = CardSelection;
        }

        // 当CardSelection为true时执行额外方法
        if (CardSelection)
        {
            YourMethod();
            // 如果只需要执行一次，可以在这里重置CardSelection = false;
        }
    }

    // 更新Image的显示/隐藏状态
    void UpdateImageVisibility()
    {
        if (targetImage != null)
        {
            targetImage.enabled = CardSelection;
            Debug.Log("Image可见性已设置为: " + CardSelection);
        }
    }

    // 你想要执行的方法
    void YourMethod()
    {
        Debug.Log("CardSelection为true! 执行方法中...");

        // 在这里写下你想要执行的代码
    }

    // 公共方法，用于外部控制CardSelection
    public void SetCardSelection(bool state)
    {
        CardSelection = state;
    }

    // 切换CardSelection状态
    public void ToggleCardSelection()
    {
        CardSelection = !CardSelection;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DeckManager : MonoBehaviour
{
    public Transform deckPanel;
    public Transform libraryPanel;

    public GameObject deckPrefab;
    public GameObject cardPrefab;

    public GameObject DataManager;//获取玩家数据

    // 卡组限制设置
    public int maxDeckSize = 5; // 卡组最大总张数
    public int maxCardCopies = 1; // 单种卡牌最大可携带数量
    public int maxUniqueCards = 5; // 卡组最大不同种类数量

    public  PlayerData PlayerData;
    private CardStore CardStore;

    private Dictionary<int, GameObject> libraryDic = new Dictionary<int, GameObject>();
    private Dictionary<int, GameObject> deckDic = new Dictionary<int, GameObject>();

    // 在Inspector中拖拽libraryCard预制体到这里
    public GameObject libraryCardPrefab;

    // 可以添加一个UI文本元素来显示错误信息
    public Text errorMessage;
    public Text reminder;

    void Start()
    {
        PlayerData = DataManager.GetComponent<PlayerData>();
        CardStore = DataManager.GetComponent<CardStore>();

        UpdateLibray();
        UpdateDeck();

        // 初始化错误信息显示
        if (errorMessage != null)
            errorMessage.text = "";
    }

    public void UpdateLibray()    // 更新牌库显示
    {
        // 先清除现有牌库卡牌
        foreach (var card in libraryDic.Values)
        {
            Destroy(card);
        }
        libraryDic.Clear();

        // 重新创建牌库卡牌
        for (int i = 0; i < PlayerData.playerCards.Length; i++)
        {
            if (PlayerData.playerCards[i] > 0)
            {
                CreatCard(i, CardState.Library);
            }
        }
    }

    public void UpdateDeck()   // 更新卡组显示
    {
        // 先清除现有卡组卡牌
        foreach (var card in deckDic.Values)
        {
            Destroy(card);
        }
        deckDic.Clear();

        // 重新创建卡组卡牌
        for (int i = 0; i < PlayerData.playerDeck.Length; i++)
        {
            if (PlayerData.playerDeck[i] > 0)
            {
                CreatCard(i, CardState.Deck);
            }
        }
    }

    // 计算当前卡组总张数
    private int GetCurrentDeckSize()
    {
        int count = 0;
        foreach (int amount in PlayerData.playerDeck)
        {
            count += amount;
        }
        return count;
    }

    // 计算当前卡组中不同种类卡牌数量
    private int GetUniqueCardCount()
    {
        int count = 0;
        foreach (int amount in PlayerData.playerDeck)
        {
            if (amount > 0)
                count++;
        }
        return count;
    }


    public void UpdateCard(CardState _state, int _id)    // 处理卡牌状态更新（移动卡牌）
    {
        // 清除错误信息
        if (errorMessage != null)
            errorMessage.text = "";

        if (_state == CardState.Library)
        {
            // 从牌库点击 - 添加到卡组
            if (!deckDic.ContainsKey(_id) || PlayerData.playerDeck[_id] > 0)
            {
                // 检查单种卡牌数量限制
                if (PlayerData.playerDeck[_id] >= maxCardCopies)
                {
                    reminder.text = "已达到该卡牌的最大携带数量！";
                    ShowError("已达到该卡牌的最大携带数量！");
                    return;
                }

                // 检查卡组总张数限制
                if (GetCurrentDeckSize() >= maxDeckSize)
                {
                    reminder.text = "已达到卡组最大容量！";
                    ShowError("已达到卡组最大容量！");
                    return;
                }

                // 检查卡组种类限制（如果是新添加的卡牌）
                if (PlayerData.playerDeck[_id] == 0 && GetUniqueCardCount() >= maxUniqueCards)
                {
                    reminder.text = "已达到卡组最大种类数量！";
                    ShowError("已达到卡组最大种类数量！");
                    return;
                }

                // 增加卡组中该卡牌的数量
                PlayerData.playerDeck[_id]++;
                // 更新卡组显示
                UpdateDeck();
            }
        }
        else if (_state == CardState.Deck)
        {
            // 从卡组点击 - 从卡组移除
            if (deckDic.ContainsKey(_id))
            {
                // 减少卡组中该卡牌的数量
                PlayerData.playerDeck[_id]--;
                // 如果数量为0，从字典中移除
                if (PlayerData.playerDeck[_id] > 0)
                {
                    deckDic.Remove(_id);
                }
                // 更新卡组显示
                UpdateDeck();
            }
        }
    }

    // 显示错误信息
    private void ShowError(string message)
    {
        if (errorMessage != null)
        {
            errorMessage.text = message;
            // 可以添加一个协程来自动隐藏错误信息
            StartCoroutine(HideErrorAfterDelay(2f));
        }
        Debug.LogWarning(message);
    }

    // 延迟隐藏错误信息
    private IEnumerator HideErrorAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (errorMessage != null)
            errorMessage.text = "";
    }

    // 创建卡牌并添加点击事件
    public void CreatCard(int _id, CardState _cardState)
    {
        Transform targetPanel;
        GameObject targetPrefab;
        var refData = PlayerData.playerCards;
        Dictionary<int, GameObject> targetDic = libraryDic;

        if (_cardState == CardState.Library)
        {
            targetPanel = libraryPanel;
            targetPrefab = cardPrefab;
        }
        else
        {
            targetPanel = deckPanel;
            targetPrefab = deckPrefab;
            refData = PlayerData.playerDeck;
            targetDic = deckDic;
        }

        GameObject newCard = Instantiate(targetPrefab, targetPanel);
        // 设置卡牌计数
        newCard.GetComponent<CardCounter>().SetCounter(refData[_id]);
        // 设置卡牌显示数据
        newCard.GetComponent<CardDisplay>().card = CardStore.cardList[_id];
        // 添加到对应字典
        targetDic[_id] = newCard;

        // 为卡牌添加点击事件
        Button cardButton = newCard.GetComponent<Button>();
        if (cardButton == null)
        {
            cardButton = newCard.AddComponent<Button>();
        }
        // 移除现有监听，避免重复添加
        cardButton.onClick.RemoveAllListeners();
        // 添加点击事件，调用UpdateCard方法
        cardButton.onClick.AddListener(() => OnCardClicked(_cardState, _id));
    }

    // 卡牌点击处理方法
    private void OnCardClicked(CardState state, int cardId)
    {
        UpdateCard(state, cardId);
    }
}


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

    private PlayerData PlayerData;
    private CardStore CardStore;

    private Dictionary<int, GameObject> libraryDic = new Dictionary<int, GameObject>();
    private Dictionary<int, GameObject> deckDic = new Dictionary<int, GameObject>();


    void Start()
    {
        PlayerData = DataManager.GetComponent<PlayerData>();
        CardStore = DataManager.GetComponent<CardStore>();

        UpdateLibray();
        UpdateDeck();
    }

    // 更新牌库显示
    public void UpdateLibray()
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

    // 更新卡组显示
    public void UpdateDeck()
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

    // 处理卡牌状态更新（移动卡牌）
    public void UpdateCard(CardState _state, int _id)
    {
        if (_state == CardState.Library)
        {
            // 从牌库点击 - 添加到卡组
            if (!deckDic.ContainsKey(_id))
            {
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
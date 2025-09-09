using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{

    public Transform deckPanel;
    public Transform libraryPanel;

    public GameObject deckPrefab;
    public GameObject cardPrefab;

    public GameObject DataManager;//获取玩家数据，下面两个脚本在这里



    private PlayerData PlayerData;//为了获取这个和下面的脚本
    private CardStore CardStore;

    private Dictionary<int, GameObject> libraryDic = new Dictionary<int, GameObject>();
    private Dictionary<int, GameObject> deckDic = new Dictionary<int, GameObject>();
    //private Dictionary<int, CardCounter> libraryCounters = new Dictionary<int, CardCounter>();//AI
    //private Dictionary<int, CardCounter> deckCounters = new Dictionary<int, CardCounter>();//AI


    // Start is called before the first frame update
    void Start()
    {
        PlayerData = DataManager.GetComponent<PlayerData>();
        CardStore = DataManager.GetComponent<CardStore>();

        UpdateLibray();
        UpdateDeck();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateLibray()//显示牌库
    {
        for (int i = 0; i < PlayerData.playerCards.Length; i++)
        {
            if (PlayerData.playerCards[i] > 0)
            {
                CreatCard(i, CardState.Library);
            }

        }
    }
    public void UpdateDeck()//显示卡组
    {
        for (int i = 0; i < PlayerData.playerDeck.Length; i++)
        {
            if (PlayerData.playerDeck[i] > 0)
            {
                CreatCard(i, CardState.Deck);
            }
        }
    }

    public void UpdateCard(CardState _state, int _id)
    {
        if (_state == CardState.Deck)//卡组数据库中的状态
        {
            PlayerData.playerDeck[_id]--;
            PlayerData.playerCards[_id]++;


            if (!deckDic[_id].GetComponent<CardCounter>().SetCounter(-1))
            //找到卡组UI中对应的卡牌物体，调用其上的"CardCounter"脚本的SetCounter方法，将其计数减1。
            {
                deckDic.Remove(_id);//移开,当数量为0则从字典移除掉
            }
            if (libraryDic.ContainsKey(_id))
            {
                libraryDic[_id].GetComponent<CardCounter>().SetCounter(1);
            }
            else
            {
                CreatCard(_id, CardState.Library);
            }
        }
        else if (_state == CardState.Library)
        //if (_state == CardState.Library)
        {
            PlayerData.playerDeck[_id]++;
            PlayerData.playerCards[_id]--;



            if (deckDic.ContainsKey(_id))//如果卡组中存在
            {
                deckDic[_id].GetComponent<CardCounter>().SetCounter(1);//卡组加一
            }
            else//如果卡组中不存在
            {
                //deckDic[_id].GetComponent<CardCounter>().SetCounter(1);
                CreatCard(_id, CardState.Deck);//打印这张
            }
            if (!libraryDic[_id].GetComponent<CardCounter>().SetCounter(-1))
            {
                libraryDic.Remove(_id);
            }
        }
        return;
    }
    public void CreatCard(int _id, CardState _cardState)
    {
        Transform targetPanel;
        GameObject targetPrefab;
        var refData = PlayerData.playerCards;
        Dictionary<int, GameObject> targetDic = libraryDic;
        if (_cardState == CardState.Library)//这里引用了枚举
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
        newCard.GetComponent<CardCounter>().SetCounter(refData[_id]);
        newCard.GetComponent<CardDisplay>().card = CardStore.cardList[_id];
        //targetDic.Add(_id, newCard);
        targetDic[_id] = newCard;

    }
}
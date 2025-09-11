using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum GamePhase
{
    gameStart, playerDraw, playerAction, enemyDraw, enemyAction, AllBeyondNum
}
public enum GamePhaseAction
{
    trunBegins, beyondNum, trunEnd
}
public class BattleScene : MonoBehaviour
{
    public PlayerData playerData;
    public PlayerData enemyData;
    public DeckManager deckManager; // 添加DeckManager引用

    // 卡组列表
    public List<Card> playerDeckList = new List<Card>();
    public List<Card> enemyDeckList = new List<Card>();

    // 牌库列表
    public List<Card> playerCardList = new List<Card>();
    public List<Card> enemyCardList = new List<Card>();

    // 手牌列表
    public List<Card> playerHandCards = new List<Card>();
    public List<Card> enemyHandCards = new List<Card>();

    public GameObject cardPrefab;
    public Transform playerHand;
    public Transform enemyHand;
    public GameObject[] Blocks;
    public GameObject playerIcon;
    public GameObject enemyIcon;

    //当前游戏状态管理
    public GamePhase GamePhase = GamePhase.gameStart;
    public GamePhaseAction GamePhaseAction = GamePhaseAction.trunBegins;

    public Text pointer;
    void Start()
    {
        GameStart();
    }

    void Update()
    {

    }

    public void GameStart()
    {

        ReadDeck();//读取卡组数据到牌库
        ShuffletCard(0);//玩家牌库洗牌
        ShuffletCard(1);//敌人牌库洗牌

        // 抽取初始手牌
        DrawCard(0, 4);
        DrawCard(1, 4);

        // 初始化游戏阶段为玩家抽卡阶段
        GamePhase = GamePhase.playerDraw;
    }

    public void ReadDeck()
    {
        // 清空现有牌库
        playerDeckList.Clear();
        enemyDeckList.Clear();

        // 加载玩家卡牌到牌库
        for (int i = 0; i < playerData.playerDeck.Length; i++)
        {
            if (playerData.playerDeck[i] > 0)
            {
                int count = playerData.playerDeck[i];
                for (int j = 0; j < count; j++)
                {
                    playerDeckList.Add(playerData.CardStore.CopyCard(i));
                }
            }
        }
        for (int i = 0; i < playerData.playerCards.Length; i++)
        {
            if (playerData.playerCards[i] > 0)
            {
                int count = playerData.playerCards[i];
                for (int j = 0; j < count; j++)
                {
                    playerCardList.Add(playerData.CardStore.CopyCard(i));
                }
            }
        }

        // 加载敌人卡牌到牌库
        for (int i = 0; i < enemyData.playerDeck.Length; i++)
        {
            if (enemyData.playerDeck[i] > 0)
            {
                int count = enemyData.playerDeck[i];
                for (int j = 0; j < count; j++)
                {
                    enemyDeckList.Add(enemyData.CardStore.CopyCard(i));
                }
            }
        }
        for (int i = 0; i < enemyData.playerCards .Length; i++)
        {
            if (enemyData.playerCards[i] > 0)
            {
                int count = enemyData.playerCards[i];
                for (int j = 0; j < count; j++)
                {
                    enemyCardList.Add(enemyData.CardStore.CopyCard(i));
                }
            }
        }
    }

    public void ShuffletCard(int _player)//0为玩家，1为敌人
    {
        List<Card> ShuffletCard = new List<Card>();
        if (_player == 0)
        {
            ShuffletCard = playerCardList;
        }
        else if (_player == 1)
        {
            ShuffletCard = enemyCardList;
        }

        // 洗牌算法
        for (int i = 0; i < ShuffletCard.Count; i++)
        {
            int rad = Random.Range(0, ShuffletCard.Count);
            Card temp = ShuffletCard[i];
            ShuffletCard[i] = ShuffletCard[rad];
            ShuffletCard[rad] = temp;
        }
    }

    public void OnPlayerDraw()
    {
        if (GamePhase == GamePhase.playerDraw)
        {
            int gain_num = 1;
            DrawCard(0, gain_num);
            GamePhase = GamePhase.playerAction;
        }
    }

    public void OnEnemyDraw()
    {
        if (GamePhase == GamePhase.enemyDraw)
        {
            int gain_num = 1;
            DrawCard(1, gain_num);
            GamePhase = GamePhase.enemyAction;
        }
    }

    public void DrawCard(int _player, int _count)
    {
        List<Card> drawDeck = new List<Card>();
        List<Card> handCards = new List<Card>();
        Transform handTransform = transform;

        if (_player == 0)
        {
            drawDeck = playerCardList;
            handCards = playerHandCards;
            handTransform = playerHand;
        }
        else if (_player == 1)
        {
            drawDeck = enemyCardList;
            handCards = enemyHandCards;
            handTransform = enemyHand;
        }

        if (drawDeck.Count == 0)
        {
            Debug.LogWarning($"玩家{_player}的牌库已空，无法抽卡！");
            return;
        }

        int actualDraw = Mathf.Min(_count, drawDeck.Count);

        for (int i = 0; i < actualDraw; i++)
        {
            GameObject card = Instantiate(cardPrefab, handTransform);
            card.GetComponent<CardDisplay>().card = drawDeck[0];

            handCards.Add(drawDeck[0]);
            drawDeck.RemoveAt(0);
        }
    }

    public void OnClickTurnEnd()//绑定给按钮
    {
        TurnEnd();
    }

    public void TurnEnd()
    {
        if (GamePhase == GamePhase.playerAction)
        {
            pointer.text = "敌方回合";
            GamePhase = GamePhase.enemyDraw;
            OnEnemyDraw();
        }
        else if (GamePhase == GamePhase.enemyAction)
        {
            pointer.text = "你的回合";
            GamePhase = GamePhase.playerDraw;
            OnPlayerDraw();
        }
    }

    public void trunBegins() { }
    public void beyondNum() { }
    public void trunEnd() { }

    public void TurnAction()
    {
        trunBegins();
        beyondNum();
        trunEnd();
    }
}

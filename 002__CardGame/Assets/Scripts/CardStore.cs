using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardStore : MonoBehaviour
{
    public TextAsset cardData;
    public List<Card> cardList = new List<Card>();//存储,链表，好处是开始不知道数量可以无限新加卡牌
    // Start is called before the first frame update
    void Start()
    {
        //LoadCardData();
        //TestLoad();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LoadCardData()
    {
        string[] dataRow = cardData.text.Split('\n');//用来读取逗号分隔符文件
        foreach (var row in dataRow)
        {
            string[] rowArray = row.Split(',');
            if (rowArray[0] == "#")
            {
                continue;
            }
            else if (rowArray[0] == "monster")
            {
                //新建怪兽卡
                int id = int.Parse(rowArray[1]);
                string name = rowArray[2];
                int upNum = int.Parse(rowArray[3]);
                int downNum = int.Parse(rowArray[4]);
                int leftNum = int.Parse(rowArray[5]);
                int rightNum = int.Parse(rowArray[6]);
                MonsterCard monsterCard = new MonsterCard(id, name, upNum, downNum, leftNum, rightNum);
                cardList.Add(monsterCard);

                Debug.Log("读取到怪兽卡：  " + monsterCard.cardName);
            }
            else if (rowArray[0] == "generalCard")
            {
                //新建武将卡
            }
        }
    }
    public void TestLoad()
    {
        foreach (var item in cardList)
        {
            Debug.Log("卡牌:  " + item.id.ToString() + item.cardName);
        }
    }

    public Card RandomCard()
    {
        Card card = cardList[Random.Range(0, cardList.Count)];
        return card;
        //这里加入抽卡稀有度
    }
    public Card CopyCard(int _id)//在牌库中复制一张卡牌的实例
    {
        Card copyCard = new Card(_id, cardList[_id].cardName);//复制一张同名卡
        if (copyCard is MonsterCard)
        {
            var mostercard = cardList[_id] as MonsterCard;
            copyCard = new MonsterCard(_id, mostercard.cardName, mostercard.upNum, mostercard.downNum, mostercard.leftNum, mostercard.rightNum);//实例所有部分
        }
        else if (cardList[_id] is weaponCard)
        {
            var generalCard = cardList[_id] as weaponCard;
            copyCard = new weaponCard(_id, generalCard.cardName, generalCard.effect);
        }
        //其他卡牌类型
        return copyCard;
    }
}
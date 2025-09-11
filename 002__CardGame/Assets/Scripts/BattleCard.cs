using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public enum BattleCardState
{
    inHand, inBlock
}
public class BattleCard : MonoBehaviour, IPointerDownHandler
{
    public int playerID;
    public BattleCardState state = BattleCardState.inHand;
    public void OnPointerDown(PointerEventData eventData)
    {
        //手牌被点击时发起召唤请求
        if (GetComponent<CardDisplay>().card is MonsterCard)//只有是怪兽卡才会响应召唤请求
        {
            if (state == BattleCardState.inHand)//如果在手牌里的话
            {
                
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}


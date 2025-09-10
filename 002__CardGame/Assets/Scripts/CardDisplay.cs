using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Text nameText;
    public Text upNum;
    public Text downNum;
    public Text leftNum;
    public Text rightNum;
    public Text effectText;
    public Text PropText;

    public Image backgroundImage;//卡牌图片
    public Image prop;

    public Card card;



    // Start is called before the first frame update
    void Start()
    {
        // 增加空值检查，避免调用空对象的方法
        if (card != null)
        {
            ShowCard();
        }
        else
        {
            Debug.LogError("card变量未赋值！请在Inspector中指定一个Card实例");
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowCard()
    {
        //nameText.text = card.cardName;
        //if (card is MonsterCard)
        //{
        //    var monster = card as MonsterCard;
        //    upNum.text = monster.upNum.ToString();
        //    downNum.text = monster.downNum.ToString();
        //    leftNum.text = monster.leftNum.ToString();
        //    rightNum.text = monster.rightNum.ToString();

        //    effectText.gameObject.SetActive(false);//文字的隐藏显示
        //}
        //else if (card is weaponCard)
        //{
        //    var spell = card as weaponCard;
        //    //effectText.text = general.effect;

        //}
        // 增加对UI组件的空值检查
        if (nameText != null)
            nameText.text = card?.cardName ?? "未知名称"; // 使用空值运算符容错

        if (card is MonsterCard monster) // 使用模式匹配简化代码
        {
            // 为每个文本组件增加空值检查
            if (upNum != null) upNum.text = monster.upNum.ToString();
            if (downNum != null) downNum.text = monster.downNum.ToString();
            if (leftNum != null) leftNum.text = monster.leftNum.ToString();
            if (rightNum != null) rightNum.text = monster.rightNum.ToString();

            if (effectText != null)
                effectText.gameObject.SetActive(false);
        }
        else if (card is weaponCard weapon) // 类名通常首字母大写，建议改为WeaponCard
        {
            if (effectText != null)
                effectText.text = weapon.effect; // 假设weaponCard有effect属性
        }
    }


}
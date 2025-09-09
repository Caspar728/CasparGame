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
        ShowCard();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowCard()
    {
        nameText.text = card.cardName;
        if (card is MonsterCard)
        {
            var monster = card as MonsterCard;
            upNum.text = monster.upNum.ToString();
            downNum.text = monster.downNum.ToString();
            leftNum.text = monster.leftNum.ToString();
            rightNum.text = monster.rightNum.ToString();

            effectText.gameObject.SetActive(false);//文字的隐藏显示
        }
        else if (card is weaponCard)
        {
            var spell = card as weaponCard;
            //effectText.text = general.effect;

        }
    }


}
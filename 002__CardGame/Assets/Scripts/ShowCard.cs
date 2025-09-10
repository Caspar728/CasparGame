using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCard : MonoBehaviour
{
    public Image  NoShowCard;//隐藏卡牌的图片
    // Start is called before the first frame update
    void Start()
    {
        noShowCard();
    }
    public void noShowCard()
    {
        NoShowCard.sprite = Resources.Load<Sprite>("Path/To/Your/Image");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

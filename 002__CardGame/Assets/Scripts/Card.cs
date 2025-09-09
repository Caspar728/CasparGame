public class Card
{
    public int id;
    public string cardName;
    public Card(int _id, string _cardName)//构造函数，括号里面输入变量
    {
        this.id = _id;     //以上每个变量都要
        this.cardName = _cardName;
    }
}

public class MonsterCard : Card//兵团卡（继承）
{
    public int upNum;
    public int downNum;
    public int leftNum;
    public int rightNum;
    //等级、属性也可以加进来
    public MonsterCard(int _id, string _cardName, int _upNum, int _downNum, int _leftNum, int _rightNum) : base(_id, _cardName)
    {
        this.upNum = _upNum;
        this.downNum = _downNum;
        this.leftNum = _leftNum;
        this.rightNum = _rightNum;
    }



}

public class weaponCard : Card//武器卡
{
    public string effect;
    public weaponCard(int _id, string _cardName, string _effect) : base(_id, _cardName)
    {
        this.effect = _effect;
    }
}

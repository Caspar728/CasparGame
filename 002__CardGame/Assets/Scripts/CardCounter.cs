using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardCounter : MonoBehaviour
{
    public Text counterText;
    private int counter = 0;

    public bool SetCounter(int _value)    //后续既可以输入初始值，也可以输入加一减一来改变
    {
        counter += _value;
        OnCounterChange();
        if (counter == 0)
        {
            Destroy(gameObject);
            return false;
        }
        return true;
    }

    private void OnCounterChange()
    {
        //counterText.text = counter.ToString();
        //if (counter == 0)
        //{
        //    Destroy(gameObject);
        //}
        counterText.text = counter.ToString();
    }
}
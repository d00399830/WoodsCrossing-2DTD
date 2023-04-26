using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneySystem : MonoBehaviour
{
    public Text moneyText;
    public static int Money;
    public int startingCash = 100;

    void Start ()
    {
        Money = startingCash;
    }
    
    void Update ()
    {
        moneyText.text = "$" + Mathf.Round(Money).ToString();
    }

    public static void deductCash(int amount)
    {
        Money -= amount;
    }

    public static void addCash(int amount)
    {
        Money += amount;
    }
}

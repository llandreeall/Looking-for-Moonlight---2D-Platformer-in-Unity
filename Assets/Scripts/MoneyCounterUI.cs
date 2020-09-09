using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class MoneyCounterUI : MonoBehaviour
{

    private Text moneyText;


    private void Awake()
    {
        moneyText = GetComponent<Text>();
    }


    void Update()
    {
        moneyText.text = "MONEY: " + GameMaster.Money.ToString();
    }
}

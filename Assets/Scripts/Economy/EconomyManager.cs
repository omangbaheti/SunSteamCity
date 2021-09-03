using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyTextField;
    [SerializeField] private List<char> currencySuffix;
    [SerializeField] private int incrementValue = 1;

    private ulong steamCoin = 0;
    private int economyTimeScale = 1;
    private bool incrementCooldown = true;
    private bool canIncrement = true;
    private int steamCoinBase1000 = 0;
    
    public ulong SteamCoin => steamCoin;

    private void Update()
    {
        if (!incrementCooldown || !canIncrement) return;
        UpdateMoney();
        incrementCooldown = false;
        StartCoroutine(MoneyIncrementCooldown());
    }

    public void SetIncrementValue(int newIncrementValue)
    {
        if (newIncrementValue < 0) throw new ArgumentException("ay yo dis be negative");
        incrementValue = newIncrementValue;
    }
    
    public void AddToIncrementValue(int addIncrementValue)
    {
        incrementValue += addIncrementValue;
    }
    
    public void SubtractFromIncrementValue(int subtractIncrementValue)
    {
        if (subtractIncrementValue > incrementValue) throw new ArgumentException("ay yo dis be negative");;
        incrementValue -= subtractIncrementValue;
    }

    private IEnumerator MoneyIncrementCooldown()
    {
        yield return new WaitForSeconds(1f / economyTimeScale);
        incrementCooldown = true;
    }

    public void ChangeTimeScale(int scaleValue)
    {
        if (scaleValue == economyTimeScale) return;
        StopCoroutine(MoneyIncrementCooldown());
        economyTimeScale = scaleValue;
        canIncrement = economyTimeScale > 0;
    }
    
    private void UpdateMoney()
    {
        steamCoin += (ulong)(incrementValue);
        if (steamCoin > Mathf.Pow(1000, steamCoinBase1000 + 1))
        {
            steamCoinBase1000++;
        }
        
        char currentSuffix = currencySuffix[steamCoinBase1000];
        float base1000Value = steamCoin / Mathf.Pow(1000, steamCoinBase1000);
        string roundedValue = Math.Round(base1000Value, 2).ToString(CultureInfo.CurrentCulture);
        string displayValue = roundedValue + currentSuffix;
        moneyTextField.text = displayValue;
    }
}

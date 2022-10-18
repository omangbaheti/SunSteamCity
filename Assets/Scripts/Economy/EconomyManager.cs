using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public static event Action MoneyUpdated;
    
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
        if (newIncrementValue < 0) throw new ArgumentException("ay yo check dis (SetIncrementValue)");
        incrementValue = newIncrementValue;
    }
    
    public void AddToIncrementValue(int addIncrementValue)
    {
        if (addIncrementValue < 0) throw new ArgumentException("ay yo check dis (AddToIncrementValue)");
        incrementValue += addIncrementValue;
    }
    
    public void SubtractFromIncrementValue(int subtractIncrementValue)
    {
        if (subtractIncrementValue > incrementValue || subtractIncrementValue<0) throw new ArgumentException("ay yo check dis (SubtractFromIncrementValue)");;
        incrementValue -= subtractIncrementValue;
    }

    public void DeductBalance(int price)
    {
        Debug.Log("deducion balance");
        steamCoin -= (ulong)price;
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
        //if performance drops, change this
        char currentSuffix = currencySuffix[steamCoinBase1000];
        float base1000Value = steamCoin / Mathf.Pow(1000, steamCoinBase1000);
        string roundedValueBelow1000 = Math.Round(base1000Value, 2).ToString(CultureInfo.CurrentCulture);
        string roundedValueAbove1000 = $"{Math.Round(base1000Value, 2):0.00}";
        string roundedValue = steamCoinBase1000==0?roundedValueBelow1000: roundedValueAbove1000;
        string displayValue = roundedValue + currentSuffix;
        moneyTextField.text = displayValue;
        MoneyUpdated?.Invoke();
    }
}

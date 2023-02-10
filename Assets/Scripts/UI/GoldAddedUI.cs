using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Updates the text with the addedBalance
/// </summary>
public class GoldAddedUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldAddedDisplay;

    void Awake()
    {
        ShopManager.OnBalanceIncrease += UpdateAddedDisplay;
    }
    private void OnDestroy()
    {
        ShopManager.OnBalanceIncrease -= UpdateAddedDisplay;
    }
    void UpdateAddedDisplay(int addedBalance)
    {
        goldAddedDisplay.text = $"+{addedBalance}";
    }
}

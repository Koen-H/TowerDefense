using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

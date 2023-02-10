using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Updates the UI text with the currentgold.
/// </summary>
public class GoldUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldDisplay;
    [SerializeField] string text;

    // Start is called before the first frame update
    void Awake()
    {
        ShopManager.OnBalanceChange += UpdateBalance;
    }
    private void OnDestroy()
    {
        ShopManager.OnBalanceChange -= UpdateBalance;
    }

    void UpdateBalance(int balance)
    {
        goldDisplay.text = $"{text}\n{balance}";
    }
}

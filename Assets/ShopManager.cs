using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public void OpenShop()
    {
        this.gameObject.SetActive(true);
    }
    public void CloseShop()
    {
        this.gameObject.SetActive(false);
    }
}

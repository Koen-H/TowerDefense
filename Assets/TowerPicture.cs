using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerPicture : MonoBehaviour
{
    [SerializeField] Image towerImage;
    [SerializeField] Image backgroundTexture;


    public void SetTowerPicture(Tower tower)
    {
        switch (tower.GetTowerType())
        {
            case TowerType.Land:
                {
                    backgroundTexture.sprite = Resources.Load<Sprite>("Card/BackgroundSprites/Grass");
                    break;
                }
            case TowerType.Water:
                {
                    backgroundTexture.sprite = Resources.Load<Sprite>("Card/BackgroundSprites/Water");
                    break;
                }
            case TowerType.Shore:
                {
                    backgroundTexture.sprite = Resources.Load<Sprite>("Card/BackgroundSprites/Shore");
                    break;
                }
        }
        towerImage.sprite = tower.stockSprite;
    }
}

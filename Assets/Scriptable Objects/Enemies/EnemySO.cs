using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemySO")]
public class EnemySO : ScriptableObject
{
    public GameObject enemyPrefab;
    [Header ("Enemy Settings")]
    public int defaultHealth;//How much health does it start off with?
    public float goldDrop;//How much gold does the ship drop on death?
    public float speed;//How fast can the enemy move?
    public float rotSpeed;//How fast can the enemy rotate?
    [Header("Enemy Sprites (from max health to low health)")]
    public List<Sprite> sprites = new List<Sprite>();//Sprites will change based on health, Hans told me this was ok and could count as an healthbar.


    /// <summary>
    /// Loops through the list
    /// </summary>
    /// <param name="currentHealth"></param>
    /// <returns>Returns the sprite a enemy should have based on their current health.</returns>
    public Sprite GetCorrectSprite(int currentHealth)
    {
        for (int i = 1; i <= sprites.Count; i++)
        {
           float indexCategory = defaultHealth - (defaultHealth / sprites.Count) * i;
           if (indexCategory < currentHealth) return sprites[i - 1];
        }
        return sprites[0];
    }
}

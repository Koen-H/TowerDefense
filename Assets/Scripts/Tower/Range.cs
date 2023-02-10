using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Range on the tower, it keeps track of which enemies are within range. It can return a target based on a targetingtype.
/// </summary>
public class Range : MonoBehaviour
{
    Tower tower;
    float range;
    List<Enemy> enemiesWithinRange = new List<Enemy>();
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        tower = GetComponentInParent<Tower>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetRange(float newRange)
    {
        range = newRange;
        this.transform.localScale = new Vector3(range, range, range);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy)) enemiesWithinRange.Add(enemy);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy)) enemiesWithinRange.Remove(enemy);
    }

    /// <summary>
    /// Returns a target based on the targeting type
    /// </summary>
    /// <param name="targeting">the type of targeting that is being used</param>
    /// <returns>Enemy class of the target</returns>
    public Enemy GetTarget(TargetingType targeting)
    {
        if (enemiesWithinRange.Count == 0) return null;
        switch (targeting)//Targeting for future, the code is here but for the school assignment we force it to first only.
        {
            case TargetingType.First:
                {
                    return GetFirstEnemy();
                }
            case TargetingType.Farthest:
                {
                    return GetFirstEnemy();
                }
            case TargetingType.Nearest:
                {
                    return GetFirstEnemy();
                }
            case TargetingType.Strongest:
                {
                    return GetFirstEnemy();
                }
            default: return null;
        }
    }

    /// <summary>
    /// Returns the range in float.
    /// </summary>
    /// <returns></returns>
    public float GetRange()
    {
        return range;
    }

    public void ShowRange(bool show)
    {
        spriteRenderer.enabled = show;
    }

    /// <summary>
    /// Returns the first on the path within reach
    /// </summary>
    /// <returns></returns>
    Enemy GetFirstEnemy()
    {
        Enemy firstEnemy = null;
        int firstEnemyNode = 0;
        float firstEnemyNodeDistance = 10000;
        foreach (Enemy enemy in enemiesWithinRange) if (!enemy.ignoredByCannons && (firstEnemy == null || enemy.walker.GetTargetNodeID() >= firstEnemyNode))
            {
                //If they share the same target node, check which one is closer to it
                if (firstEnemyNode == enemy.walker.GetTargetNodeID() && firstEnemyNodeDistance < (enemy.transform.position - enemy.walker.GetTargetNode().transform.position).magnitude) continue;
                firstEnemy = enemy;
                firstEnemyNode = firstEnemy.walker.GetTargetNodeID();
                firstEnemyNodeDistance = (firstEnemy.transform.position - firstEnemy.walker.GetTargetNode().transform.position).magnitude;

                //firstEnemyDistance = new Vector2(enemy.transform.position.x - this.transform.position.x, enemy.transform.position.y - this.transform.position.y).magnitude;
            }
        return firstEnemy;
    }
    /// <summary>
    /// Get the last enemy on the path within reach
    /// </summary>
    /// <returns></returns>
    Enemy GetLastEnemy()
    {
        Enemy firstEnemy = null;
        int firstEnemyNode = 0;
        foreach (Enemy enemy in enemiesWithinRange) if (!enemy.ignoredByCannons && firstEnemy == null || enemy.walker.GetTargetNodeID() >= firstEnemyNode)
            {
                firstEnemy = enemy;
                //firstEnemyDistance = new Vector2(enemy.transform.position.x - this.transform.position.x, enemy.transform.position.y - this.transform.position.y).magnitude;
            }
        return firstEnemy;
    }

    /// <summary>
    /// Get the farthest enemy within reach
    /// </summary>
    /// <returns></returns>
    Enemy GetFarthestEnemy()
    {
        Enemy farthestEnemy = null;
        float farthestEnemyDistance = 0;
        foreach (Enemy enemy in enemiesWithinRange) if (!enemy.ignoredByCannons && farthestEnemy == null || new Vector2(enemy.transform.position.x - this.transform.position.x, enemy.transform.position.y - this.transform.position.y).magnitude < farthestEnemyDistance)
            {
                farthestEnemy = enemy;
                farthestEnemyDistance = new Vector2(enemy.transform.position.x - this.transform.position.x, enemy.transform.position.y - this.transform.position.y).magnitude;
            }
        return farthestEnemy;
    }
    /// <summary>
    /// Get the closest enemy within range
    /// </summary>
    /// <returns></returns>
    Enemy GetNearestEnemy()
    {
        Enemy nearestEnemy = null;
        float nearestDistance = 0;
        foreach (Enemy enemy in enemiesWithinRange) if (!enemy.ignoredByCannons && nearestEnemy == null || new Vector2(enemy.transform.position.x - this.transform.position.x, enemy.transform.position.y - this.transform.position.y).magnitude > nearestDistance )
            {
                nearestEnemy = enemy;
                nearestDistance = new Vector2(enemy.transform.position.x - this.transform.position.x, enemy.transform.position.y - this.transform.position.y).magnitude;
            }
        return nearestEnemy;
    }
    /// <summary>
    /// Get the strongest enemy within range
    /// </summary>
    /// <returns></returns>
    Enemy GetStrongestEnemy()
    {
        Enemy strongestEnemy = null;
        float strongestValue = 0;
        foreach (Enemy enemy in enemiesWithinRange) if (!enemy.ignoredByCannons && strongestEnemy == null || enemy.GetHealth() > strongestValue)
            {
                strongestEnemy = enemy;
                strongestValue = strongestEnemy.GetHealth();//Store it
            }
        return strongestEnemy;
    }


}
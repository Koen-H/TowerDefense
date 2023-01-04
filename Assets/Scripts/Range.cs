using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Range : MonoBehaviour
{
    Tower tower;
    [SerializeField] float range;
    List<Enemy> enemiesWithinRange = new List<Enemy>();

    private void Awake()
    {
        tower = GetComponentInParent<Tower>();
    }

    // Start is called before the first frame update
    void Start()
    {
        this.transform.localScale = new Vector3(range, range, range);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy)) enemiesWithinRange.Add(enemy);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy)) enemiesWithinRange.Remove(enemy);
    }

    public Enemy GetTarget(TargetingType targeting)
    {
        if (enemiesWithinRange.Count == 0) return null;
        switch (targeting)
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

    public float GetRange()
    {
        return range;
    }

    Enemy GetFirstEnemy() //TODO
    {
        Enemy firstEnemy = null;
        int firstEnemyNode = 0;
        foreach (Enemy enemy in enemiesWithinRange) if (firstEnemy == null || enemy.walker.GetTargetNode() >= firstEnemyNode)
            {
                if (firstEnemyNode == enemy.walker.GetTargetNode())
                {

                }
                firstEnemy = enemy;
                //firstEnemyDistance = new Vector2(enemy.transform.position.x - this.transform.position.x, enemy.transform.position.y - this.transform.position.y).magnitude;
            }
        return firstEnemy;
    }


    Enemy GetFarthestEnemy()
    {
        Enemy farthestEnemy = null;
        float farthestEnemyDistance = 0;
        foreach (Enemy enemy in enemiesWithinRange) if (farthestEnemy == null || new Vector2(enemy.transform.position.x - this.transform.position.x, enemy.transform.position.y - this.transform.position.y).magnitude < farthestEnemyDistance)
            {
                farthestEnemy = enemy;
                farthestEnemyDistance = new Vector2(enemy.transform.position.x - this.transform.position.x, enemy.transform.position.y - this.transform.position.y).magnitude;
            }
        return farthestEnemy;
    }

    Enemy GetNearestEnemy()
    {
        Enemy nearestEnemy = null;
        float nearestDistance = 0;
        foreach (Enemy enemy in enemiesWithinRange) if (nearestEnemy == null || new Vector2(enemy.transform.position.x - this.transform.position.x, enemy.transform.position.y - this.transform.position.y).magnitude > nearestDistance)
            {
                nearestEnemy = enemy;
                nearestDistance = new Vector2(enemy.transform.position.x - this.transform.position.x, enemy.transform.position.y - this.transform.position.y).magnitude;
            }
        return nearestEnemy;
    }

    Enemy GetStrongestEnemy()
    {
        Enemy strongestEnemy = null;
        float strongestValue = 0;
        foreach (Enemy enemy in enemiesWithinRange) if (strongestEnemy == null || enemy.GetHealth() > strongestValue)
            {
                strongestEnemy = enemy;
                strongestValue = strongestEnemy.GetHealth();//Store it
            }
        return strongestEnemy;
    }


}

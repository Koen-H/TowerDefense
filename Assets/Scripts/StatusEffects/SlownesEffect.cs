using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class SlownesEffect : StatusEffect
{
    SpriteRenderer spriteRenderer;
    [SerializeField] Color effectColor;

    /// <summary>
    /// Strength of 1 means the new speed is 90% of the default, a strength of 2 means 80% of default and so on. with a 10% of backup
    /// </summary>
    public override void ApplyEffect()
    {
        Debug.LogWarning("should be slow");
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color= effectColor;
        float tenPercentSpeed = enemy.walker.GetSpeed() / 10;
        float slowedSpeed = strength >= 10 ? tenPercentSpeed : System.Math.Abs(tenPercentSpeed * (10 - strength));//If the result is 0 or lower, it becomes 10%
        enemy.walker.SetSpeed(slowedSpeed);
        Debug.Log($"{tenPercentSpeed}, {slowedSpeed}, {strength}");

    }

    public override void RemoveEffect()
    {
        Debug.LogWarning("should no longer be slow");
        spriteRenderer.color = new Color(255,255,255);
        enemy.walker.SetSpeed(enemy.GetEnemyData().speed);
        enemy.statusEffects.Remove(this.GetType());
        Destroy(this);
    }

    public override void CopyFrom(StatusEffect effect)
    {
        if (!(effect is SlownesEffect))
        {
            //exception
            Debug.LogError("Something is wrong with the status effect copy from");
            return;
        }

        SlownesEffect copyFrom = effect as SlownesEffect;
        effectColor = copyFrom.GetEffectColor();
        duration = copyFrom.GetDuration();
        strength = copyFrom.GetStrength();
        Debug.Log($"Copied {strength} from projectile!");
    }

    public Color GetEffectColor()
    {
        return effectColor;
    }
}

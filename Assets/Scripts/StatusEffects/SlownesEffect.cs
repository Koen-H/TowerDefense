using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlownesEffect : StatusEffect
{
    SpriteRenderer spriteRenderer;
    [SerializeField] Color effectColor;


    public override void ApplyEffect()
    {
        Debug.LogWarning("should be slow");
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color= effectColor;
    }

    public override void RemoveEffect()
    {
        Debug.LogWarning("should no longer be slow");
        spriteRenderer.color = new Color(255,255,255);
    }

    public override void CopyFrom(StatusEffect effect)
    {
        if (!(effect is SlownesEffect))
        {
            //exception
        }

        SlownesEffect prototype = effect as SlownesEffect;
        effectColor= prototype.effectColor;
    }
}

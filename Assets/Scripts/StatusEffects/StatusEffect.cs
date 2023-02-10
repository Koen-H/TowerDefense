using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An abstract statuseffect class that can be used to aplly custom status effects on enemies
/// </summary>
public abstract class StatusEffect : MonoBehaviour
{
    [SerializeField] protected float duration;
    [SerializeField] protected float strength;
    protected Enemy enemy;

    private void Start()
    {
        if (gameObject.TryGetComponent<Enemy>(out Enemy e)) {
            enemy = e;
            ApplyEffect();
            StartCoroutine(EffectCountdown());
        }
    }

    /// <summary>
    /// Simple countdown for when the effect should wear off.
    /// The effects duration can change mid-effect.
    /// </summary>
    /// <returns></returns>
    private IEnumerator EffectCountdown()
    {
        while (duration > 0)
        {
            duration -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        RemoveEffect();
    }

    /// <summary>
    /// Reset the effect if the duration is longer
    /// Also increases the strength of the ffect if thew new effect is stronger.
    /// </summary>
    /// <param name="effect"></param>
    public void ResetEffect(StatusEffect effect)
    {
        if (effect.duration > duration) duration = effect.duration;
        if (effect.strength > strength)
        {
            strength = effect.strength;
            ApplyEffect();
        }
    }
    public void SetData(float effectDuration, float effectStrength)
    {
        duration = effectDuration;
        strength = effectStrength;
    }

    public virtual void ApplyEffect()
    {
        Debug.LogWarning("There is no Apply Effect set");
    }

    public virtual void RemoveEffect()
    {
        Debug.LogWarning("There is no Remove Effect set");
    }

    public virtual void CopyFrom (StatusEffect effect)
    {
        Debug.LogWarning("There is no CopyFrom set");
    }

    public float GetDuration()
    {
        return duration;
    }
    public float GetStrength()
    {
        return strength;
    }
}

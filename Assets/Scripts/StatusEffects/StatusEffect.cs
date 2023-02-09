using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect : MonoBehaviour
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

    private IEnumerator EffectCountdown()
    {
        while (duration > 0)
        {
            duration -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        RemoveEffect();
    }

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect : MonoBehaviour
{
    [SerializeField] protected float duration;
    [SerializeField] protected float strength;

    private void Awake()
    {
        if (gameObject.TryGetComponent<Enemy>(out Enemy e)) {
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
        Debug.Log("Effect reset");
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
}

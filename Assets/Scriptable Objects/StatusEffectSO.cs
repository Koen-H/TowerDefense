using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu (menuName ="StatusEffectSO")]
public class StatusEffectSO : ScriptableObject
{
    public float slowdown = 5;

   public void ApplyEffect (MonoBehaviour pEnemy)
    {
        Debug.Log("APPLY");
        Debug.Log(pEnemy);
        pEnemy.StartCoroutine(innerEffect());
    }

    public IEnumerator innerEffect()
    {
        Debug.Log($"Applying slowdown {slowdown} to enemy");
        yield return new WaitForSeconds(3);

        Debug.Log("Done applying effect to enemy");
    }
}

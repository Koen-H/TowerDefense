using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class waveButtonManager : MonoBehaviour
{
    Button button;
    TextMeshProUGUI buttonText;
    private void Start()
    {
        WaveManager.OnEndOfWave += StartNextWaveCountdown;
        button = GetComponent<Button>();
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
    }

    /// <summary>
    /// Starts the next wave countdown, it can be skipped by pressing the button.
    /// </summary>
    /// <param name="wave">The wave that just finished, used for breaktime</param>
    void StartNextWaveCountdown(Wave wave)
    {
        if (wave.isLastWave) return;
        this.gameObject.SetActive(true);
        button.onClick.RemoveAllListeners();
        IEnumerator coroutine = NextWaveCountdown(wave.breakTimeAfterCompletion, wave.waveNumber);
        StartCoroutine(coroutine);
        button.onClick.AddListener(() => StopCoroutine(coroutine));
    }

    public void StartNextWave()
    {
        GameManager.Instance.waveManager.StartWave();
    }

    IEnumerator NextWaveCountdown(int breaktime, int waveNumber)
    {
        for(int i = breaktime; i > 0; i--)
        {
            buttonText.text = $"Wave {waveNumber + 1} in: {i}";
            yield return new WaitForSeconds(1);

        }
        StartNextWave();

    }

}

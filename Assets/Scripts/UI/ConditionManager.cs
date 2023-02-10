using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The condition Manager is used to handle the end-screen, it showcases the win or lose and has the functionality for the buttons.
/// </summary>
public class ConditionManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI conditionTextDisplay;

    public void SetCondition(GameCondition condition)
    {
        this.gameObject.SetActive(true);
        if(condition == GameCondition.Won)conditionTextDisplay.text = "YOU WON";
        else conditionTextDisplay.text = "YOU LOST";
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Continue()
    {
        this.gameObject.SetActive(false);
        GameManager.Instance.Continue();
    }
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        //set health to default again.
        //make the game condition to playing
    }
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NextLevel : MonoBehaviour
{
    public static int level;

    // Use this for initialization
    public void onClick()
    {
        if(level == 6)
        {
            SceneManager.LoadScene("menu");
        }
        string levelStr = "level" + level;
        SceneManager.LoadScene(levelStr);
        level++;
        Debug.Log("Level" + level);
        TriggerBehaviors.deathCount = 0;
        TriggerBehaviors.time = 0;
    }
    public void onClickSelect(int levelSelect)
    {
        string levelStr = "level" + levelSelect;
        SceneManager.LoadScene(levelStr);
        level = levelSelect+1;
    }
}
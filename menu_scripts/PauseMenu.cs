using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public string levelSelect;
    public string mainMenu;
    public bool isPaused;
    public GameObject pauseMenuCanvas;

    // Update is called once per frame
    void Update () {
        GameObject cc = GameObject.Find("Main Camera");
        CustomCursor other = (CustomCursor)cc.GetComponent(typeof(CustomCursor));
        if (isPaused)
        {
            pauseMenuCanvas.SetActive(true);
            other.OnDisable();
            Time.timeScale = 0f;
        }
        else
        {
            pauseMenuCanvas.SetActive(false);
            other.SetCustomCursor();
            Time.timeScale = 1f;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }

    }
    public bool getState()
    {
        return isPaused;
    }

    public void Resume()
    {
        isPaused = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("menu");
        NextLevel.level = 0;
        isPaused = false;

    }

}

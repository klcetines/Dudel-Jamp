using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause_script : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void OnPauseButtonPress()
    {
        Debug.Log("Pause button pressed");
        if (gameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }
}
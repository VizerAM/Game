using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public LevelLoader Loader;
    public static bool paused;
    public float timeScaile = 1;
    public GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if(paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        timeScaile = Time.timeScale;
        Time.timeScale = 0;
        paused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = timeScaile;
        paused = false;

    }

    public void Exit()
    {

        Loader.LevelLoad(0);
        Time.timeScale = 1;
    }

    public void Reload()
    {

        Loader.LevelReload();
        Time.timeScale = 1;
    }


}

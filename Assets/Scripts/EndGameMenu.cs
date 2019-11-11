using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameMenu : MonoBehaviour
{
    public LevelLoader Loader;
    public GameObject EndGamePanel;
    public void EndGame()
    {
        EndGamePanel.SetActive(true);
        Time.timeScale = 0;

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

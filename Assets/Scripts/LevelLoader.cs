using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{

    public GameObject LoadScrean;
    public Slider slider;


    public void LevelLoad(int index)
    {
        
        StartCoroutine(LoadAsynchronosly(index));
        Time.timeScale = 1;
    }

    public void LevelReload()
    {
        StartCoroutine(LoadAsynchronosly(SceneManager.GetActiveScene().buildIndex));
        Time.timeScale = 1;
    }

    IEnumerator LoadAsynchronosly(int index)
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        LoadScrean.SetActive(true);

        while(!operation.isDone)
        {

            float progres = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progres;
            yield return null;
        }



    }

}

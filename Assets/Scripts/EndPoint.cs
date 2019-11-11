using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndPoint : MonoBehaviour
{
    public GameObject LevelComplitePanel;

    public Toggle Stars;
    public Toggle Hurt;
    public Toggle time;

    public bool IsPoint = true;

    float startTime;

    private void Start()
    {
        startTime = Time.time;
    }


    public void EnGame(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            float endTime = Time.time;

            Time.timeScale = 0;

            LevelComplitePanel.SetActive(true);

            Character character = collision.GetComponent<Character>();

            Stars.isOn = character.GetStars() >= 3;

            Hurt.isOn = character.noHurt;

            time.isOn = endTime - startTime < 60;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(IsPoint)
            EnGame(collision);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; //Работа со сценами

public class GameWinHalf2 : MonoBehaviour
{
    private float timer;
    private void Awake()
    {
        timer = 1;
        GetComponent<Canvas>().enabled = false;

    }
    void Update()
    {
        Time.timeScale = timer;
        if (GameObject.FindWithTag("Enemy") == false)
        {
            timer = 0;
            GetComponent<Canvas>().enabled = true;
        }

    }
    public void Resume()
    {
        SceneManager.LoadScene("ThirdLevel");

    }
    public void BackToMenu()
    {
        timer = 0;
        SceneManager.LoadScene("MainMenu"); // здесь при нажатии на кнопку загружается другая сцена, вы можете изменить название сцены на свое

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Работа со сценами

public class GameMenu : MonoBehaviour
{
    public float timer;
    public bool ispuse;
    public bool guipuse;
    public bool isOpened = false; //Открыто ли меню
    private void Awake()
    {

    }
    void Update()
    {
        Time.timeScale = timer;
        if (Input.GetKeyDown(KeyCode.Escape) && ispuse == false)
        {
            ispuse = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && ispuse == true)
        {
            ispuse = false;
        }
        if (ispuse == true)
        {
            timer = 0;
            guipuse = true;
            GetComponent<Canvas>().enabled = true;
        }
        else if (ispuse == false)
        {
            timer = 1f;
            guipuse = false;
            GetComponent<Canvas>().enabled = false;
        }
    }
    
    public void Resume()
    {
        ispuse = false;
        timer = 0;
    }
    public void BackToMenu()
    {
        ispuse = false;
        timer = 0;
        SceneManager.LoadScene("MainMenu"); // здесь при нажатии на кнопку загружается другая сцена, вы можете изменить название сцены на свое

    }
   
}

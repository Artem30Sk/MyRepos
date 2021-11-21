using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; //Работа со сценами

public class GameOverMenu : MonoBehaviour
{
    private float timer;
    private float hpp;
    private TankMove hp;
    public GameObject GameOverCanvas;
    private void Awake()
    {
        hp = GameOverCanvas.GetComponent<TankMove>();
        timer = 1;
        GetComponent<Canvas>().enabled = false;
        
    }
    void Update()
    {
        hpp = hp.health;
        Time.timeScale = timer;
        if (hpp<=0)
        {
            timer = 0;
            GetComponent<Canvas>().enabled = true;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("FirstLevel");
    }
    public void BackToMenu()
    {
        timer = 0;
        SceneManager.LoadScene("MainMenu"); // здесь при нажатии на кнопку загружается другая сцена, вы можете изменить название сцены на свое

    }
}

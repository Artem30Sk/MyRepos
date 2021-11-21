using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Работа с интерфейсами
using UnityEngine.SceneManagement; //Работа со сценами
using UnityEngine.Audio; //Работа с аудио
using System.IO; //Библиотек для работы с файлами
using System.Runtime.Serialization.Formatters.Binary; //Библиотека для работы бинарной сериализацией
using System.Threading;

public class Menu : MonoBehaviour
{
    
    /*public float volume = 0; //Громкость
    public int quality = 0; //Качество
    public bool isFullscreen = false; //Полноэкранный режим
    public Dropdown resolutionDropdown; //Список с разрешениями для игры
    public Toggle FullscreenToggle;
    public Dropdown qualityDropdown;
    private Resolution[] resolutions; //Список доступных разрешений
    private int currResolutionIndex = 0; //Текущее разрешение*/
    /*private void Awake()
    {
        resolutions = Screen.resolutions; //Получение доступных разрешений
        List<string> options = new List<string>(); //Создание списка со строковыми значениями

        for (int i = 0; i < resolutions.Length; i++) //Поочерёдная работа с каждым разрешением
        {
            string option = resolutions[i].width + " x " + resolutions[i].height; //Создание строки для списка
            options.Add(option); //Добавление строки в список

            if (resolutions[i].Equals(Screen.currentResolution)) //Если текущее разрешение равно проверяемому
            {
                currResolutionIndex = i; //То получается его индекс
            }
        }

        resolutionDropdown.AddOptions(options); //Добавление элементов в выпадающий список
        resolutionDropdown.value = currResolutionIndex; //Выделение пункта с текущим разрешением
        resolutionDropdown.RefreshShownValue(); //Обновление отображаемого значения
    }*/
    public void GoToMain()
    {
        SceneManager.LoadScene("FirstLevel"); //Переход на сцену с названием Menu
    }

    public void QuitGame()
    {
        Application.Quit(); //Закрытие игры. В редакторе, кончено, она закрыта не будет, поэтому для проверки можно использовать Debug.Log();
    }
    void Update()
    {
        
    }

    /*public void ResDrop(int index) //Изменение разрешения
    {
        
    }

    public void ChangeFullscreenMode() //Включение или отключение полноэкранного режима
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void ChangeQuality() //Изменение качества
    {
        
    }*/
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuBallMan : MonoBehaviour
{
    [Header("Window")]
    [SerializeField] private GameObject[] MenuVar;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private SpriteRenderer[] render;
    [SerializeField] private GameObject Watch;
    [Header("Setting")]
    [SerializeField] private Text[] ScreenText;
    [SerializeField] private int[] ScreenWidth;
    
    private UIBallManScript UI;
    private int[] ScreenHeigh = new int[] { 768, 720, 1024, 1080, 1080 };
    private Vector3 trans;

    private void Awake()
    {
        UI = FindObjectOfType<UIBallManScript>();
        trans = Watch.transform.localPosition;

        int width = PlayerPrefs.GetInt("Width");
        int heigh = PlayerPrefs.GetInt("Heigh");
        if (width == 0)
        {
            width = 1920;
        }
        if (heigh == 0)
        {
            heigh = 1080;
        }
        Screen.SetResolution(width, heigh, true);

        for(int i = 0; i < ScreenWidth.Length; i ++)
        {
            if(ScreenWidth[i] == width)
            {
                ScreenText[i].color = Color.cyan;
                ScreenText[i].raycastTarget = false;
                WatchTrans(i);
            }
        }

        render[0].enabled = true;
        render[1].enabled = false;

        this.gameObject.SetActive(false);
    }

    public void Continue()//Реализация продолжения игры и выход из меню
    {
        if(UI.Indicator() == false)
        {
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0.3f;
        }
        this.gameObject.SetActive(false);
    }

    public void Restart()//Реализация перезапуска уровня
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Setting()//Реализация перехода в меню настроек
    {
        render[0].sprite = sprites[1];
        MenuVar[0].SetActive(false);
        MenuVar[1].SetActive(true);
    }

    public void Back(int var)//Реализация выход в меню из меню настроек
    {
        if (var == 0)
        {
            render[0].enabled = true;
            render[1].enabled = false;
            render[0].sprite = sprites[0];

            for (int i = 0; i < ScreenText.Length; i++)
            {
                if (ScreenText[i].raycastTarget == false)
                {
                    ScreenText[i].color = Color.cyan;
                }
            }

            for (int i = 0; i < MenuVar.Length; i++)
            {
                if (i == 0)
                {
                    MenuVar[i].SetActive(true);
                }
                else
                {
                    MenuVar[i].SetActive(false);
                }
            }
        }
        else
        {
            MenuVar[3].SetActive(true);
            MenuVar[4].SetActive(false);
        }
    }

    public void ScreenNew(int var)//Реализация изменения разрешения экрана
    {
        for(int i = 0; i < ScreenWidth.Length; i++)
        {
            if(i == var)
            {
                ScreenText[i].raycastTarget = false;
                Screen.SetResolution(ScreenWidth[i], ScreenHeigh[i], true);
                PlayerPrefs.SetInt("Width", ScreenWidth[i]);
                PlayerPrefs.SetInt("Heigh", ScreenHeigh[i]);
            }
            else if(i !=var)
            {
                ScreenText[i].color = Color.white;
                ScreenText[i].raycastTarget = true;
            }
        }

        WatchTrans(var);
    }

    private void WatchTrans(int var)//Реализация перемещения часов
    {
        switch (var)
        {
            case 0: Watch.transform.localPosition = new Vector3(trans.x + 1, trans.y, trans.z); break;
            case 1: Watch.transform.localPosition = trans; break;
            case 2: Watch.transform.localPosition = new Vector3(trans.x + 0.5f, trans.y, trans.z); break;
            case 3: Watch.transform.localPosition = trans; break;
            case 4: Watch.transform.localPosition = trans; break;
        }
    }

    public void QuitMenu(int var)//Реализация перехода в меню выхода
    {
        if (var == 0)
        {
            render[0].enabled = false;
            render[1].enabled = true;
            MenuVar[0].SetActive(false);
            MenuVar[2].SetActive(true);
        }
        else
        {
            MenuVar[4].SetActive(true);
            MenuVar[3].SetActive(false);
        }
    }

    public void Quit()//Реализация выхода
    {
        Application.Quit();
    }

    public void BackToMenu()//Реализация перехода в главное меню
    {
        SceneManager.LoadScene(0);
    }

    private void OnEnable()
    {
        Back(0);
    }
}

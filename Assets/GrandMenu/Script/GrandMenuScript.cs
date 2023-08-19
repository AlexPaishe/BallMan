using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrandMenuScript : MonoBehaviour
{
    [Header("Screen")]
    [SerializeField] private Text[] ScreenText;
    [SerializeField] private Animator[] anima;
    [Header("Rotate")]
    [SerializeField] private float speed;
    [SerializeField] private float Timer;
    [Header("Are You Ready")]
    [SerializeField] private Text AYR;
    [SerializeField] private float AYRspeed = 0.05f;

    private int[] ScreenWidth = new int[] { 1024, 1280, 1600, 1920, 2000};
    private int[] ScreenHeigh = new int[] { 768, 720, 1024, 1080, 1080 };
    private string[] ayr = new string[] {"A","AR","ARE","ARE ", "ARE Y","ARE YO",
        "ARE YOU","ARE YOU ","ARE YOU R","ARE YOU RE","ARE YOU REA","ARE YOU READ",
        "ARE YOU READY","ARE YOU READY?" };

    private float CurrentStep = 225;
    private float NextStep = 225;
    private float currentTimer;
    private bool go = false;
    private bool ayrGo = false;

    private float ct = 0.1f;
    private int numb = 0;

    private void Awake()
    {
        currentTimer = Timer;
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

        for (int i = 0; i < ScreenWidth.Length; i++)
        {
            if (ScreenWidth[i] == width)
            {
                ScreenText[i].color = Color.cyan;
                ScreenText[i].raycastTarget = false;
                anima[i].SetBool("Up", false);
            }
        }

        ct = AYRspeed;
        Time.timeScale = 1f;
    }

    private void Update()
    {
        RotatePad();
        AYRText();
    }

    public void ButtonScreen(int var)//Реализация нажимание на кнопку выбора разрешения экрана и смена разрешения экрана
    {
        for (int i = 0; i < ScreenWidth.Length; i++)
        {
            if (i == var)
            {
                ScreenText[i].raycastTarget = false;
                ScreenText[i].color = Color.cyan;
                Screen.SetResolution(ScreenWidth[i], ScreenHeigh[i], true);
                PlayerPrefs.SetInt("Width", ScreenWidth[i]);
                PlayerPrefs.SetInt("Heigh", ScreenHeigh[i]);
                anima[i].SetBool("Up", false);
            }
            else if (i != var)
            {
                ScreenText[i].color = Color.white;
                ScreenText[i].raycastTarget = true;
                anima[i].SetBool("Up", true);
            }
        }
    }

    public void ButtonClick(int var)//Реализация нажатия на кнопку
    {
        anima[var].SetTrigger("Up");
    }

    private void RotatePad()//Реализация поворота планшета
    {       
        if(go == true)
        {
            currentTimer -= Time.deltaTime;
        }
        if (currentTimer < 0)
        {
            if (CurrentStep == NextStep && go == true)
            {
                CurrentStep = NextStep;
                go = false;
                currentTimer = Timer;
            }
            else if (CurrentStep < NextStep)
            {
                CurrentStep += speed;
                transform.eulerAngles = new Vector3(0, CurrentStep, 0);
            }
            else if (CurrentStep > NextStep)
            {
                CurrentStep -= speed;
                transform.eulerAngles = new Vector3(0, CurrentStep, 0);
            }
        }
    }

    public void Back()//Реализация поворота на начальный экран
    {
        NextStep += 180;
        go = true;
    }

    public void Setting()//Реализация поворота на меню настроек
    {
        NextStep -= 180;
        go = true;
    }

    public void QuitMenu(bool var)//Реализация выхода в меню выхода
    {
        if (var == true)
        {
            anima[7].SetTrigger("Down");
            ayrGo = true;
        }
        else
        {
            anima[7].SetTrigger("Up");
            ayrGo = false;
        }
    }

    public void PlayMenu(bool var)//Реализация выхода в меню игры
    {

        if (var == true)
        {
            anima[7].SetTrigger("On");
        }
        else
        {
            anima[7].SetTrigger("Off");
        }
    }

    public void Quit()//Реализация выхода из игры
    {
        Application.Quit();
    }

    private void AYRText()//Реализация анимации текста AYR
    {
        if (ayrGo == true)
        {
            ct -= Time.deltaTime;
            if (ct < 0 && numb < ayr.Length)
            {
                ct = AYRspeed;
                AYR.text = ayr[numb];
                numb++;
            }
        }
        else
        {
            ct -= Time.deltaTime;
            if (ct < 0 && numb > 0)
            {
                numb--;
                ct = AYRspeed;
                AYR.text = ayr[numb];
            }
            else if(ct < 0 && numb == 0)
            {
                ct = AYRspeed;
                AYR.text = "";
            }
        }
    }
}

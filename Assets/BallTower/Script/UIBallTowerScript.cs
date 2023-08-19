using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBallTowerScript : MonoBehaviour
{
    [SerializeField] private GameObject[] DropPref;
    [SerializeField] private List<GameObject> Health;
    [SerializeField] private List<GameObject> Mana;
    [SerializeField] private GameObject[] Menu;
    [SerializeField] private SpriteRenderer YDMenu;
    [SerializeField] private float Step;
    [SerializeField] private float speedRotate;

    private float _cooldown = 0.005f;
    private float NewStep = 0;
    private float CurrentStep = 0;
    private float step;
    private int pointOne = 0;
    private int pointTwo = 0;
    private int MaxOne = 150;
    private int MaxTwo = 190;
    private int timer = 0;
    private bool go = true;
    private bool newCircle = false;
    private bool change = false;
    private bool frozen = false;
    private bool checkpoint = false;

    private bool isLava = false;
    private bool isSave = false;
    private bool isVent = false;

    private Vector3 Target;
    private Vector3 newDrop;

    private LavaScript lava;
    private AudioBallManScript audio;

    private void Awake()
    {
        step = Step;
        Target = transform.position;
        newDrop = Target;
        newDrop.y += 0.5f;
        Target.y += 1.5f;
        if (newCircle == true)
        {
            NewStep = 180;
        }
        else
        {
            NewStep = 0;
        }

        lava = FindObjectOfType<LavaScript>();
        audio = FindObjectOfType<AudioBallManScript>();
        Time.timeScale = 1;
    }

    void Update()
    {
        if(go == true)
        {
            SpawnDrop();
            OpenMenu();
        }

        if (checkpoint == true)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                change = !change;
                frozen = true;
                if (timer != 0)
                {
                    Physics2D.gravity *= -1;
                }
            }

            LavaChange();
        }

        if(newCircle == true)
        {
            RotateUI();
        }
    }

    public void YouDiedMenu()//Включение меню смерти
    {
        go = false;
        Menu[1].SetActive(true);
        Menu[2].SetActive(true);
        Menu[3].SetActive(true);
        YDMenu.enabled = true;
        isSave = audio.PauseSound(2);
        isLava = audio.PauseSound(4);
        isVent = audio.PauseSound(6);
        Time.timeScale = 0;
    }

    private void SpawnDrop()//Реализация заполнения шкалы хп и маны
    {
        if (pointTwo < MaxTwo)
        {
            _cooldown -= Time.deltaTime;

            while (_cooldown < 0)
            {
                _cooldown = 0.005f;
                if (pointOne < MaxOne)
                {
                    GameObject HP = Instantiate(DropPref[0], transform.position, Quaternion.identity);
                    Health.Add(HP);
                }
                float rand = Random.Range(-0.05f, 0.05f);
                Vector3 spawn = Target;
                spawn.x += rand;
                GameObject M = Instantiate(DropPref[1], spawn, Quaternion.identity);
                Mana.Add(M);
                pointOne++;
                pointTwo++;
            }
        }
    }

    private void OpenMenu()//Реализация включения меню
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Menu[0].SetActive(!Menu[0].activeSelf);
            if (Menu[0].activeSelf == false)
            {
                Time.timeScale = 1;
            }
            else
            {
                isLava = audio.PauseSound(4);
                isSave = audio.PauseSound(2);
                isVent = audio.PauseSound(6);
            }
            if(isLava == true && Menu[0].activeSelf == false)
            {
                isLava = false;
                audio.Sound(4);
            }
            if(isSave == true && Menu[0].activeSelf == false)
            {
                isSave = false;
                audio.Sound(2);
            }
            if (isVent == true && Menu[0].activeSelf == false)
            {
                isVent = false;
                audio.Sound(6);
            }
        }
    }

    private void RotateUI()//Реализация поворота UI
    {
        if (CurrentStep == NewStep)
        {
            CurrentStep = NewStep;
            checkpoint = true;
        }
        else if (CurrentStep < NewStep)
        {
            CurrentStep += speedRotate;
            transform.eulerAngles = new Vector3(0, 0, CurrentStep);
        }
        else if (CurrentStep > NewStep)
        {
            CurrentStep -= speedRotate;
            transform.eulerAngles = new Vector3(0, 0, CurrentStep);
        }
    }

    public void RechargeUI()//Реализация перезарядки маны
    {
        newCircle = !newCircle;
        if (newCircle == true)
        {
            NewStep += 180;
            Physics2D.gravity = new Vector2(0, -9.81f);
        }
    }

    public void Damage(int Maxlive)//Реализация получения урона
    {
        int OneHp = MaxOne / Maxlive;
        int damage = 0;
        for (int i = 0; i < MaxOne; i++)
        {
            if (Health[i].activeSelf == true && damage < OneHp)
            {
                damage++;
                Health[i].SetActive(false);
            }
            if (damage == OneHp)
            {
                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Booster"))
        {
            timer++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Booster"))
        {
            timer--;
        }
    }

    private void LavaChange()//Реализация изменения лавы
    {
        if (change == true)
        {
            step -= Time.deltaTime;
            if(step<=0 && timer > 0)
            {
                int useDrop = MaxTwo - timer;
                float rand = Random.Range(-0.05f, 0.05f);
                Vector3 spawn = newDrop;
                spawn.x += rand;
                Mana[useDrop].transform.position = spawn;
                Mana[useDrop].GetComponent<SpriteRenderer>().color = Color.yellow;
                step = Step;
            }
            if (timer == 0)
            {
                lava.HotLava();
                Physics2D.gravity = new Vector2(0, -9.81f);
                change = false;
            }
            else if(timer>0 && frozen == true)
            {
                frozen = false;
                lava.FrozenLava();
            }
        }
        else
        {
            if(frozen == true)
            {
                frozen = false;
                lava.HotLava();
            }
        }

    }
}

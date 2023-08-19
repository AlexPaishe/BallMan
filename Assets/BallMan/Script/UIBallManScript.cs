using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class UIBallManScript : MonoBehaviour
{
    [SerializeField] private GameObject[] DropPref;
    [SerializeField] private List<GameObject> Health;
    [SerializeField] private Transform spawnpoint;
    [SerializeField] private Color[] BonusCol;
    [SerializeField] private GameObject[] Menu;
    [SerializeField] private SpriteRenderer YDMenu;

    private float _cooldown = 0;
    private int pointOne;
    private int pointTwo;
    private int MaxOne = 100;
    private int timer = 0;
    private bool go = true;
    private bool indicator= false;

    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (go == true)
        {
            SpawnDrop();

            TimeKiller();
        }

        if(timer == 0)
        {
            Time.timeScale = 1f;
            Physics2D.gravity = new Vector2(0, -0.4f);
            indicator = false;
        }
    }

    public void Damage()//Реализация получения урона на UI
    {
        int damage = 0;
        for(int i = 0; i < MaxOne; i ++)
        {
            if(Health[i].activeSelf == true && damage < 20)
            {
                damage++;
                Health[i].SetActive(false);
            }
            if(damage == 20)
            {
                break;
            }
        }
    }

    private void SpawnDrop()//Реализация заполнения шкалы хп и маны
    {
        if (pointTwo < 150)
        {
            _cooldown -= Time.deltaTime;

            while (_cooldown < 0)
            {
                _cooldown += 0.02f;
                if (pointOne < MaxOne)
                {
                    GameObject HP = Instantiate(DropPref[0], spawnpoint.position, Quaternion.identity);
                    Health.Add(HP);
                }
                float rand = Random.Range(-0.05f, 0.05f);
                Vector3 spawn = spawnpoint.position;
                spawn.x = spawnpoint.position.x + rand;
                Instantiate(DropPref[1], spawn, Quaternion.identity);
                pointOne++;
                pointTwo++;
            }
        }
    }

    private void TimeKiller()//Реализация замедления времени
    {
        if (Menu[0].activeSelf == false)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && indicator == true)
            {
                Physics2D.gravity = new Vector2(0, -0.4f);
                Time.timeScale = 1f;
                indicator = false;
            }
            else if(Input.GetKeyDown(KeyCode.UpArrow) && indicator == false)
            {
                Physics2D.gravity = new Vector2(0, 5f);
                Time.timeScale = 0.3f;
                indicator = true;
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (Menu[0].activeSelf == false)
            {
                Time.timeScale = 0f;
            }
            else
            {
                if(indicator == false)
                {
                    Time.timeScale = 1;
                }
                else
                {
                    Time.timeScale = 0.3f;
                }
            }
            Menu[0].SetActive(!Menu[0].activeSelf);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Booster"))
        {
            timer++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Booster"))
        {
            timer--;
        }
    }

    public void GoBack()//Запрет на использование замедления времени
    {
        go = false;
    }

    public void BonusColor(int arm)//Реализация индикатора бонуса
    {
        if (arm > -1)
        {
            sprite.color = BonusCol[arm];
        }
        else
        {
            sprite.color = BonusCol[4];
        }
    }

    public bool Indicator()//Возвращает индикатор
    {
        return indicator;
    }

    public void YouDiedMenu()//Включение меню смерти
    {
        Menu[1].SetActive(true);
        Menu[2].SetActive(false);
        YDMenu.enabled = true;
        Time.timeScale = 0;
    }
}

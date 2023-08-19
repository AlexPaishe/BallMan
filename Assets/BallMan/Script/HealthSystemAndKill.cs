using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BallManMovement))]
public class HealthSystemAndKill : MonoBehaviour
{ 
    private int Armor = -1;
    private ManagerSCript manager;
    private BonusScript bonus;
    private BallManMovement ball;
    private Animator anima;
    private UIBallManScript UI;
    private AudioBallManScript audio;
    private int live = 5;

    private void Awake()
    {
        manager = FindObjectOfType<ManagerSCript>();
        bonus = FindObjectOfType<BonusScript>();
        anima = GetComponent<Animator>();
        ball = GetComponent<BallManMovement>();
        UI = FindObjectOfType<UIBallManScript>();
        audio = FindObjectOfType<AudioBallManScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bonus"))//Реализация взаимодействия с бонусом
        {
            Armor = bonus.Variation();
            bonus.TargetUp();
            UI.BonusColor(Armor);
            audio.Sound(3);
        }

        if(other.CompareTag("Boss"))
        {
            GhostMovement ghost = other.GetComponent<GhostMovement>();
            if(Armor == ghost.Variation())
            {
                manager.NextLevel();
                ghost.EndEnemy();
                Armor = -1;
                UI.BonusColor(Armor);
                if (manager.Level() < 4)
                {
                    bonus.Recalculation();
                }
                audio.Sound(2);
            }
            else
            {
                if (live != 0)
                {
                    live--;
                    UI.Damage();
                    audio.Sound(1);
                }
            }
            if(live == 0)
            {
                UI.GoBack();
                ball.Go();
                anima.SetTrigger("Death");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        audio.Sound(0);
    }

    public void EndGame()//Реализация окочания игры
    {
        UI.YouDiedMenu();
    }

    public int Live()//Возвращает здоровье
    {
        return live;
    }
}

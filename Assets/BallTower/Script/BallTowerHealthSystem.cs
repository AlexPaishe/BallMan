using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(SphereCollider)), RequireComponent(typeof(Rigidbody)),
    RequireComponent(typeof(BallTowerMovement)), RequireComponent(typeof(MeshRenderer))]
public class BallTowerHealthSystem : MonoBehaviour
{
    [SerializeField] private int maxLive;
    [SerializeField] private float speed;

    private Rigidbody rb;
    private SphereCollider col;
    private BallTowerMovement player;
    private MeshRenderer mesh;
    private Animator anima;
    private UIBallTowerScript UI;
    private AudioBallManScript audio;
    private Vector3 Target;
    private bool go = false;
    private int live = 0;

    private void Awake()
    {
        Target = transform.position;
        col = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
        player = GetComponent<BallTowerMovement>();
        mesh = GetComponent<MeshRenderer>();
        UI = FindObjectOfType<UIBallTowerScript>();
        audio = FindObjectOfType<AudioBallManScript>();
        live = maxLive;
    }

    private void Update()
    {
        PortalBallMan();
    }

    public void Damage()//Реализация получения урона и начала телепортации
    {
        live--;
        go = true;
        rb.isKinematic = true;
        col.isTrigger = true;
        player.enabled = false;
        mesh.material.EnableKeyword("_EMISSION");
        anima.SetBool("Teleport", true);
        UI.Damage(maxLive);
        this.gameObject.tag = "Boy";
        audio.Sound(2);
        if(live == 0)
        {
            UI.YouDiedMenu();
        }
    }

    public void CheckPoint(Animator animator, Vector3 target)//Реализация сохранения
    {
        anima = animator;
        Target = target;
        audio.Sound(3);
    }

    private void PortalBallMan()//Телепортация игрока при получении урона
    {
        if (go == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target, speed * Time.deltaTime);
            if (transform.position == Target)
            {
                go = false;
                rb.isKinematic = false;
                col.isTrigger = false;
                player.enabled = true;
                mesh.material.DisableKeyword("_EMISSION");
                anima.SetBool("Teleport", false);
                audio.StopSound();
                this.gameObject.tag = "Player";
            }
        }
    }
}

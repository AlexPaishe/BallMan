using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider)), RequireComponent(typeof(Animator))]
public class GhostMovement : MonoBehaviour
{
    [Header("Variation")]
    [SerializeField] private Material[] GhostMat;
    [Header("Move")]
    [SerializeField] private float MaxDistance;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask Targetlayer;
    [SerializeField] private LayerMask IgnoreLayer;

    private Vector3 Target;
    private MeshRenderer mesh;
    private Animator anima;
    private bool go = false;
    private int variation;

    private void Start()
    {
        mesh = GetComponentInChildren<MeshRenderer>();
        anima = GetComponent<Animator>();
        for (int i = 0; i < GhostMat.Length; i++)
        {
            if (i == variation)
            {
                mesh.material = GhostMat[i];
            }
        }
        SearchTarget();
    }

    private void Update()
    {
        if (go == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target, speed * Time.deltaTime);
            if (transform.position == Target)
            {

                SearchTarget();
            }
        }
    }

    private void SearchTarget()//Реализация передвижения врага по платформе
    {
        Transform[] points = new Transform[4];
        int count = -1;

        Ray[] raycast = new Ray[points.Length];
        raycast[0] = new Ray(transform.position, transform.forward);
        raycast[1] = new Ray(transform.position, -transform.forward);
        raycast[2] = new Ray(transform.position, transform.right);
        raycast[3] = new Ray(transform.position, -transform.right);
        RaycastHit[] hits = new RaycastHit[points.Length];
        for (int i = 0; i < raycast.Length; i++)
        {
            if (Physics.Raycast(raycast[i], out hits[i], MaxDistance, Targetlayer))
            {
                if (Physics.Raycast(raycast[i], 3f, IgnoreLayer) == false)
                {
                    count++;
                    points[count] = hits[i].transform;
                }
            }
        }

        int rand = Random.Range(0, count + 1);
        Target = points[rand].position;
    }

    public void StartEnemy()//Реализация появления призраков
    {
        go = true;
    }

    public void EndEnemy()//Рeализация смерти призрака
    {
        go = false;
        this.gameObject.tag = "Boy";
        anima.SetTrigger("Death");
    }

    public void Death()//Выключение обьекта
    {
        this.gameObject.SetActive(false);
    }

    public int Variation()//Возвращает номер призрака
    {
        return variation;
    }

    public void Variation(int var)
    {
        variation = var;
    }

    public void SpeedChost()//Увеличение скорости призракам
    {
        speed += 1f;
    }
}

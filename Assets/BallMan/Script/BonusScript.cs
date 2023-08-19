using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusScript : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform[] point;
    [SerializeField] private float height;
    [SerializeField] private float speed;
    [Header("Variation")]
    [SerializeField] private Material[] Mats;
    [SerializeField] private Color[] col;

    private int[] arr2 = new int[] { 0, 1, 2, 3 };
    private int level = 4;
    private Vector3 Target;
    private int variation;
    private int lengthPoint;
    private int lengthMats;
    private bool go = false;

    private Light lanter;
    private MeshRenderer mesh;

    private void Awake()
    {
        lengthPoint = point.Length;
        lengthMats = Mats.Length;
        lanter = GetComponent<Light>();
        mesh = GetComponent<MeshRenderer>();
        BonusSearch();
        Recalculation();
    }

    void Update()
    {
        if (go == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target, Time.deltaTime * speed);
            if (transform.position == Target)
            {
                go = false;
            }
        }
    }

    private void BonusSearch()//Выбор вариации бонусы
    {
        int[] arr1 = new int[lengthMats];
        for (int i = 0; i < lengthMats; i++)
            arr1[i] = Random.Range(0, lengthMats - 1);
        int buf1, buf2;
        for (int i = 0; i < lengthMats - 1; i++)
        {
            for (int j = i + 1; j < lengthMats; j++)
            {
                if (arr1[i] > arr1[j])
                {
                    buf1 = arr1[i];
                    buf2 = arr2[i];
                    arr1[i] = arr1[j];
                    arr2[i] = arr2[j];
                    arr1[j] = buf1;
                    arr2[j] = buf2;
                }
            }
        }
    }

    private void SwitcherTarget(int rand)//Реализация выбора места падения
    {
        for (int i = 0; i < lengthPoint; i++)
        {
            if (i == rand)
            {
                transform.position = point[i].position + (height * Vector3.up);
                Target = point[i].position + (Vector3.up * 0.5f);
            }
        }
    }

    private void SwitchingVariation()//Реализация изменения вариации бонуса 
    {
        int i = arr2[level];
        variation = i;
        mesh.material = Mats[i];
        lanter.color = col[i];
    }

    public void Recalculation()//Реализация совокупности операций по появлению бонуса
    {
       go = true;
       int rand = Random.Range(0, point.Length);
       SwitcherTarget(rand);
       level--;
       SwitchingVariation();
    }

    public int Variation()//Возвращает вариацию бонуса
    {
        return variation;
    }

    public void TargetUp()//Отправление бонуса обратно
    {
        Target = transform.position + Vector3.up * 40;
        go = true;
    }
}

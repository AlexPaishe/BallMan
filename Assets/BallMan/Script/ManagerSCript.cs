using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSCript : MonoBehaviour
{
    [SerializeField] private List<GhostMovement> ghostNow;
    [SerializeField] private GhostMovement ghost;
    [SerializeField] private Transform[] point;
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private PortalMovement portal;

    private int length;
    private int[] arr2 = new int[4] { 0, 1, 2, 3 };
    private int number = 4;
    private int level = 0;

    private void Awake()
    {
        length = point.Length;
        EnemySearch();
        for (int i = 0; i < point.Length; i ++)
        {
            GhostMovement enemy = Instantiate(ghost);
            number--;
            enemy.Variation(arr2[number]);
            enemy.transform.position = point[i].position;
            ghostNow.Add(enemy);
        }

        for(int i = 1; i < obstacles.Length; i ++)
        {
            obstacles[i].SetActive(false);
        }
    }

    private void EnemySearch()//Выбор вариации врагов
    {
        int[] arr1 = new int[length];
        for (int i = 0; i < length; i++)
            arr1[i] = Random.Range(0, length);
        int buf1, buf2;
        for (int i = 0; i < length - 1; i++)
        {
            for (int j = i + 1; j < length; j++)
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

    public void NextLevel()//Усложнение игры
    {
        level++;
        for(int i = 0; i < ghostNow.Count; i ++)
        {
            ghostNow[i].SpeedChost();
        }
        obstacles[level].SetActive(true);
        if(level == 4)
        {
            portal.GoPortal();
        }
    }

    public int Level()//Возвращает значение уровня
    {
        return level;
    }
}

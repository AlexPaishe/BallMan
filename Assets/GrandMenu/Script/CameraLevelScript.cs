using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraLevelScript : MonoBehaviour
{
    private Animator anima;

    private void Awake()
    {
        anima = GetComponent<Animator>();
    }

    public void NewTarget(int var)//Реализация приближения и отдаления камеры
    {
        if (var == 0)
        {
            anima.SetTrigger("Up");
        }
        else
        {
            anima.SetTrigger("Down");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotateScript : MonoBehaviour
{
    [SerializeField] private float speed;

    private float NextStep = 0;
    private float CurrentStep = 0;
    private bool go = false;

    void Update()
    {
        InputRotate();
    }

    private void FixedUpdate()
    {
        RotateCam();
    }

    private void InputRotate()//Реализация поворота камеры по клавишам
    {
        if(Input.GetKeyDown(KeyCode.RightArrow) && go == false)
        {
            NextStep -=90;
            go = true;
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow) && go == false)
        {
            NextStep +=90;
            go = true;
        }
    }

    private void RotateCam()//Реализация поворота камеры
    {
        if (CurrentStep == NextStep && go == true)
        {
            CurrentStep = NextStep;
            go = false;
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

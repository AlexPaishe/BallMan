using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody)),RequireComponent(typeof(MeshRenderer))]
public class BallManMovement : MonoBehaviour
{
    [SerializeField] private float speed = 500;
    private Rigidbody rb;
    private Camera MainCamera;
    private bool go = true;
    private MeshRenderer mesh;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        MainCamera = Camera.main;
        mesh = GetComponent<MeshRenderer>();
    }

    void FixedUpdate()
    {
        if (go == true)
        {
            Movement();
        }
    }

    private void Movement()//Реализация движения шарика по вектору камеры
    {
        if (Input.GetKey(KeyCode.W))//Движение вперед по вектору камеры
        {
            rb.AddForce(MainCamera.transform.forward * speed * Time.deltaTime);///Движение вперед при нажатии клавиши W
        }
        if (Input.GetKey(KeyCode.S))//Движение назад по вектору камеры
        {
            rb.AddForce(-MainCamera.transform.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D)) //Движение вправо по вектору камеры
        {
            rb.AddForce(MainCamera.transform.right * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A)) //Движение влево по вектору камеры
        {
            rb.AddForce(-MainCamera.transform.right * speed * Time.deltaTime);
        }
    }

    public void Go()//Реализация прекращения движения
    {
        go = false;
        rb.isKinematic = true;
        mesh.material.EnableKeyword("_EMISSION");
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTowerMovement : MonoBehaviour
{
    [SerializeField] private float force = 30;
    [SerializeField] private float jumpForce = 300;

    private Rigidbody rb;
    private AudioBallManScript audio;
    private bool go = true;
    private int jump = 0;
    private float currentForce;
    private float jumpTime = 1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audio = FindObjectOfType<AudioBallManScript>();
        currentForce = force;
    }

    private void FixedUpdate()
    {
        if(go == true)
        {
            Movement();
            Jump();
        }
    }

    private void Movement()//Реализация передвижения игрока
    {
        float X = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        rb.AddForce(Vector3.forward * y * currentForce);
        rb.AddForce(Vector3.right * X * currentForce);
        //rb.velocity = new Vector3(1 * X * currentForce, rb.velocity.y, 1 * y * currentForce);
    }

    private void Jump()//Реализация прыжка персонажа
    {
        jumpTime += Time.fixedDeltaTime;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpTime >= 0.2f)
            {
                if (jump == 0)
                {
                    rb.velocity = new Vector3(0, rb.velocity.y, 0);
                    currentForce = force / 4;
                }
                jumpTime = 0;
                if (jump < 2)
                {
                    jump++;
                    rb.AddForce(Vector3.up * jumpForce);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Floor"))
        {
            jump = 0;
            currentForce = force;
            jumpTime = 1;
            audio.Sound(1);
        }
        else if(collision.collider.CompareTag("Invasion"))
        {
            audio.Sound(1);
        }
    }

    public void NewJump()//востанавление прыжков
    {
        jump = 0;
        jumpTime = 1;
    }
}

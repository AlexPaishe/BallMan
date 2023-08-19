using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentilatorScript : MonoBehaviour
{
    [SerializeField] private float force;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<BallTowerMovement>().NewJump();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * force * Time.deltaTime);
        }
    }
}

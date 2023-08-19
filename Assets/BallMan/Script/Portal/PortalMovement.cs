using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 Begin;
    [SerializeField] private Vector3 Target;

    private bool go = false;

    // Update is called once per frame
    void Update()
    {
        if(go == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target, speed * Time.deltaTime);
        }
    }

    public void GoPortal()
    {
        transform.position = Begin;
        go = true;
    }
}

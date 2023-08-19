using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject portal;
    [SerializeField] private float speed;
    [SerializeField] private Vector3 Begin;
    [SerializeField] private Vector3 Target;

    private bool go = false;
    private Animator anima;
    private FallenCameraScript cam;
    private LavaScript lava;

    private void Awake()
    {
        anima = GetComponent<Animator>();
        cam = FindObjectOfType<FallenCameraScript>();
        lava = FindObjectOfType<LavaScript>();
    }

    void Update()
    {
        if(go == true)
        {
            portal.transform.position = Vector3.MoveTowards(portal.transform.position, Target, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            anima.SetTrigger("Down");
        }
    }

    public void StartPortal()//Реализация появления портала
    {
        portal.transform.position = Begin;
        go = true;
        cam.enabled = false;
        lava.FrozenLava();
    }
}

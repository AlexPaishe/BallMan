using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBlockScript : MonoBehaviour
{
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private GameObject nextPlatform;
    [SerializeField] private float timer;

    private Animator anima;

    private bool go = false;
    private float col = 1;
    private float step;

    private void Awake()
    {
        anima = GetComponent<Animator>();
        step = col / (timer * 60);
        this.gameObject.SetActive(false);
    }

    void Update()
    {     
        if(go == true)
        {
            col -= step;
            mesh.material.color = new Color(col, 0, col);
            if(col <= 0 )
            {

                anima.SetBool("Start", false);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            go = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            nextPlatform.SetActive(true);
        }
    }

    private void OnEnable()
    {
        col = 1;
        go = false;
        mesh.material.color = new Color(col, 0, col);
        anima.SetBool("Start", true);
    }

    public void DestroyPlatform()//Уничтожение обьекта
    {
        this.gameObject.SetActive(false);
    }
}

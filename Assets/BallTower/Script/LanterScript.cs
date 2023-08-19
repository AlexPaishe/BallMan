using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanterScript : MonoBehaviour
{
    [SerializeField] private Material mat;
    [SerializeField] private Material[] neon;
    [SerializeField] private GameObject Destroy;

    private Animator anima;
    private float offset = 1;
    private bool go = true;
    private float timer = 0;

    private void Awake()
    {
        anima = GetComponent<Animator>();
        mat.DisableKeyword("_EMISSION");
    }

    private void Update()
    {
        timer += Time.deltaTime;
        offset += Time.deltaTime * 0.1f;
        for(int i = 0; i < neon.Length; i++)
        {
            if (i < 2)
            {
                neon[i].mainTextureOffset = new Vector2(offset, 0);
            }
            else
            {
                neon[i].mainTextureOffset = new Vector2(0, offset);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            if (timer >= 0.5f)
            {
                timer = 0;
                go = !go;
                anima.SetBool("Up", go);
                if (go == true)
                {
                    mat.DisableKeyword("_EMISSION");
                    Destroy.GetComponent<Animator>().SetBool("Start", false);
                }
                else
                {
                    mat.EnableKeyword("_EMISSION");
                    Destroy.SetActive(true);
                }
            }
        }
    }
}

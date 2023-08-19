using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveMachineScript : MonoBehaviour
{
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] float lavaStep;

    private Animator anima;
    private LavaScript lava;
    private UIBallTowerScript UI;
    private AudioBallManScript audio;
    private bool check = false;
    private Vector3 Target;

    private void Awake()
    {
        anima = GetComponent<Animator>();
        lava = FindObjectOfType<LavaScript>();
        UI = FindObjectOfType<UIBallTowerScript>();
        audio = FindObjectOfType<AudioBallManScript>();
        Target = transform.position;
        Target.y += 5.75f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(check == false)
        {
            if(collision.collider.CompareTag("Player"))
            {
                check = true;
                anima.SetTrigger("Check");
                collision.collider.GetComponent<BallTowerHealthSystem>().CheckPoint(anima, Target);
                lava.NewSpeed(lavaStep);
                UI.RechargeUI();
                audio.LavaSounf(true);
            }
        }
    }

    public void OnLight()//Реализация включения подсветки
    {
        mesh.material.EnableKeyword("_EMISSION");
    }
}

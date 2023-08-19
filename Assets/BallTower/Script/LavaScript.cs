using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LavaScript : MonoBehaviour
{
    [SerializeField] private float speedOffset;

    private MeshRenderer mesh;
    private BoxCollider box;
    private AudioBallManScript audio;
    private Vector3 Target;
    private bool go = true;
    private float offset = 0;
    private float speed;

    private void Awake()
    {
        Target = transform.position;
        Target.y += 70;
        mesh = GetComponent<MeshRenderer>();
        box = GetComponent<BoxCollider>();
        audio = FindObjectOfType<AudioBallManScript>();
    }

    void FixedUpdate()
    {
        if(go == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target, speed * Time.fixedDeltaTime);
            offset -= speedOffset;
            mesh.material.mainTextureOffset = new Vector2(0, offset);
        }
    }

    public void NewSpeed(float var)//Реализация увеличения скорости подъема лавы
    {
        speed += var;
    }

    public void FrozenLava()//Замарозка лавы
    {
        mesh.material.color = new Color(0, 0.25f, 1);
        mesh.material.DisableKeyword("_EMISSION");
        go = false;
        audio.LavaSounf(go);
        box.isTrigger = false;
    }

    public void HotLava()//Разагрев лавы
    {
        mesh.material.color = new Color(1, 1, 1);
        mesh.material.EnableKeyword("_EMISSION");
        go = true;
        audio.LavaSounf(go);
        box.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<BallTowerHealthSystem>().Damage();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenCameraScript : MonoBehaviour
{
    [SerializeField] private Transform Player;
    private Vector3 offset;

    private void Awake()
    {
        offset = transform.position - Player.position;
    }

    void Update()
    {
        Vector3 target = Player.position + offset;
        target.z = transform.position.z;
        target.x = Player.position.x * 0.5f;
        transform.position = target;
    }
}

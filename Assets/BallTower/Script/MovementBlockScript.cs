using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class MovementBlockScript : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] lantersBlock;
    [SerializeField] private Vector3[] points;
    [SerializeField] private float speed;

    private Vector3 Target;
    private Vector3 currentPos;

    private void Awake()
    {
        Target = points[0];
        currentPos = transform.position;
        LanterSearch(currentPos, Target);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Target, speed * Time.deltaTime);
        if(transform.position == Target)
        {
            currentPos = transform.position;
            if(Target == points[0])
            {
                Target = points[1];
            }
            else
            {
                Target = points[0];
            }
            LanterSearch(currentPos, Target);
        }
    }

    private void LanterSearch(Vector3 current, Vector3 Target)//Реализация изменения свечения фонарей при изменении движения
    {
        for(int i = 0; i < lantersBlock.Length;i++)
        {
            if (current.z < Target.z)
            {
                if (i == 0 || i == 1 || i == 4 || i == 5)
                {
                    lantersBlock[i].material.EnableKeyword("_EMISSION");
                }
                else
                {
                    lantersBlock[i].material.DisableKeyword("_EMISSION");
                }
            }
            else if (current.z > Target.z)
            {
                if (i == 2 || i == 3 || i == 6 || i == 7)
                {
                    lantersBlock[i].material.EnableKeyword("_EMISSION");
                }
                else
                {
                    lantersBlock[i].material.DisableKeyword("_EMISSION");
                }
            }
            else if (current.x < Target.x)
            {
                if(i > 3)
                {
                    lantersBlock[i].material.EnableKeyword("_EMISSION");
                }
                else
                {
                    lantersBlock[i].material.DisableKeyword("_EMISSION");
                }
            }
            else if(current.x > Target.x)
            {
                if (i < 4)
                {
                    lantersBlock[i].material.EnableKeyword("_EMISSION");
                }
                else
                {
                    lantersBlock[i].material.DisableKeyword("_EMISSION");
                }
            }
            else if (current.y < Target.y)
            {
                if (i % 2 == 1)
                {
                    lantersBlock[i].material.EnableKeyword("_EMISSION");
                }
                else
                {
                    lantersBlock[i].material.DisableKeyword("_EMISSION");
                }
            }
            else if (current.y > Target.y)
            {
                if (i % 2 == 0)
                {
                    lantersBlock[i].material.EnableKeyword("_EMISSION");
                }
                else
                {
                    lantersBlock[i].material.DisableKeyword("_EMISSION");
                }
            }
        }
    }
}

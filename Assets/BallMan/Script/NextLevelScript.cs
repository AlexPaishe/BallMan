using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelScript : MonoBehaviour
{
    private int step = 0;

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            step++;
        }
        if(step == 100)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}

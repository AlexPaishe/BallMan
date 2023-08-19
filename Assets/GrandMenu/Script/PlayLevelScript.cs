using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayLevelScript : MonoBehaviour
{
    [SerializeField] private LayerMask layer;
    [SerializeField] private int level;
    [SerializeField] private Material mat;
    [SerializeField] private GameObject sliderLevel;
    [SerializeField] private string tagIcon;

    private Animator anima;
    private AsyncOperation asyncLoad;

    private void Awake()
    {
        anima = GetComponent<Animator>();
    }

    private void Update()
    {
        RaycastLevel();
        Loading();
    }

    public void LevelUp()//Включение меню выбора уровней
    {
        this.gameObject.SetActive(true);
    }

    public void LevelDown()//Выключение меню выбора уровней
    {
        anima.SetBool("Down", false);
        this.gameObject.SetActive(false);
    }

    public void OffLevel()//Свертование меню выбора уровней
    {
        anima.SetBool("Down", true);
    }

    private void RaycastLevel()//Реализация рейкаста при наведении на значек уровня
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50.0f, layer))
        {
            if (hit.collider.CompareTag(tagIcon))
            {
                anima.SetBool("Up", true);
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    sliderLevel.SetActive(true);
                    asyncLoad = SceneManager.LoadSceneAsync(level);
                    asyncLoad.allowSceneActivation = false;
                }
            }
            else
            {
                anima.SetBool("Up", false);
            }
        }
        else
        {
            anima.SetBool("Up", false);
        }
    }

    private void Loading()//Реализация сладера загрузки уровня
    {
        if (asyncLoad != null)
        {
            double offset = 0.28 + (asyncLoad.progress * 0.2);
            float a = Convert.ToSingle(offset);
            mat.mainTextureOffset = new Vector2(a, 0);
            if (asyncLoad.progress >= .9f && !asyncLoad.allowSceneActivation)
            {
                asyncLoad.allowSceneActivation = true;
            }
        }
    }
}

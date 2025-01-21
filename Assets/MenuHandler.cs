using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MenuHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject defaultMenu;

    [Header("Menus")]
       
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject creditsMenu;

    private static List<GameObject> menuList = new List<GameObject>();

    public void Awake()
    {
        CreateMenuArray();
        SwitchToMenu(defaultMenu);
    }

    void CreateMenuArray()
    {
        if (pauseMenu != null)
        {
            menuList.Add(pauseMenu);
        }

        if(creditsMenu != null)
        {
            menuList.Add(creditsMenu);
        }

    }

    public void SwitchToMenu(GameObject _menu)
    {
        foreach (GameObject item in menuList.ToArray())
        {
            if(item != null)
            {
                if(item == _menu)
                {
                    _menu.SetActive(true);
                }

                else
                {
                    item.SetActive(false);
                }
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
    }
}

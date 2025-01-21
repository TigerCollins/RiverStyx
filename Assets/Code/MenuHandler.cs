using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.ComponentModel;
using TMPro;
using System.Linq;
using System.Text;

public class MenuHandler : MonoBehaviour
{
    public static MenuHandler menuInstance;

    [SerializeField]
    private GameObject defaultMenu;

    [Header("Menus")]
    private List<GameObject> menuList = new List<GameObject>();

    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject creditsMainMenu;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject creditsMenu;
    [SerializeField]
    private GameObject winStateMenu;
    [SerializeField]
    private GameObject loseStateMenu;



    [Header("Score")]
    [SerializeField]
    private TextMeshProUGUI winScoreText;
    [SerializeField]
    private TextMeshProUGUI loseScoreText;
    [SerializeField]
    private int gameScore;

    [Header("Credits")]
    [SerializeField]
    private TextMeshProUGUI mainCreditsText;
    [SerializeField]
    private TextMeshProUGUI pauseCreditsText;



    public void Awake()
    {  
        CreateInstance();
        CreateMenuArray();
        SwitchToMenu(defaultMenu);
    }

    private void Start()
    {
        GenerateCredits();
    }

    void CreateInstance()
    {
        if(menuInstance != null)
        {
            Destroy(gameObject);
            return;
        }
        menuInstance = this;
        DontDestroyOnLoad(gameObject);
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

        if (winStateMenu != null)
        {
            menuList.Add(winStateMenu);
        }

        if (loseStateMenu != null)
        {
            menuList.Add(loseStateMenu);
        }

        if (mainMenu != null)
        {
            menuList.Add(mainMenu);
        }

        if (creditsMainMenu != null)
        {
            menuList.Add(creditsMainMenu);
        }

    }

    public void SetScore(int _score)
    {
       gameScore = _score;
        winScoreText.text = "Score: " + gameScore.ToString();
        loseScoreText.text = "Score: " + gameScore.ToString();
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

    void GenerateCredits()
    {
        List<CreditsItem> _credits = MetaScript.metaInstance.GetCreditsList();
        string _string = "";

        foreach (CreditsItem item in _credits)
        {
            _string += item.GetName() + " - " + item.GetRole() + "<br>";
        }

        mainCreditsText.text = _string;
        pauseCreditsText.text = _string;
    }

}

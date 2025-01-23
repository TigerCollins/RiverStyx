using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.ComponentModel;
using TMPro;
using System.Linq;
using System.Text;
using UnityEngine.SceneManagement;

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
    public GameObject winStateMenu;
    [SerializeField]
    private GameObject loseStateMenu;
    [SerializeField]
    private GameObject HUDMenu;



    [Header("Score")]
    [SerializeField]
    private TextMeshProUGUI winScoreText;
    [SerializeField]
    private TextMeshProUGUI loseScoreText;
    [SerializeField]
    private TextMeshProUGUI hudScoreText;

    [Header("Credits")]
    [SerializeField]
    private TextMeshProUGUI mainCreditsText;
    [SerializeField]
    private TextMeshProUGUI pauseCreditsText;
    [SerializeField]
    private TextMeshProUGUI mainAssetsText;
    [SerializeField]
    private TextMeshProUGUI pauseAssetsText;

    public void Awake()
    {  
        CreateInstance();
        CreateMenuArray();
        SwitchToMenu(defaultMenu);
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void Start()
    {
        GenerateCredits();
        GenerateAssets();
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if(scene.name != "GameLevel")
        {
            SwitchToMenu(HUDMenu);
        }

        else
        {
            SwitchToMenu(mainMenu);
        }
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

        if (HUDMenu != null)
        {
            menuList.Add(HUDMenu);
        }

    }

    public void SetScoreUI(int _score)
    {
        winScoreText.text = "Fairies Crossed: " + _score.ToString();
        loseScoreText.text = "Fairies Crossed: " + _score.ToString();
        hudScoreText.text = "Fairies Crossed: " + _score.ToString();
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

    void GenerateAssets()
    {
        List<AssetsItem> _assets = MetaScript.metaInstance.GetAssetsList();
        string _string = "";

        foreach (AssetsItem item in _assets)
        {
            _string += item.GetName() + "<br>";
        }

        mainAssetsText.text = _string;
        pauseAssetsText.text = _string;
    }

}

using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class CreditsItem
{
    [SerializeField]
    string name;
    [SerializeField]
    string role;

    public string GetName()
    {
        return name;
    }
    public string GetRole()
    {
        return role;
    }
}

[System.Serializable]
public class AssetsItem
{
    [SerializeField]
    string name;

    public string GetName()
    {
        return name;
    }
    
}

public enum LevelType { MainMenu, Level1, Level2, Level3 }

public class MetaScript : MonoBehaviour
{
    public static MetaScript metaInstance;
    MenuHandler menuHandler;

    [SerializeField]
    private List<CreditsItem> creditsList = new List<CreditsItem>();
    [SerializeField]
    private List<AssetsItem> assetsList = new List<AssetsItem>();
    int playerScore;
    LevelType levelType = LevelType.MainMenu;

    private void Awake()
    {
        FindReferences();
        CreateInstance();
        SetScore(0);
    }

    void FindReferences()
    {
        menuHandler = GameObject.FindObjectOfType<MenuHandler>();
    }

    void CreateInstance()
    {
        if (metaInstance != null)
        {
            Destroy(gameObject);
            return;
        }
        metaInstance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetTimeScale(float _timeScale)
    {
        Time.timeScale = _timeScale;
    }

    public void LoadLevel(LevelType level)
    {
        levelType = level;
        Application.LoadLevel(GetLevelStringFromEnum(level));
        print(levelType.ToString());
    }

    public string GetLevelStringFromEnum(LevelType level)
    {
        switch (level)
        {
            case LevelType.MainMenu:
                return "GameLevel";
                break;
            case LevelType.Level1:
                return "Level1";
                break;
            case LevelType.Level2:
                return "Level2";
                break;
            case LevelType.Level3:
                return "Level3";
                break;
            default:
                return "GameLevel";
                break;
        }
        return "GameLevel";
    }

    public LevelType GetNextLevel(LevelType currentLevel)
    {
        switch (currentLevel)
        {
            case LevelType.MainMenu:
                return LevelType.Level1;
                break;
            case LevelType.Level1:
                return LevelType.Level2;
                break;
            case LevelType.Level2:
                return LevelType.Level3;
                break;
            case LevelType.Level3:
                return LevelType.MainMenu;
                break;
            default:
                break;
        }
        return LevelType.MainMenu;
    }

    public void ReloadLevel()
    {
        LoadLevel(levelType);
    }

    public void StartGame(LevelType level)
    {
        SetScore(0);
        SetTimeScale(1);
        LoadLevel(level);
    }

    public void NextLevel(LevelType level)
    {
        SetScore(0);
        SetTimeScale(1);
        LoadLevel(level);
    }

    public void LoadNextLevel()
    {
        NextLevel(GetNextLevel(levelType));
    }

    public void QuitToMainMenu()
    {
        StartGame(LevelType.MainMenu);
    }

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
    }

    public void PlaySound()
    {

    }

    public List<CreditsItem> GetCreditsList()
    {
        return creditsList;
    }

    public List<AssetsItem> GetAssetsList()
    {
        return assetsList;
    }

    public void SetScore(int _score)
    {
        playerScore = _score;
        menuHandler.SetScoreUI(_score);
    }

    public int GetScore()
    {
        return playerScore;
    }
}

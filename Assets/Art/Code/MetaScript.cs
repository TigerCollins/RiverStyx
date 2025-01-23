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

public class MetaScript : MonoBehaviour
{
    public static MetaScript metaInstance;
    MenuHandler menuHandler;

    [SerializeField]
    private List<CreditsItem> creditsList = new List<CreditsItem>();
    [SerializeField]
    private List<AssetsItem> assetsList = new List<AssetsItem>();
    private enum eLevel { MainMenu, Level1, Level2, Level3 };
    int playerScore;

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

    public void LoadLevel(eLevel level)
    {
        Application.LoadLevel(GetLevelStringFromEnum(level));
    }

    public string GetLevelStringFromEnum(eLevel level)
    {
        switch (level)
        {
            case eLevel.MainMenu:
                return "GameLevel";
                break;
            case eLevel.Level1:
                return "Level1";
                break;
            case eLevel.Level2:
                return "Level2";
                break;
            case eLevel.Level3:
                return "Level3";
                break;
            default:
                return "GameLevel";
                break;
        }
    }

    public void ReloadLevel()
    {
        LoadLevel(SceneManager.GetActiveScene().name);
    }

    public void StartGame(eLevel level)
    {
        SetScore(0);
        SetTimeScale(1);
        LoadLevel(GetLevelStringFromEnum(level));
    }

    public void NextLevel(eLevel level)
    {
        SetScore(0);
        SetTimeScale(1);
        LoadLevel(GetLevelStringFromEnum(level));
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

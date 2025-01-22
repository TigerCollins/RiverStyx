using System.Collections.Generic;
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

    public void LoadLevel(string levelName)
    {
        Application.LoadLevel(levelName);
    }

    public void StartGame(string levelName)
    {
        SetScore(0);
        SetTimeScale(1);
        LoadLevel(levelName);
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
}

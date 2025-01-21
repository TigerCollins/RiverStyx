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
}

public class MetaScript : MonoBehaviour
{
    public static MetaScript metaInstance;

    [SerializeField]
    private List<CreditsItem> creditsList = new List<CreditsItem>();

    private void Awake()
    {
        CreateInstance();
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

    public void LoadLevel(string levelName)
    {
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
}

using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    public static DontDestroyOnLoad dontDestroy;

    void Awake()
    {
        CreateInstance();
    }

    void CreateInstance()
    {
        if (dontDestroy != null)
        {
            Destroy(gameObject);
            return;
        }
        dontDestroy = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

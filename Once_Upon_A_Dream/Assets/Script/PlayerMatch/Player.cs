using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public string username;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 이동 시 유지
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string username;
    public string roomId;
    public string chosenRole;

    void Awake()
    {
        // 싱글톤 처리
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // ← 씬 변경해도 삭제되지 않음
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

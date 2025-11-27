using UnityEngine;
using WebSocketSharp;

[System.Serializable]
public class MoveMessage
{
    public string type;
    public float x, y, z;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string username;
    public string roomId;
    public string chosenRole;

    private WebSocket ws;
    public GameObject otherPlayer;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void ConnectToRoom()
    {
        ws = new WebSocket($"ws://localhost:8000/ws/game/{roomId}/");
        ws.OnMessage += (sender, e) => OnMessage(e.Data);
        ws.Connect();
    }

    private void OnMessage(string data)
    {
        var msg = JsonUtility.FromJson<MoveMsg>(data);

        // 내 위치면 무시
        if (msg.username == username) return;

        // 상대 캐릭터에 위치 적용
        if (otherPlayer != null)
        {
            otherPlayer.transform.position = new Vector2(msg.x, msg.y);
        }
    }

    public void SendPosition(Vector3 pos)
    {
        if (ws != null && ws.IsAlive)
        {
            MoveMessage msg = new MoveMessage
            {
                type = "move",
                x = pos.x,
                y = pos.y,
                z = pos.z
            };
            ws.Send(JsonUtility.ToJson(msg));
        }
    }

    void OnDestroy()
    {
        if (ws != null) ws.Close();
    }
}

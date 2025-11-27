using UnityEngine;
using WebSocketSharp;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager I;

    WebSocket ws;

    void Awake()
    {
        I = this;
    }

    void Start()
    {
        ws = new WebSocket($"ws://localhost:8000/ws/game/{GameManager.Instance.roomId}/");
        ws.OnMessage += (s, e) => OnMessage(e.Data);
        ws.Connect();
    }

    void OnMessage(string json)
    {
        BaseMsg msg = JsonUtility.FromJson<BaseMsg>(json);

        switch (msg.type)
        {
            case "role_select": RemotePlayerManager.I.OnRole(json); break;
            case "move": RemotePlayerManager.I.OnMove(json); break;
            case "anim": RemotePlayerManager.I.OnAnim(json); break;
        }
    }

    // ------- º¸³»±â -------
    public void SendRole(string username, string role)
    {
        var msg = new RoleMsg { type = "role_select", username = username, role = role };
        ws.Send(JsonUtility.ToJson(msg));
    }

    public void SendMove(string username, Vector2 pos)
    {
        var msg = new MoveMsg { type = "move", username = username, x = pos.x, y = pos.y };
        ws.Send(JsonUtility.ToJson(msg));
    }

    public void SendAnim(string username, string anim)
    {
        var msg = new AnimMsg { type = "anim", username = username, animState = anim };
        ws.Send(JsonUtility.ToJson(msg));
    }

}

[System.Serializable]
public class BaseMsg { public string type; }

[System.Serializable]
public class MoveMsg : BaseMsg
{
    public string username;
    public float x;
    public float y;
}

[System.Serializable]
public class RoleMsg : BaseMsg
{
    public string username;
    public string role;
}

[System.Serializable]
public class AnimMsg : BaseMsg
{
    public string username;
    public string animState;
}
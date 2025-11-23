using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class UnitytoDjango : MonoBehaviour
{
    IEnumerator SendDataToDjango()
    {
        string url = "http://127.0.0.1:8000/unity/data/";
        string json = "{\"player\":\"´©³ª\", \"score\":100}";
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        Debug.Log(request.downloadHandler.text);
    }

    void Start()
    {
        StartCoroutine(SendDataToDjango());
    }
}

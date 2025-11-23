using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class LoginManager : MonoBehaviour
{
    public InputField nameInput;
    public InputField passwordInput;

    public void Login()
    {
        LoginData data = new LoginData(); // 이름이랑 비밀번호를 받습니다.
        data.username = nameInput.text;
        data.password = passwordInput.text;

        string json = JsonUtility.ToJson(data); //Json 으로 변환시킵니다.

        StartCoroutine(SendLoginRequest(json));
    }

    public void Register()
    {
        LoginData data = new LoginData(); // 이름이랑 비밀번호를 받습니다.
        data.username = nameInput.text;
        data.password = passwordInput.text;

        string json = JsonUtility.ToJson(data); //Json 으로 변환시킵니다.

        StartCoroutine(SendNewRequest(json));
    }

    private IEnumerator SendLoginRequest(string json)
    {
        UnityWebRequest request = new UnityWebRequest("http://127.0.0.1:8000/unity/login/", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("로그인 성공! 응답: " + request.downloadHandler.text);
        }
        else
        {
            Debug.Log("로그인 실패: " + request.error);
        }
    }

    private IEnumerator SendNewRequest(string json)
    {
        UnityWebRequest request = new UnityWebRequest("http://127.0.0.1:8000/unity/register/", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("회원가입 성공 : " + request.downloadHandler.text);
        }
        else
        {
            Debug.Log("회원가입 실패 : " + request.error);
        }
    }
}

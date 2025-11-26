using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;

public class RoleManager : MonoBehaviour
{
    public PlayerController roleAPlayer; // RoleA 캐릭터
    public PlayerController roleBPlayer; // RoleB 캐릭터

    public string username; // 로그인 후 저장된 유저명
    public string roomId;   // 매칭된 방 ID

    // 버튼에서 호출
    public void ChooseRole(string chosenRole)
    {
        RoleRequest req = new RoleRequest
        {
            username = username,
            room = roomId,
            chosenRole = chosenRole
        };
        StartCoroutine(SendRoleRequest(req));
    }

    private IEnumerator SendRoleRequest(RoleRequest req)
    {
        string json = JsonUtility.ToJson(req);
        UnityWebRequest request = new UnityWebRequest("http://127.0.0.1:8000/unity/choose_role/", "POST");
        request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(json));
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            RoleResponse res = JsonUtility.FromJson<RoleResponse>(request.downloadHandler.text);

            if (res.success)
            {
                Debug.Log("내 역할: " + res.role);

                // 선택한 역할 캐릭터만 움직이도록 활성화
                roleAPlayer.isMyTurn = res.role == "RoleA";
                roleBPlayer.isMyTurn = res.role == "RoleB";

                // 씬 이동 예: 역할 선택 후 게임 씬으로
                yield return new WaitForSeconds(1f); // 잠깐 대기
                SceneManager.LoadScene("GameScene");
            }
            else
            {
                Debug.Log("선택 실패: " + res.message);
            }
        }
        else
        {
            Debug.Log("요청 실패: " + request.error);
        }
    }
}

[System.Serializable]
public class RoleRequest
{
    public string username;
    public string room;
    public string chosenRole;
}

[System.Serializable]
public class RoleResponse
{
    public bool success;
    public string role;
    public string message;
}

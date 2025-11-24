using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public string role; // 내 역할(RoleA, RoleB 등)
    public bool isMyTurn = false; // 역할 선택 후 활성화

    public float speed = 5f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!isMyTurn) return; // 선택 안 한 역할이면 움직이지 않음

        float h = Input.GetAxis("Horizontal"); // A/D, ←/→
        float v = Input.GetAxis("Vertical");   // W/S, ↑/↓

        Vector3 move = new Vector3(h, 0, v) * speed * Time.deltaTime;
        rb.MovePosition(transform.position + move);
    }
}

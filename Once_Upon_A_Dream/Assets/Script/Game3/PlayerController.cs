using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public string role; // 내 역할(RoleA, RoleB 등)
    public bool isMyTurn = false;

    public float speed = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!isMyTurn) return;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector2 move = new Vector2(h, v).normalized * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);

        // 위치 서버에 보내기
        NetworkManager.I.SendMove(GameManager.Instance.username, rb.position);
    }

}

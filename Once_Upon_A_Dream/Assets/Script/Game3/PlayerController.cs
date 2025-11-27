using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public string role; // 내 역할(RoleA, RoleB 등)
    public bool isMyTurn = false; // 역할 선택 후 활성화

    public float speed = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isMyTurn) return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(h, v) * speed * Time.deltaTime;
        transform.Translate(move);

    }
}

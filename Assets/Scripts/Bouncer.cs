using UnityEngine;

public class Bouncer : MonoBehaviour
{
    [SerializeField] private float bounceForce = 10f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
        }
    }
}
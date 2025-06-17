using UnityEngine;

public class PuppetMove : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 3f;
    public float followDistance = 2f;

    private Transform player;
    private Rigidbody2D rb;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.gravityScale = 0;
            rb.freezeRotation = true;
        }
    }

    private void FixedUpdate()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > followDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            if (rb != null)
            {
                rb.linearVelocity = direction * speed;
            }
            else
            {
                transform.position = Vector2.MoveTowards(
                    transform.position,
                    player.position,
                    speed * Time.deltaTime
                );
            }
        }
        else if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
}
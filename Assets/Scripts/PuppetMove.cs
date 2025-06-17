using UnityEngine;

public class PuppetMove : MonoBehaviour
{
    [Header("Movement Settings")]
    public float followSpeed = 3f;
    public float minFollowDistance = 2f;
    public float maxFollowDistance = 5f;

    [Header("Physics Settings")]
    public ForceMode2D movementForceMode = ForceMode2D.Force;
    public float stoppingDistance = 0.5f;

    public bool IsFollowing { get; private set; } = true; // Added back the IsFollowing property

    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        rb = GetComponent<Rigidbody2D>();

        if (player == null)
            Debug.LogError("Player not found! Make sure player has 'Player' tag.");
    }

    void FixedUpdate()
    {
        if (player == null)
        {
            IsFollowing = false;
            return;
        }

        Vector2 direction = player.position - transform.position;
        float distance = direction.magnitude;

        // Update IsFollowing based on distance
        IsFollowing = distance > minFollowDistance;

        // Only move if outside follow range
        if (distance > maxFollowDistance)
        {
            Vector2 moveDirection = direction.normalized;

            if (rb != null)
            {
                // Physics-based movement
                if (distance > stoppingDistance)
                {
                    rb.AddForce(moveDirection * followSpeed, movementForceMode);
                }
                else
                {
                    rb.linearVelocity = Vector2.zero;
                }
            }
            else
            {
                // Non-physics movement
                transform.position = Vector2.MoveTowards(
                    transform.position,
                    player.position,
                    followSpeed * Time.fixedDeltaTime
                );
            }
        }
    }
}
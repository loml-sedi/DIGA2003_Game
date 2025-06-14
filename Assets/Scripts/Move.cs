using UnityEngine;
using System.Collections;

public class GridMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float gridSize = 1f; // Must match your tile size

    private bool isMoving;
    private Vector3 targetPosition;

    void Update()
    {
        if (!isMoving)
        {
            // Get input
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            // Prioritize one direction at a time
            if (horizontal != 0) vertical = 0;

            if (horizontal != 0 || vertical != 0)
            {
                // Calculate target position
                targetPosition = transform.position +
                                new Vector3(horizontal * gridSize, vertical * gridSize, 0);

                // Start movement
                StartCoroutine(MoveToTarget());
            }
        }
    }

    IEnumerator MoveToTarget()
    {
        isMoving = true;

        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                                                    targetPosition,
                                                    moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Snap to final grid position
        transform.position = targetPosition;
        isMoving = false;
    }
}
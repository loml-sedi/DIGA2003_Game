using UnityEngine;

public class collectable : MonoBehaviour
{
    [SerializeField] private int value;
    private bool hasTriggered;

    // Works for both trigger and collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleCollection(collision.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollection(collision.gameObject);
    }

    private void HandleCollection(GameObject collector)
    {
        if (collector.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            CollectableManager.instance.ChangeCollectable(value); // Fixed method name
            Destroy(gameObject);
        }
    }
}
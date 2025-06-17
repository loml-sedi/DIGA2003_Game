using UnityEngine;

public class PuppetController : MonoBehaviour
{
    [Header("Despawn Settings")]
    public float maxDistanceFromPlayer = 15f;
    public float minYPosition = -10f;

    private MagicBox magicBox;
    private Transform player;

    public void Initialize(MagicBox box)
    {
        magicBox = box;
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        if (ShouldDespawn())
        {
            magicBox.DespawnPuppet();
        }
    }

    private bool ShouldDespawn()
    {
        if (player == null) return false;

        // Check distance from player
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > maxDistanceFromPlayer) return true;

        // Check if fallen off world
        if (transform.position.y < minYPosition) return true;

        return false;
    }
}
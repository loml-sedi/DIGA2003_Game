using UnityEngine;

public class MagicBox : MonoBehaviour
{
    [Header("Settings")]
    public int requiredObjects = 0; // Set to 0 for instant unlock
    public GameObject puppetPrefab;
    public Transform spawnPoint;
    public VentSystem ventSystem;

    [Header("Effects")]
    public ParticleSystem spawnEffect;
    public AudioClip unlockSound;

    private bool hasActivated = false;
    private bool canSpawnPuppet = false; // New flag to control puppet spawning

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasActivated)
        {
            // Check if player has enough collectables
            if (CollectableManager.instance.GetCurrentCollectable() >= requiredObjects)
            {
                canSpawnPuppet = true; // Allow puppet to spawn
                UnlockVent(); // Unlock vent immediately
                Debug.Log("Magic Box activated! Puppet will spawn on collision.");
            }
            else
            {
                Debug.LogWarning($"Not enough collectables! Have: {CollectableManager.instance.GetCurrentCollectable()}, Need: {requiredObjects}");
            }
        }
    }

    // This is called when the player collides with the magic box item
    public void OnMagicBoxItemCollected()
    {
        if (canSpawnPuppet && !hasActivated)
        {
            SpawnNPC();
            hasActivated = true;
        }
    }

    void SpawnNPC()
    {
        if (puppetPrefab == null)
        {
            Debug.LogError("Puppet prefab not assigned!");
            return;
        }

        if (spawnPoint == null)
        {
            Debug.LogError("Spawn point not assigned!");
            return;
        }

        // Spawn NPC
        Instantiate(puppetPrefab, spawnPoint.position, Quaternion.identity);

        // Play effects
        if (spawnEffect != null)
            Instantiate(spawnEffect, spawnPoint.position, Quaternion.identity);

        Debug.Log("Puppet spawned!");
    }

    void UnlockVent()
    {
        if (ventSystem == null)
        {
            Debug.LogError("VentSystem not assigned!");
            return;
        }

        // Directly unlock the vent
        ventSystem.isLocked = false;
        ventSystem.UpdateAppearance();

        // Play sound
        if (unlockSound != null)
            AudioSource.PlayClipAtPoint(unlockSound, transform.position);

        Debug.Log($"Vent unlocked! isLocked={ventSystem.isLocked}");
    }

    // Debugging aid
    private void OnDrawGizmos()
    {
        if (spawnPoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(spawnPoint.position, 0.5f);
        }
    }
}
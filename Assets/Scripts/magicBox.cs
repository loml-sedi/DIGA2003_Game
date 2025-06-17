using UnityEngine;

public class MagicBox : MonoBehaviour
{
    [Header("Settings")]
    public int requiredObjects = 5;
    public GameObject puppetPrefab;
    public Transform spawnPoint;
    public VentSystem ventSystem;

    [Header("Effects")]
    public ParticleSystem spawnEffect;
    public AudioClip unlockSound;

    private bool hasActivated = false;
    private GameObject spawnedPuppet; // Track the spawned puppet

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasActivated)
        {
            if (CollectableManager.instance.GetCurrentCollectable() >= requiredObjects)
            {
                SpawnNPC();
                UnlockVent();
                hasActivated = true;
            }
            else
            {
                Debug.LogWarning($"Not enough collectables! Need: {requiredObjects}");
            }
        }
    }

    void SpawnNPC()
    {
        if (puppetPrefab == null || spawnPoint == null)
        {
            Debug.LogError("Missing puppet prefab or spawn point!");
            return;
        }

        // Destroy existing puppet if any
        if (spawnedPuppet != null)
            Destroy(spawnedPuppet);

        // Spawn with proper initialization
        spawnedPuppet = Instantiate(puppetPrefab, spawnPoint.position, Quaternion.identity);

        // Ensure it has proper components
        var puppetMove = spawnedPuppet.GetComponent<PuppetMove>();
        if (puppetMove == null)
        {
            Debug.LogWarning("Spawned puppet missing PuppetMove script!");
        }

        // Play effects
        if (spawnEffect != null)
            Instantiate(spawnEffect, spawnPoint.position, Quaternion.identity);
    }

    void UnlockVent()
    {
        if (ventSystem != null)
        {
            ventSystem.isLocked = false;
            ventSystem.UpdateAppearance();

            if (unlockSound != null)
                AudioSource.PlayClipAtPoint(unlockSound, transform.position);
        }
    }

    private void OnDrawGizmos()
    {
        if (spawnPoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(spawnPoint.position, 0.5f);
        }
    }
}
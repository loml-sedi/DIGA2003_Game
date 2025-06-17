using UnityEngine;

public class MagicBox : MonoBehaviour
{
    [Header("Spawning Settings")]
    public int requiredCollectables = 3;
    public GameObject puppetPrefab; // Assign in inspector
    public Transform spawnPoint;    // Assign in inspector
    public VentSystem ventSystem;   // Assign if needed

    [Header("Effects")]
    public ParticleSystem spawnEffect;
    public ParticleSystem despawnEffect;
    public AudioClip spawnSound;

    private GameObject currentPuppet;
    private bool hasSpawned = false;

    private void Start()
    {
        // Ensure no puppet exists at start
        if (currentPuppet != null)
        {
            Destroy(currentPuppet);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasSpawned) return;

        if (other.CompareTag("Player"))
        {
            TrySpawnPuppet();
        }
    }

    private void TrySpawnPuppet()
    {
        // Get current collectables safely
        int currentCollectables = 0;
        if (CollectableManager.instance != null)
        {
            currentCollectables = CollectableManager.instance.GetCurrentCollectable();
        }

        if (currentCollectables >= requiredCollectables)
        {
            SpawnPuppet();
            UnlockVent();
            hasSpawned = true;
            Debug.Log("Puppet spawned successfully!");
        }
        else
        {
            Debug.Log($"Need {requiredCollectables} collectables (have {currentCollectables})");
        }
    }

    private void SpawnPuppet()
    {
        if (currentPuppet != null) return;

        // Instantiate new puppet
        currentPuppet = Instantiate(puppetPrefab, spawnPoint.position, Quaternion.identity);

        // Play effects
        if (spawnEffect != null)
        {
            Instantiate(spawnEffect, spawnPoint.position, Quaternion.identity);
        }

        if (spawnSound != null)
        {
            AudioSource.PlayClipAtPoint(spawnSound, transform.position);
        }
    }

    public void DespawnPuppet()
    {
        if (currentPuppet == null) return;

        // Play despawn effects
        if (despawnEffect != null)
        {
            Instantiate(despawnEffect, currentPuppet.transform.position, Quaternion.identity);
        }

        Destroy(currentPuppet);
        currentPuppet = null;
        hasSpawned = false;
        Debug.Log("Puppet despawned");
    }

    private void UnlockVent()
    {
        if (ventSystem != null)
        {
            ventSystem.isLocked = false;
            ventSystem.UpdateAppearance();
        }
    }
}
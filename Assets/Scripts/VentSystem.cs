using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SpriteRenderer))] 
[RequireComponent(typeof(BoxCollider2D))]
public class VentSystem : MonoBehaviour
{
    [Header("Settings")]
    public bool isLocked = true;
    public string nextSceneName = "Level2"; // Must match scene name in Build Settings

    [Header("Visuals")]
    public Sprite lockedSprite;
    public Sprite unlockedSprite;
    public Color lockedColor = Color.red;
    public Color unlockedColor = Color.green;

    [Header("Effects")]
    public ParticleSystem unlockParticles;
    public AudioClip unlockSound;
    public AudioClip enterSound;
    public float enterDelay = 0.5f; // Delay before scene loads

    // Components
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _collider;
    private AudioSource _audioSource;

    private void Awake()
    {
        // Get required components
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
        _audioSource = GetComponent<AudioSource>();

        // Initialize
        UpdateAppearance();
        Debug.Log($"Vent initialized. Locked: {isLocked}");
    }

    public void UpdateAppearance()
    {
        if (_spriteRenderer == null) return;

        // Update sprite and color
        _spriteRenderer.sprite = isLocked ? lockedSprite : unlockedSprite;
        _spriteRenderer.color = isLocked ? lockedColor : unlockedColor;

        // Log changes for debugging
        Debug.Log($"Vent appearance updated. Locked: {isLocked}");
    }

    public void Unlock()
    {
        if (!isLocked) return; // Already unlocked

        isLocked = false;

        // Visual/audio feedback
        if (unlockParticles != null)
            unlockParticles.Play();

        if (unlockSound != null && _audioSource != null)
            _audioSource.PlayOneShot(unlockSound);

        UpdateAppearance();
        Debug.Log("Vent unlocked successfully!");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") )return;
        if (isLocked) return;

        Debug.Log("Player entered unlocked vent");
        StartCoroutine(EnterVent());
    }

    private System.Collections.IEnumerator EnterVent()
    {
        // Play enter sound
        if (enterSound != null && _audioSource != null)
            _audioSource.PlayOneShot(enterSound);

        // Optional: Trigger player animation here

        // Wait before loading
        yield return new WaitForSeconds(enterDelay);

        // Load scene if name is valid
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            Debug.Log($"Loading scene: {nextSceneName}");
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("Next scene name not set!");
        }
    }

    // Visual debugging in editor
    private void OnDrawGizmos()
    {
        Gizmos.color = isLocked ? Color.red : Color.green;
        if (_collider != null)
        {
            Gizmos.DrawWireCube(transform.position, _collider.size);
        }
    }
}
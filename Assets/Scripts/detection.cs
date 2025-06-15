using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class detection : MonoBehaviour
{
    [Header("Laser Settings")]
    public float rotateSpeed = 180f;
    public float distance = 10f;
    public LineRenderer lineOfContact;
    public Gradient redColour;
    public Gradient greenColour;

    [Header("Sound Settings")]
    public AudioClip detectionSound;
    public AudioClip playerHitSound;
    [Range(0f, 1f)] public float volume = 0.8f;
    public float soundCooldown = 0.3f;

    private AudioSource audioSource;
    private float lastSoundTime;
    private bool isPlayerHitInProgress;

    void Start()
    {
        Physics2D.queriesStartInColliders = false;
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.volume = volume;

        if (lineOfContact == null)
        {
            lineOfContact = GetComponent<LineRenderer>();
        }
    }

    void Update()
    {
        if (isPlayerHitInProgress) return;

        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distance);

        if (hitInfo.collider != null)
        {
            lineOfContact.colorGradient = redColour;
            lineOfContact.SetPosition(1, hitInfo.point);

            if (hitInfo.collider.CompareTag("Player"))
            {
                PlayerImmunity immunity = hitInfo.collider.GetComponent<PlayerImmunity>();
                if (immunity == null || !immunity.IsImmune())
                {
                    StartCoroutine(HandlePlayerHit(hitInfo.collider.gameObject));
                }
                else
                {
                    PlaySound(detectionSound);
                }
            }
            else
            {
                PlaySound(detectionSound);
            }
        }
        else
        {
            lineOfContact.colorGradient = greenColour;
            lineOfContact.SetPosition(1, transform.position + transform.right * distance);
        }

        lineOfContact.SetPosition(0, transform.position);
    }

    IEnumerator HandlePlayerHit(GameObject player)
    {
        isPlayerHitInProgress = true;

        // 1. Play the hit sound
        if (playerHitSound != null)
        {
            AudioSource.PlayClipAtPoint(playerHitSound, Camera.main.transform.position, volume);
            Debug.Log("Player hit sound played");

            // Wait for the sound to start playing
            yield return null;

            // Optional: Wait for sound to complete (if you want the full sound to play)
            // yield return new WaitForSeconds(playerHitSound.length);
        }

        // 2. Then process the hit
        DHealth(player);

        isPlayerHitInProgress = false;
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null && Time.time > lastSoundTime + soundCooldown)
        {
            audioSource.PlayOneShot(clip);
            lastSoundTime = Time.time;
        }
    }

    void DHealth(GameObject player)
    {
        if (HeartSystem.instance != null)
        {
            HeartSystem.instance.TakeDamage(1);
            StartCoroutine(RestartAfterDelay(0.5f)); // Small delay after sound
        }
        else
        {
            Debug.LogError("HeartSystem instance not found!");
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
    }

    IEnumerator RestartAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        HeartSystem.instance.restartGame();
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * distance);
    }
#endif
}
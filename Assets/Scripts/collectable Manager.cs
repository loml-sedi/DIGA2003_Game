using UnityEngine;
using TMPro;

public class CollectableManager : MonoBehaviour
{
    public static CollectableManager instance;

    [Header("Collection Settings")]
    [SerializeField] private int collectable;
    [SerializeField] private TMP_Text collectableDisplay;
    [SerializeField] private int maxCollectables = 999;

    [Header("Effects")]
    [SerializeField] private GameObject collectionEffect;
    [SerializeField] private AudioClip collectionSound;

    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Initialize();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Initialize()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        UpdateUI();
    }

    public int GetCurrentCollectable()
    {
        return collectable;
    }

    public void ChangeCollectable(int amount)
    {
        collectable = Mathf.Clamp(collectable + amount, 0, maxCollectables);
        UpdateUI();

        if (amount > 0)
        {
            PlayCollectionEffects();
        }
    }

    public void Changecollectable(int amount)
    {
        ChangeCollectable(amount);
    }

    private void UpdateUI()
    {
        if (collectableDisplay != null)
        {
            collectableDisplay.text = collectable.ToString();
        }
    }

    private void PlayCollectionEffects()
    {
        if (collectionSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(collectionSound);
        }

        if (collectionEffect != null)
        {
            Instantiate(collectionEffect, transform.position, Quaternion.identity);
        }
    }

    public void ResetCollectables()
    {
        collectable = 0;
        UpdateUI();
    }
}
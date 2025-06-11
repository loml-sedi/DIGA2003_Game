using UnityEngine;
using TMPro;
/*Shark Games (2023) 2D COIN COLLECTION IN UNITY (Game dev tutorial). 14 May. [Online] Available at: https://www.youtube.com/watch?v=YUp-kl06RUM ( Accessed: 16 April 2025). */

public class collectableManager : MonoBehaviour
{
    public static collectableManager instance;

    private int collectable;
    [SerializeField] private TMP_Text collectableDisplay;

    private void Awake()
    {
        if (!instance) instance = this;
    }

    // Public method to GET the current value
    public int GetCurrentCollectable()
    {
        return collectable;
    }

    // Public method to MODIFY the value
    public void Changecollectable(int amount)
    {
        collectable += amount;
        collectableDisplay.text = collectable.ToString();
    }
}
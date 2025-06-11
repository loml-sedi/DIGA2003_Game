using UnityEngine;
using System.Collections;

public class PlayerImmunity : MonoBehaviour
{
    [Header("Settings")]
    public float immunityDuration = 3f;
    public SpriteRenderer playerSprite;

    private bool _isImmune = false;
    private Coroutine _immunityRoutine;

    public void ActivateImmunity()
    {
        if (!_isImmune)
        {
            if (_immunityRoutine != null)
                StopCoroutine(_immunityRoutine);

            _immunityRoutine = StartCoroutine(ImmunityRoutine());
        }
    }

    private IEnumerator ImmunityRoutine()
    {
        _isImmune = true;
        float elapsed = 0f;

        while (elapsed < immunityDuration)
        {
            // Toggle visibility for blinking effect
            playerSprite.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(0.15f);
            playerSprite.color = Color.white;
            yield return new WaitForSeconds(0.15f);
            elapsed += 0.3f;
        }

        _isImmune = false;
    }

    public bool IsImmune() => _isImmune;
}
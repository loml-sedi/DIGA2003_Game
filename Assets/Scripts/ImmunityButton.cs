using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*
 * Estay. C, 2020. Unity Custom Button Class with more events [Online]  Available at: https://youtu.be/RhC8_MdCGTs?si=zHsaQYIyFmFckv5s (Accessed: 18 April 2025) 
 * GameDevGuide, 2021. Unity UI canvas modes and canvas scaler explained. [Online]  Available at: https://www.youtube.com/watch?v=1OwQflHq5kg (Accessed: 19 April 2025) 
 * Hottest Gadgets /Unity Game Dev, 2022. how to use button interactable in unity 2022 [Online]  Available at: https://youtu.be/Yx6xi1tePjM?si=XZq-cqeg9pi0JjrT (Accessed: 17 April 2025) 
 * Simple, 2020. TIMERS IN UNITY!! Ability Cooldowns (Unity Coroutine Tutorial) [Online]  Available at: https://youtu.be/Z2hD0WxCFak?si=UsjP0952HKGwKsoW (Accessed: 19 April 2025) 
 * SpeedTutor, 2021. How to create buttons in Unity. [YouTube video. [Online]  Available at: https://youtu.be/gSfdCke3684?si=XNYuKFub8gNW82NZ (Accessed: 18 April 2025) 
 * 
 */

public class ImmunityButton : MonoBehaviour
{
    public PlayerImmunity playerImmunity;
    public Image cooldownOverlay;
    public float cooldownDuration = 5f;
    public Vector2 screenPosition = new Vector2(-30, 70); 

    private Button _button;
    private RectTransform _rectTransform;
    private bool _isCooldownActive;

    void Awake()
    {
        _button = GetComponent<Button>();
        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.anchoredPosition = screenPosition;
        _rectTransform.sizeDelta = new Vector2(200, 80);

        if (cooldownOverlay != null)
        {
            cooldownOverlay.type = Image.Type.Filled;
            cooldownOverlay.fillMethod = Image.FillMethod.Radial360;
            cooldownOverlay.fillAmount = 0;
            cooldownOverlay.raycastTarget = false;
        }
        else
        {
            Debug.LogWarning("Cooldown overlay not assigned!", this);
        }
    }

    [System.Obsolete]
    void Start()
    {
        if (playerImmunity == null)
        {
            playerImmunity = FindObjectOfType<PlayerImmunity>();
            if (playerImmunity == null)
                Debug.LogError("No PlayerImmunity found in scene!", this);
        }

        _button.onClick.AddListener(OnButtonClicked);
    }

    void OnButtonClicked()
    {
        if (!_isCooldownActive && playerImmunity != null && !playerImmunity.IsImmune())
        {
            playerImmunity.ActivateImmunity();
            StartCoroutine(CooldownRoutine());
        }
    }

    IEnumerator CooldownRoutine()
    {
        _isCooldownActive = true;
        _button.interactable = false;
        float timer = cooldownDuration;

        while (timer > 0)
        {
            if (cooldownOverlay != null)
                cooldownOverlay.fillAmount = timer / cooldownDuration;

            timer -= Time.deltaTime;
            yield return null;
        }

        if (cooldownOverlay != null)
            cooldownOverlay.fillAmount = 0;

        _button.interactable = true;
        _isCooldownActive = false;
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        if (_rectTransform == null)
            _rectTransform = GetComponent<RectTransform>();

        _rectTransform.anchoredPosition = screenPosition;
    }
#endif
}

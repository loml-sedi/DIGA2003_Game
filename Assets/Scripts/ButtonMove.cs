using UnityEngine;

public class ButtonMove : MonoBehaviour
{
    public Transform player;
    public Vector2 screenOffset = new Vector2(0, 50f);

    private RectTransform _rectTransform;
    private Camera _mainCam;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _mainCam = Camera.main;
    }

    void Update()
    {
        if (player == null) return;

        Vector2 screenPoint = _mainCam.WorldToScreenPoint(player.position);

        _rectTransform.position = screenPoint + screenOffset;
    }
}

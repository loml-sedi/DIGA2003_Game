using UnityEngine;

public class detection : MonoBehaviour
{
    public float rotateSpeed;
    public float distance;
    public LineRenderer lineOfContact;
    public Gradient redColour;
    public Gradient greenColour;

    void Start() => Physics2D.queriesStartInColliders = false;

    void Update()
    {
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
                    DHealth();
                }
            }
        }
        else
        {
            lineOfContact.colorGradient = greenColour;
            lineOfContact.SetPosition(1, transform.position + transform.right * distance);
        }

        lineOfContact.SetPosition(0, transform.position);
    }

    void DHealth()
    {
        if (HeartSystem.instance != null)
        {
            HeartSystem.instance.TakeDamage(1);
            HeartSystem.instance.restartGame();
        }
        else
        {
            Debug.LogError("HeartSystem instance is null!");
        }
    }
}
using UnityEngine;
/*Anup Prasad (2020) How to Make Camera Follow In UNITY 2D. 30 October. [Online] Available at: https://www.youtube.com/watch?v=FXqwunFQuao ( Accessed: 20 April 2025). */

public class cameraFollow : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public float yOffset = 1f;
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x, target.position.y + yOffset, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
    }
}

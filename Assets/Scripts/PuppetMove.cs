using UnityEngine;

/*
 * Lague, S. (2014). A Pathfinding Tutorial. Retrieved from YouTube: https://youtu.be/nhiFx28e7JY
 * Reddit user. (2013). How to proper spawn NPCs. Retrieved from Reddit: https://www.reddit.com/r/gmod/comments/1av6fvl/how_to_proper_spawn_npcs/
 */

public class PuppetMove : MonoBehaviour
{
    public float followSpeed = 3f;
    public float followDistance = 1f;
    public bool IsFollowing { get; private set; } 
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player == null)
            Debug.LogError("No GameObject with tag 'Player' found!");
    }

    void Update()
    {
        if (player != null)
        {
            IsFollowing = true;

            Vector2 direction = player.position - transform.position;
            if (direction.magnitude > followDistance)
            {
                transform.position = Vector2.MoveTowards(
                    transform.position,
                    player.position,
                    followSpeed * Time.deltaTime
                );
            }
        }
    }
}
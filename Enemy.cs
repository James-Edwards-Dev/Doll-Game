using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 1; //enemy speed
    public float stopDist = 0.25f;
    private Transform player;
    private float playerRad = 0f;

    void Start()
    {
        //searches for object with player tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player"); 

        //sets the transform component to the player
        if (playerObject != null)
        {
            player = playerObject.transform;

            //checks collision
            Collider2D playerCollider = playerObject.GetComponent<Collider2D>();
            if (playerCollider != null)
            {
                playerRad = playerCollider.bounds.extents.magnitude; //sets radius
            }
        }


    }
    void Update()
    {
        if (player != null) 
        {

            Vector2 direction = (player.position - transform.position).normalized;
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            float stopAtDist = playerRad + stopDist;

            if (distanceToPlayer > stopAtDist)
            {
                //moves towards player
                transform.position += (Vector3)direction * speed * Time.deltaTime;
            }
        }
    }
}

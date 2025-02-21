using UnityEngine;

public class Feathers : MonoBehaviour
{
    public float speed = 10f;
    private Transform player;
    public GameObject feather;
    private float playerRad = 5f;
    public float stopDist = 0;

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
                transform.position += (Vector3)direction * speed * Time.deltaTime;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player")) // checks if collides with player
        {
            Destroy(gameObject); //destroys itself
        }

    }
}

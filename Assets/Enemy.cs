using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed; //enemy speed
    private Transform player;

    void Start()
    {
        //searches for object with player tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player"); 

        //sets the transform component to the player
        if (playerObject != null)
        {
            player = playerObject.transform;
        }

    }
    void Update()
    {
        if (player != null) 
        {
            //moves towards player
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)direction * speed * Time.deltaTime;
        }
    }
}

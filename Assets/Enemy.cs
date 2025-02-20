using UnityEngine;
using System.Collections;
public class Enemy : MonoBehaviour
{
    public float speed = 1; //enemy speed
    public float stopDist = 0.25f;
    private Transform player;
    private float playerRad = 0f;
    public float fadeDuration = 1.5f;
    public float destroyDelay = 2.0f;
    private SpriteRenderer spriteRenderer;
    private bool isDying = false;
    private GameLoop gameLoop;

    void Start()
    {
        gameLoop = FindObjectOfType<GameLoop>();
        spriteRenderer = GetComponent<SpriteRenderer>();        
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
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet")) // Check if the colliding object has the "bullet" tag
        {
            GetComponent<BoxCollider2D>().enabled = false;
            speed = 0;
            Destroy(collision.gameObject); // Destroy the bullet
            StartCoroutine(DeathRoutine());
            gameLoop.enemiesRemaining--;
            gameLoop.enemyCount--;

        }
    }
    IEnumerator DeathRoutine()
    {
        isDying = true;
        Animator animator = GetComponent<Animator>();

        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        yield return new WaitForSeconds(0.5f);

        float elapsedTime = 0;
        Color originalColor = spriteRenderer.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}

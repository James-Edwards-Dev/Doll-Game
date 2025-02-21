using UnityEngine;
using System.Collections;
public class Enemy : MonoBehaviour
{
    public float speed = 2f; //enemy speed
    public float stopDist = 0.25f;
    private Transform playerT;
    private float playerRad = 5f;
    public float fadeDuration = 3f;
    public float destroyDelay = 4f;
    public bool isAlive = true;
    private SpriteRenderer spriteRenderer;
    private GameLoop gameLoop;
    public float attackRange = 2f;
    public float attackDMG = 5f;
    public float attackCD = 1.5f;
    private bool isAttacking = false;

    void Start()
    {
        gameLoop = FindFirstObjectByType<GameLoop>();
        spriteRenderer = GetComponent<SpriteRenderer>();        
        //searches for object with player tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player"); 

        //sets the transform component to the player
        if (playerObject != null)
        {
            playerT = playerObject.transform;

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
        if (playerT != null) 
        {

            Vector2 direction = (playerT.position - transform.position).normalized;
            float distanceToPlayer = Vector2.Distance(transform.position, playerT.position);
            float stopAtDist = playerRad + stopDist;

            if (distanceToPlayer > stopAtDist)
            {
                //moves towards player
                transform.position += (Vector3)direction * speed * Time.deltaTime;
            }
        }
        if (!isAttacking)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);
            foreach (var collider in colliders)  
            {    
                Debug.Log("Found collider: " + collider.name);        
                if (collider.CompareTag("Player"))
                {
                    Debug.Log("Player in range!");
                    StartCoroutine(Attack(collider.gameObject));
                    break;
                }
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet")) // Check if the colliding object has the "bullet" tag
        {
            isAlive = false;
            StartCoroutine(DeathRoutine()); //starts death animation
            GetComponent<BoxCollider2D>().enabled = false; //disables the enemies collider
            speed = 0; //stops the enemy from moving
            Destroy(collision.gameObject); // Destroy the bullet

            gameLoop.enemiesRemaining--;
            gameLoop.enemyCount--;
        }
    }
    IEnumerator Attack(GameObject target)
    {
        if (isAlive)
        {
            isAttacking = true;

            player PlayerHP = target.GetComponent<player>();

            if (PlayerHP.health > 0)
            {
                PlayerHP.damage(attackDMG);
            }

            yield return new WaitForSeconds(attackCD);
            isAttacking = false;
        }
    }
    IEnumerator DeathRoutine()
    {
        Animator animator = GetComponent<Animator>(); //runs animator
        Destroy(transform.GetChild(0).gameObject); // Destroys hammer

        if (animator != null)
        {
            animator.SetTrigger("Die"); //sets animation trigger
        }

        yield return new WaitForSeconds(0.5f); //waits .5 seconds

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

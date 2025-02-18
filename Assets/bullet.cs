using System.Collections;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;
    public float despawnSeconds = 1;
    
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * speed * 100);

        StartCoroutine(DespawnTimer(despawnSeconds));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DespawnTimer(float despawnSeconds)
    {
        yield return new WaitForSecondsRealtime(despawnSeconds);

        Destroy(gameObject);
    }
}

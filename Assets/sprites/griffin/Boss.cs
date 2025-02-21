using UnityEngine;
using System.Collections;
public class Boss : MonoBehaviour
{
    public GameObject Feathers;
    public float spawnInterval = 2f;
    public float feathersPerAttack = 5;
    public float spawnDistance = 3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(FeathersAttack());   
    }

    IEnumerator FeathersAttack()
    {
        while (true)
        {
            for (int i = 0; i < feathersPerAttack; i++)
            {
                SpawnFeathers();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    void SpawnFeathers()
    {
    // Calculate a random spawn position around the player at a safe distance
        Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        GameObject feather = Instantiate(Feathers, spawnPosition, Quaternion.identity);

    }


}

using UnityEngine;
using System.Collections;

public class EnemySpawns : MonoBehaviour
{
    public int roundCount;
    public bool roundActive;
    public int enemyCount;
    public GameObject enemy;
    public int timer = 2;

    private void Spawn()
    {
            GameObject Enemy = GameObject.Instantiate(enemy);
            enemyCount = enemyCount + 1;
            Transform t  = Enemy.transform;

            float randX = Random.Range(-5f, 5f);
            float randY = Random.Range(-5f, 5f);
            float randZ = Random.Range(-5f, 5f);

            t.position = new Vector3(randX, randY, randZ);
            t.rotation = Quaternion.identity;
    }

    public void EnemyCheck()
    {
        if (enemyCount < 5)
        {
            StartCoroutine(TimerRoutine());
        }
    }
    IEnumerator TimerRoutine()
    {
        WaitForSeconds delay =  new WaitForSeconds(timer);
        while (enemyCount < 5)
        {

            Spawn();

            yield return delay;
            StopCoroutine(TimerRoutine());
        }

    }
    void Start()
    {
        EnemyCheck();
    }
}
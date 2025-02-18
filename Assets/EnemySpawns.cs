using UnityEngine;
using System.Collections;

public class EnemySpawns : MonoBehaviour
{
    public float roundCount;
    public float roundMult;
    public bool roundActive = false;
    public float maxEnemies = 10;
    public float enemiesRemaining;
    public float enemyCount;
    public GameObject enemy;
    private GameObject Enemy;
    public GameObject boss;
    private GameObject Boss;
    public float timer = 0.75f;
    public float timer2 = 0.01f;
    private bool isSpawning = false;

    public void StartGame()
    {
        roundCount = 1;
        roundActive = true;
        EnemyCheck();
    }
    private void EndRound()
    {
        roundActive = false;
    }

    private void NextRound()
    {
        roundCount++;
        roundActive = true;
        RoundMultiplier();
        enemiesRemaining = maxEnemies * roundMult;
        EnemyCheck();


    }
    public void BossRound()
    {
        roundCount++;
        roundActive = true;

        Boss = Instantiate(boss);
        Transform b  = Boss.transform;
        b.position = new Vector2(0,0);
        b.rotation = Quaternion.identity;

    }
    public void GameOver()
    {
        Debug.Log("You Won!");
    }
    public void RoundMultiplier()
    {
        switch ((int)roundCount)
        {
            case 1: 
                roundMult = 1.5f;
                break;
            case 2:
                roundMult = 2.0f; 
                break;
            case 3:
                roundMult = 2.5f;
                break;
            case 4:
                roundMult = 3.0f;
                break;
            case 5:
                roundMult = 3.5f;
                break;
            case 6: 
                roundMult = 4.0f;
                break;
            case 7:
                roundMult = 4.5f; 
                break;
            case 8:
                roundMult = 5.0f;
                break;
            case 9:
                roundMult = 5.5f;
                break;
        }
    }
    
    public void Spawn()
    {
        Vector2 spawnPos;
        do
        {
            float randX = Random.Range(-5f, 5f);
            float randY = Random.Range(-5f, 5f);
            spawnPos = new Vector2(randX, randY);


        } while (Physics2D.OverlapCircle(spawnPos, 0.5f) != null);

        Enemy = Instantiate(enemy);
        enemyCount = enemyCount + 1;
        Transform t  = Enemy.transform;
        t.position = spawnPos;
        t.rotation = Quaternion.identity;

    }

    public void EnemyCheck()
    {
        if (enemyCount < 5 && !isSpawning)
        {
            StartCoroutine(TimerRoutine());
        }
    }
    IEnumerator TimerRoutine()
    {
        isSpawning = true;

        WaitForSeconds delay =  new WaitForSeconds(timer);
        WaitForSeconds delay2 =  new WaitForSeconds(timer2);
        yield return delay2;

        while (enemyCount < 5 && enemiesRemaining > 0) //CHANGE THE ENEMY COUNT CHECKER
        {
            Spawn();
            yield return delay;
        }
        isSpawning = false;

        StopCoroutine(TimerRoutine());
    }
    void Start()
    {
        StartGame();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero); 

            if(hit.collider != null )
            {
                if(hit.collider.gameObject.gameObject)
                {
                    Destroy(hit.collider.gameObject);
                    enemyCount = enemyCount - 1;
                    enemiesRemaining --;

                    if (enemiesRemaining <= 0)
                    {
                        EndRound();
                    }
                    if (!isSpawning)
                    {  
                        StartCoroutine(TimerRoutine());
                    }
                }
            }
        }
        if(roundActive == false )
        {
            if(roundCount == 10)
            {
                GameOver();
            }
            else if (roundCount == 9 && Input.GetKeyDown(KeyCode.Space))
            {
                BossRound();            
            }
            else if(roundCount <= 8 && Input.GetKeyDown(KeyCode.Space)) 
            {
                NextRound();
            }
        }
    } 

}
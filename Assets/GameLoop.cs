using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameLoop : MonoBehaviour
{
    public float roundCount = 0; 
    public float roundMult;
    public bool roundActive = false;
    public float maxEnemies = 10;
    public float enemiesRemaining = 1;
    public float enemyCount;
    public GameObject enemy;
    private GameObject Enemy;
    public GameObject boss;
    private GameObject Boss;
    public float timer = 0.75f;
    public float timer2 = 0.01f;
    private bool isSpawning = false;
    public Image darkness;
    public Image nextRoundImage;
    public Button nextRoundButton;
    private player Player;

    private void EndRound()
    {
        roundActive = false;
        Destroy(Enemy);
        darkness.gameObject.SetActive(true);
        Player.enabled = false;
    }

    private void NextRound()
    {
        darkness.gameObject.SetActive(false);
        roundCount++;
        roundActive = true;
        RoundMultiplier();
        enemiesRemaining = maxEnemies * roundMult;
        EnemyCheck();
        Player.enabled = true;
    }

    public void BossRound()
    {
        darkness.gameObject.SetActive(false);
        roundCount++;
        roundActive = true;
        Player.enabled = true;

        Boss = Instantiate(boss);
        Transform b  = Boss.transform;
        b.position = new Vector2(0, 0);
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
            case 1: roundMult = 1.5f; break;
            case 2: roundMult = 2.0f; break;
            case 3: roundMult = 2.5f; break;
            case 4: roundMult = 3.0f; break;
            case 5: roundMult = 3.5f; break;
            case 6: roundMult = 4.0f; break;
            case 7: roundMult = 4.5f; break;
            case 8: roundMult = 5.0f; break;
            case 9: roundMult = 5.5f; break;
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
        enemyCount++;
        Transform t = Enemy.transform;
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
        WaitForSeconds delay = new WaitForSeconds(timer);
        WaitForSeconds delay2 = new WaitForSeconds(timer2);
        yield return delay2;

        while (enemyCount < 5 && enemiesRemaining > 0)
        {
            Spawn();
            yield return delay;
        }
        isSpawning = false;
    }

    void Start()
    {
        nextRoundButton.onClick.AddListener(nxtRnd);
        Player = FindObjectOfType<player>();
        /**
        StartScreen();
        StartScreen(){
        if (startButton == pressed)
        {NextRound()}}
        **/
        NextRound();
    }
    void Update()
    {
        if (enemiesRemaining <= 0 && isSpawning)
        {
            EndRound();
        }
    }
    public void nxtRnd(){
        if (roundCount <=1){
            NextRound();    
        }
        else
        {
            BossRound();
        }

    }
}

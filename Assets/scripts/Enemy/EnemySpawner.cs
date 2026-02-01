using UnityEngine;
using System.Collections;
using System;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int maxEnemies;
    [SerializeField] private int enemiesPerSecond;
    public static int enemyActiveCounter;
    private int fibPrev = 5;
    private int fibCurrent = 8;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxEnemies = fibCurrent;
        enemiesPerSecond = 1;
        enemyActiveCounter = 0;

        StartCoroutine(SpawnEnemy());
        StartCoroutine(addEnemiesPerSecond());
        StartCoroutine(IncreaseMaxEnemiesFibonacci());


    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("enemy Active: "+ enemyActiveCounter);
        Debug.Log("enemies Per Second: " + enemiesPerSecond);
        Debug.Log("max enemies: " + maxEnemies);
    }
    public Vector3 GetSpawnPosition()
    {
        Camera cam = Camera.main;

        float radius = cam.orthographicSize * cam.aspect + 2f;
        Vector2 dir = UnityEngine.Random.insideUnitCircle.normalized;

        Vector3 pos = cam.transform.position + (Vector3)(dir * radius);
        pos.z = 0;

        return pos;
    }


    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            for (int i = 0; i < enemiesPerSecond; i++)
            {
                if (enemyActiveCounter >= maxEnemies)
                    break;

                GameObject enemyBasic = EnemyPool.instance.RequestEnemy();
                enemyBasic.transform.position = GetSpawnPosition();
                enemyBasic.SetActive(true);
            }

            yield return new WaitForSeconds(1f);
        }
    }
    IEnumerator addEnemiesPerSecond()
    {
        while (true)
        {
            yield return new WaitForSeconds(15f);
            enemiesPerSecond++;
        }
    }

    IEnumerator IncreaseMaxEnemiesFibonacci()
    {
        while (true)
        {
            float waitTime = maxEnemies <= 100 ? 15f : 60f;
            yield return new WaitForSeconds(waitTime);

            int next = fibPrev + fibCurrent;
            fibPrev = fibCurrent;
            fibCurrent = next;

            maxEnemies = fibCurrent;
        }
    }
}

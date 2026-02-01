using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Vector3 GetSpawnPosition()
    {
        Camera cam = Camera.main;

        float radius = cam.orthographicSize * cam.aspect + 2f;
        Vector2 dir = Random.insideUnitCircle.normalized;

        Vector3 pos = cam.transform.position + (Vector3)(dir * radius);
        pos.z = 0;

        return pos;
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            GameObject enemyBasic = EnemyPool.instance.RequestEnemy();
            enemyBasic.transform.position = GetSpawnPosition();
            enemyBasic.SetActive(true);
            yield return new WaitForSeconds(5f);
        }
    }
}

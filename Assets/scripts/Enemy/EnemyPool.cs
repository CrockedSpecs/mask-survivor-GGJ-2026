using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool instance;
    [SerializeField] private GameObject enemyPrefab;
    private int poolSize = 30;
    [SerializeField] private List<GameObject> enemyList = new List<GameObject>();


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        AddEnemiesToPool(poolSize);
    }

    private void AddEnemiesToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject enemyBasic = Instantiate(enemyPrefab);
            enemyBasic.SetActive(false);
            enemyList.Add(enemyBasic);
        }
    }

    public GameObject RequestEnemy()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (!enemyList[i].activeSelf)
            {
                return enemyList[i];
            }
        }
        AddEnemiesToPool(1);
        enemyList[enemyList.Count - 1].SetActive(true);
        return enemyList[enemyList.Count - 1];
    }
    // Update is called once per frame
    void Update()
    {

    }
}

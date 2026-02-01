using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool instance;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int poolSize = 30;
    [SerializeField] private List<GameObject> enemyList = new List<GameObject>();

    private bool poolReady = false;

    private void Awake()
    {
        if (instance == null) instance = this;
        else { Destroy(gameObject); return; }
    }

    private IEnumerator Start()
    {
        // Espera hasta que exista el singleton del Player
        while (Player.Instance == null)
            yield return null;

        AddEnemiesToPool(poolSize);
        poolReady = true;
    }

    private void AddEnemiesToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            // IMPORTANTE: instanciar como hijo ayuda a ordenar jerarquía
            GameObject enemyBasic = Instantiate(enemyPrefab, transform);

            // Si el prefab está activo, su OnEnable YA se ejecutó al Instanciar.
            // Por eso también debes aplicar el punto #2 abajo (prefab inactivo) o proteger OnEnable.
            enemyBasic.SetActive(false);

            enemyList.Add(enemyBasic);
        }
    }

    public GameObject RequestEnemy()
    {
        if (!poolReady) return null; // o Debug.LogWarning si quieres

        for (int i = 0; i < enemyList.Count; i++)
        {
            if (!enemyList[i].activeSelf)
                return enemyList[i];
        }

        AddEnemiesToPool(1);
        return enemyList[enemyList.Count - 1];
    }
}

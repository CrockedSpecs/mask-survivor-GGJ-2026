using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool instance;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int poolSize = 15;
    [SerializeField] private List<GameObject> bulletList = new List<GameObject>();

    private bool poolReady = false;

    private void Awake()
    {
        if (instance == null) instance = this;
        else { Destroy(gameObject); return; }
    }

    private IEnumerator Start()
    {
        // Espera hasta que el Player singleton exista
        while (Player.Instance == null)
            yield return null;

        AddBulletsToPool(poolSize);
        poolReady = true;
    }

    private void AddBulletsToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            // Instancia como hijo desde el comienzo
            GameObject bullet = Instantiate(bulletPrefab, transform);

            // Asegúrate que quede inactiva en el pool
            bullet.SetActive(false);

            bulletList.Add(bullet);
        }
    }

    public GameObject RequestBullet()
    {
        if (!poolReady)
        {
            // Si disparan antes de estar listo, devuelve null (o log si prefieres)
            // Debug.LogWarning("BulletPool aún no está listo (Player.Instance todavía no existe).");
            return null;
        }

        for (int i = 0; i < bulletList.Count; i++)
        {
            if (!bulletList[i].activeSelf)
                return bulletList[i];
        }

        // Expandimos pool y devolvemos la nueva bala INACTIVA
        AddBulletsToPool(1);
        return bulletList[bulletList.Count - 1];
    }
}

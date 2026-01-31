using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic; 

public class BulletPool : MonoBehaviour
{
    public static BulletPool instance;
    [SerializeField] private GameObject bulletPrefab;
    private int poolSize = 15;
    [SerializeField] private List<GameObject> bulletList = new List<GameObject>();


    private void Awake()
    {
        if(instance == null)
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
        AddBulletsToPool(poolSize);
    }

    private void AddBulletsToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletList.Add(bullet);
            bullet.transform.parent = transform;
        }
    }

    public GameObject RequestBullet()
    {
        for (int i = 0; i < bulletList.Count; i++)
        {
            if (!bulletList[i].activeSelf)
            {
                return bulletList[i];
            }
        }
        AddBulletsToPool(1);
        bulletList[bulletList.Count - 1].SetActive(true);
        return bulletList[bulletList.Count-1];
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

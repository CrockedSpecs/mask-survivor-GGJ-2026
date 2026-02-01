using UnityEngine;
using System;
public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private int health = 1;
    [SerializeField] private int damage = 1;
    [SerializeField] private bool isPossesable = false;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    private void OnEnable()
    {
        EnemySpawner.enemyActiveCounter++;
        player = GameObject.FindGameObjectWithTag("Player");
        health = (int)Math.Round(health * Player.Instance.GetCurrentLevel() * 0.6f); // NEW: Scale enemy health with player level

    }

    private void OnDisable()
    {
        EnemySpawner.enemyActiveCounter--;
        CancelInvoke();
    }


    private void FixedUpdate()
    {
        if (player == null) return;
        if(isPossesable) return;
        Vector2 direction = ((Vector2)player.transform.position - rb.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {

            EnemySpawner.enemyActiveCounter--;
            CancelInvoke();
            isPossesable = true;
            MakePossesable();


        }
    }
    private void MakePossesable()
    {
        gameObject.GetComponent<Collider2D>().isTrigger = true;
        rb.linearVelocity = Vector2.zero;
        isPossesable = true;
        Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
        tmp.a = 0.3f;
        gameObject.GetComponent<SpriteRenderer>().color = tmp;
    }
    public bool IsPossesable() { return isPossesable; }
    public int GetDamageAmount() { return damage; }

}

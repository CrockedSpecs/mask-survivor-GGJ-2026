using Unity.Mathematics;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    private Rigidbody2D rb;
    private int damage = 1;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    public void SetDirection(Vector2 direction)
    {
        rb.linearVelocity = direction.normalized * bulletSpeed;
    }

    private void OnEnable()
    {
        Invoke(nameof(DestroyBullet), 5f);
        damage = Math.Max(1,(int)(Mathf.Ceil(damage * Player.Instance.GetCurrentLevel() * 0.5f))); // Scale bullet damage with player level
    }

    private void OnDisable()
    {
        CancelInvoke();
        rb.linearVelocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyBehaviour enemy = collision.gameObject.GetComponent<EnemyBehaviour>();
            if (enemy != null && !enemy.IsPossesable())
            {
                enemy.TakeDamage(damage);
            }
        }
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        gameObject.SetActive(false);
    }
}

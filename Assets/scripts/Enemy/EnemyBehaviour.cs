using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float moveSpeed = 3f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        EnemySpawner.enemyActiveCounter++;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnDisable()
    {
        EnemySpawner.enemyActiveCounter--;
        CancelInvoke();

    }


    private void FixedUpdate()
    {
        if (player == null) return;

        Vector2 direction = ((Vector2)player.transform.position - rb.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
    }
}

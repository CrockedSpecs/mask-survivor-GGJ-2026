using UnityEngine;
using UnityEngine.InputSystem;
public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    private Vector3 moveDirection;

    public void SetDirection(Vector3 direction)
    {
        moveDirection = direction.normalized;
    }

    void Update()
    {
        transform.position += moveDirection * bulletSpeed * Time.deltaTime;
    }

    private void OnEnable()
    {
        Invoke(nameof(DestroyBullet), 5f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        gameObject.SetActive(false);
    }
}

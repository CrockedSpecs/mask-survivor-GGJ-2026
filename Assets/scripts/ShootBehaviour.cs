using UnityEngine;
using UnityEngine.InputSystem;

public class ShootBehaviour : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private float cadency = 0.8f;
    [SerializeField] private int maxAmmo = 6;
    [SerializeField] private float reloadTime = 1.5f;

    private int ammo;
    private float shootTimer;
    private bool isReloading = false;

    [Header("Weapon Orbit Settings")]
    [SerializeField] private Transform player;

    private void Start()
    {
        ammo = maxAmmo;
        shootTimer = cadency;
    }

    private void Update()
    {
        shootTimer += Time.deltaTime;
        if (isReloading) return;

        if ((ammo > 0) && !isReloading)
        {
            Shoot();
        }
        else if (ammo <= 0 || Keyboard.current.rKey.wasPressedThisFrame)
        {
            Reload();
        }
    }

    private void Shoot()
    {
        bool click = Mouse.current.leftButton.wasPressedThisFrame;
        bool hold = Mouse.current.leftButton.isPressed;

        if ((click || hold) && shootTimer >= cadency)
        {
            GameObject bullet = BulletPool.instance.RequestBullet();
            if (bullet == null) return;

            bullet.transform.position = transform.position;

            Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(
                new Vector3(mouseScreenPos.x, mouseScreenPos.y, -Camera.main.transform.position.z)
            );
            mouseWorldPos.z = 0f;

            Vector2 direction = (mouseWorldPos - transform.position).normalized;
            bullet.SetActive(true);
            bullet.GetComponent<BulletBehaviour>().SetDirection(direction);


            shootTimer = 0f;
            ammo--;
        }
    }

    private void Reload()
    {
        if (isReloading) return;

        isReloading = true;
        Invoke(nameof(FinishReload), reloadTime);
    }

    private void FinishReload()
    {
        ammo = maxAmmo;
        isReloading = false;
    }
}

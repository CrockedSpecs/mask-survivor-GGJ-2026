using UnityEngine;
using UnityEngine.InputSystem;

public class ShootBehaviour : MonoBehaviour
{

    [Header("shooting Settings")]
    [SerializeField] private float cadency = 0.8f;
    [SerializeField] private int maxAmmo = 6;
    [SerializeField] private float reloadTime = 1.5f;
    private int ammo;
    private float shootTimer;
    private bool isReloading = false;

    [Header("weapon orbit settings")]
    [SerializeField] private Transform player;
    [SerializeField] private float distance = 0.2f;

    private void Start()
    {
        ammo = maxAmmo;
        shootTimer = cadency;
    }

    void Update()
    {
        shootTimer += Time.deltaTime;
        if (isReloading) return;
        if (ammo > 0 && !isReloading)
        {
            Shoot();
        }
        else if (ammo <= 0 || Keyboard.current.rKey.wasPressedThisFrame)
        {
            Reload();
        }


    }

    void Shoot()
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

            Vector3 direction = (mouseWorldPos - transform.position).normalized;
            bullet.GetComponent<BulletBehaviour>().SetDirection(direction);
            bullet.SetActive(true);


            shootTimer = 0f;
            ammo--;
        }
    }

    void Reload()
    {
        if (isReloading) return;

        isReloading = true;
        Invoke(nameof(FinishReload), reloadTime);
    }

    void FinishReload()
    {
        ammo = maxAmmo;
        isReloading = false;
    }
}

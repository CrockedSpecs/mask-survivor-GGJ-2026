using UnityEngine;
using UnityEngine.InputSystem;

public class OrbitingWeapon : MonoBehaviour
{
    [SerializeField] private GameObject orbitingPrefab; 
    [SerializeField] private float distance = 0.2f;

    private GameObject orbitingInstance; 

    void Start()
    {
        if (orbitingPrefab != null)
        {
            orbitingInstance = Instantiate(orbitingPrefab, transform.position, Quaternion.identity);
        }
    }

    void Update()
    {
        if (orbitingInstance == null) return;


        Vector2 mouseScreen = Mouse.current.position.ReadValue();
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mouseScreen);
        mouseWorld.z = 0f;


        Vector2 direction = (mouseWorld - transform.position).normalized;


        orbitingInstance.transform.position = transform.position + (Vector3)(direction * distance);


        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        orbitingInstance.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
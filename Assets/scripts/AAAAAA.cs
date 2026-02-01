using UnityEngine;

public class AAAAAA : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger area.");
        }
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Player entered the trigger area.");
        }
    }
}

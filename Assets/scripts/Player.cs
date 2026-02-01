using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // SINGLETON PATTERN IMPLEMENTATION
    public static Player Instance { get; private set; }

    // Player attributes [CORE]
    [Header("Core Stats")]
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int health = 3;
    [SerializeField] private int score = 0;
    [SerializeField] private int currentLevel = 1;
    [SerializeField] private int currentExperience = 0;

    // BROWNIE POINTS
    [Header("Info")]
    [SerializeField] private string playerName = "Hero";

    private Dictionary<string, bool> habilities = new Dictionary<string, bool>();

    // Track the possessable enemy we're currently inside
    private EnemyBehaviour possessCandidate, possessCurrent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else if (Instance != this) Destroy(gameObject);

        // Initialize abilities
        habilities["hability1"] = false;
        habilities["hability2"] = false;
        habilities["hability3"] = false;
    }

    void Update()
    {
        // New Input System
        if (possessCandidate != null &&
            Keyboard.current != null &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            Debug.Log("Pressed E with candidate: " + possessCandidate.name);

            PossessEnemy();
        }
    }

    #region Getters
    public Dictionary<string, bool> GetAbilitiesDIC() { return habilities; }
    public int GetCurrentHealth() { return health; }
    public int GetMaxHealth() { return maxHealth; }
    public int GetScore() { return score; }
    public int GetCurrentLevel() { return currentLevel; }
    #endregion

    #region Setters
    public void HabilityUnlock(string habilty)
    {
        habilities[habilty] = true;
        // agregar más lógica si es necesario
    }

    public void HabilityLock(string habilty)
    {
        habilities[habilty] = false;
    }

    public void HabilityLockAll()
    {
        foreach (var key in new List<string>(habilities.Keys))
        {
            habilities[key] = false;
        }
    }

    public void HealthRegenByNumber(int amount)
    {
        health += amount;
        // agregar más lógica si es necesario
    }

    public void MaxHealthIncreaseByNumber(int amount)
    {
        maxHealth += amount;
        HealthRegenByNumber(amount); // Al aumentar la vida máxima, también aumento la vida actual
        // agregar más lógica si es necesario
    }

    public void ScoreIncreaseByNumber(int amount)
    {
        score += amount;
        // agregar más lógica si es necesario
    }

    public void LevelIncreaseByNumber(int amount)
    {
        currentLevel += amount;
        // agregar más lógica si es necesario
    }

    /*
    // PARA QUÉ ES LA EXPERIENCIA AAAAAAAAAAAAAAAAAAAAAAAAAA    
    public void ExperienceIncreaseByNumber(int amount)
    {
        currentExperience += amount;
        if (currentExperience >= currentLevel * 100 * 1.1) //cada nivel requiere 100 * nivel experiencia * 1.1 para subir de nivel
        {
            currentExperience -= currentLevel * 100;
            LevelIncreaseByNumber(1);
        }
    }
    */

    public void PossessEnemy()
    {
        if (possessCandidate == null) return;

        int amountToRegen = 1;
        HealthRegenByNumber(amountToRegen);
        LevelIncreaseByNumber(1);

        // Don't destroy the same enemy  trying to possess
        if (possessCurrent != null && possessCurrent != possessCandidate)
        {
            Destroy(possessCurrent.gameObject);
        }

        // Attach
        possessCandidate.transform.SetParent(transform, false);
        possessCandidate.transform.localPosition = Vector3.zero;

        // Make it follow nicely (physics won't fight parenting)
        Rigidbody2D rb = possessCandidate.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.simulated = false;
        }

        // Disable its collider so it doesn't re-trigger / collide
        Collider2D col = possessCandidate.GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        possessCurrent = possessCandidate;
        possessCandidate = null;
    }

    #endregion

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Debug.Log("Player has died.");
            // Destroy(gameObject);
        }
    }

    // DO NOT MODIFY - kept exactly as you had it
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyBehaviour enemy = collision.gameObject.GetComponent<EnemyBehaviour>();

            // Lógica para recibir daño de un enemigo
            if (!enemy.IsPossesable())
            {
                TakeDamage(enemy.GetDamageAmount());
            }
        }
    }

    // Trigger-based possession detection
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entering in trigger");
        TrySetPossessCandidate(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // Robust if the enemy turns trigger/possessable while you're already overlapping.
        Debug.Log("Staying in trigger");

        TrySetPossessCandidate(other);
        Debug.Log("Staying in trigger with " + other.name);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        EnemyBehaviour enemy = other.GetComponent<EnemyBehaviour>() ?? other.GetComponentInParent<EnemyBehaviour>();
        if (enemy != null && enemy == possessCandidate)
        {
            possessCandidate = null;
        }
    }

    private void TrySetPossessCandidate(Collider2D other)
    {
        EnemyBehaviour enemy = other.GetComponent<EnemyBehaviour>() ?? other.GetComponentInParent<EnemyBehaviour>();
        if (enemy == null) return;

        // Keep it consistent with collision logic: only consider Enemy-tagged objects
        if (!enemy.gameObject.CompareTag("Enemy")) return;

        if (enemy.IsPossesable())
        {
            possessCandidate = enemy;
        }
        Debug.Log($"Trigger with {enemy.name}, possesable={enemy.IsPossesable()}");

    }
}

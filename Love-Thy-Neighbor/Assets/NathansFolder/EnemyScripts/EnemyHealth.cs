using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] FlowerManData FMData;
    [SerializeField] int moneyForKill;
    [SerializeField] GameObject partEnemySystem;
    float health;
    // Start is called before the first frame update
    void Start()
    {
        health = FMData.health;
    }

    public void TakeDamage(float damage)
    {
        // Reduce the health by the damage amount
        health -= damage;
        Debug.Log("Enemy took damage, remaining health: " + health);

        // Check if health is zero or below
        if (health <= 0)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        // Instantiate particle system for enemy death
        Instantiate(partEnemySystem, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);

        // Add money to player for the kill
        GameObject.Find("UpgradeMenu").GetComponent<FinaceHandler>().PlayerMoney += moneyForKill;

        // Notify WaveManager that this enemy has died
        WaveManager waveManager = FindObjectOfType<WaveManager>();
        if (waveManager != null)
        {
            waveManager.EnemyDefeated(gameObject);
        }
        else
        {
            Debug.LogError("WaveManager not found!");
        }

        // Destroy the enemy game object
        Destroy(gameObject);
    }

    public float GetHealth()
    {
        return health;
    }
}

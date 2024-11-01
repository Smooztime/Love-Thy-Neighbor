using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform playerTarget;
    public float speed = 3.5f;
    public float damage = 10f;
    public float attackRange = 3.0f;
    public float stoppingDistance = 1.5f; // Distance at which the enemy stops moving towards the player
    public float attackCooldown = 1.5f; // Time (in seconds) between attacks
    private bool canAttack = true;

    private PlayerHealth playerHealth;

    // Start is called before the first frame update
    private void Start()
    {
        // Get the NavMeshAgent component attached to the GameObject
        agent = GetComponent<NavMeshAgent>();

        // Set the speed of the NavMeshAgent to the given value
        agent.speed = speed;

        // Set the agent's stopping distance to avoid pushing the player
        agent.stoppingDistance = stoppingDistance;

        // Find the player by tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerTarget = playerObject.transform;
            playerHealth = playerObject.GetComponent<PlayerHealth>();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        // If the player target is assigned, set the destination to the player's position
        if (playerTarget != null)
        {
            agent.SetDestination(playerTarget.position);

            // Check if the enemy is within attack range of the player
            float distanceToPlayer = Vector3.Distance(transform.position, playerTarget.position);
            if (distanceToPlayer <= attackRange && canAttack)
            {
                StartCoroutine(AttackPlayer());
            }
        }
    }

    private IEnumerator AttackPlayer()
    {
        canAttack = false;
        Debug.Log("Attacking player with " + damage + " damage");

        // Apply damage to the palyer
        if (playerHealth != null)
        {
            playerHealth.TakeDamage((int)damage);
        }

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private void OnDeath()
    {
        WaveManager waveManager = FindObjectOfType<WaveManager>();
        if (waveManager != null)
        {
            waveManager.EnemyDefeated(gameObject); // Pass the enemy GameObject to remove from the list
        }

        Destroy(gameObject);
    }

}
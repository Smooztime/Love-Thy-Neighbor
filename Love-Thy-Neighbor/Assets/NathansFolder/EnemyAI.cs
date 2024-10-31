using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform playerTarget;
    public float speed = 7f;
    public float damage = 10f;
    public float attackRange = 3.0f;
    public float stoppingDistance = 1.5f; // Distance at which the enemy stops moving towards the player
    public float attackCooldown = 1.5f; // Time (in seconds) between attacks
    private bool canAttack = true;

    private PlayerHealth playerHealth;
    FreezeManager freezeManager;

    // Start is called before the first frame update
    private void Start()
    {
        freezeManager = GameObject.Find("FreezeManager").GetComponent<FreezeManager>();
        // Get the NavMeshAgent component attached to the GameObject
        agent = GetComponent<NavMeshAgent>();

        // Set the speed of the NavMeshAgent to the given value
        agent.speed = speed;

        // Set the agent's stopping distance to avoid pushing the player
        agent.stoppingDistance = stoppingDistance;

        // Get the PlayerHealth component from the player target
        if (playerTarget != null)
        {
            playerHealth = playerTarget.GetComponent<PlayerHealth>();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        // If the player target is assigned, set the destination to the player's position
        if (playerTarget != null)
        {
            agent.SetDestination(playerTarget.position);
            agent.speed = speed * freezeManager.FrozenTime;
            // Check if the enemy is within attack range of the player
            float distanceToPlayer = Vector3.Distance(transform.position, playerTarget.position);
            if (distanceToPlayer <= attackRange && canAttack && freezeManager.TimeIsFrozen == false)
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
}
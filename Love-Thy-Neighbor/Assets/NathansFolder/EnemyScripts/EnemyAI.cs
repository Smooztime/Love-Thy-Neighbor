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
    [SerializeField] bool isRanged = false;
    [SerializeField] float rangeForBullet;
    [SerializeField] Animator animator;
    GameObject bulletPrefab;
    private PlayerHealth playerHealth;
    FreezeManager freezeManager;

    // Start is called before the first frame update
    private void Start()
    {
        freezeManager = GameObject.Find("FreezeManager").GetComponent<FreezeManager>();
        bulletPrefab = freezeManager.bulletPrefab;
        // Get the NavMeshAgent component attached to the GameObject
        agent = GetComponent<NavMeshAgent>();

        // Set the speed of the NavMeshAgent to the given value
        agent.speed = speed;

        // Set the agent's stopping distance to avoid pushing the player
        agent.stoppingDistance = stoppingDistance;

        // Find the player by tag
        GameObject playerObject = GameObject.Find("PlayerCapsule");
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
            if(!freezeManager.TimeIsFrozen && canAttack == true)
            {
                animator.SetBool("IsRun", true);
            }
            else if (freezeManager.TimeIsFrozen && canAttack == true)
            {
                animator.SetBool("IsRun", false);
            }
            agent.SetDestination(playerTarget.position);
            agent.speed = speed * freezeManager.FrozenTime;
            // Check if the enemy is within attack range of the player
            float distanceToPlayer = Vector3.Distance(transform.position, playerTarget.position);
            if (distanceToPlayer <= attackRange && canAttack && freezeManager.TimeIsFrozen == false && !isRanged)
            {
                StartCoroutine(AttackPlayer());
            }
            else if(distanceToPlayer <= attackRange && canAttack && freezeManager.TimeIsFrozen == false && isRanged)
            {
                StartCoroutine(ShootPlayer());
            }
        }
    }
    private IEnumerator ShootPlayer()
    {
        canAttack = false;
        animator.SetBool("IsShoot", true);
        yield return new WaitForSeconds(0.8f); 
        
        Vector3 aimAt = new Vector3(playerTarget.position.x,playerTarget.position.y + .3f,playerTarget.position.z);
        GameObject lastBullet = Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity);
        lastBullet.GetComponent<BulletScript>().targetPosition = (aimAt+ (aimAt - new Vector3(transform.position.x, transform.position.y + 2, transform.position.z)) * rangeForBullet);
        lastBullet.GetComponent<BulletScript>().Damage = (int)damage;
        yield return new WaitForSeconds(attackCooldown);
        animator.SetBool("IsShoot", false);
        canAttack = true;
    }
    private IEnumerator AttackPlayer()
    {
        canAttack = false;
        animator.SetBool("IsShoot", true);
        Debug.Log("Attacking player with " + damage + " damage");

        // Apply damage to the palyer
        if (playerHealth != null)
        {
            playerHealth.TakeDamage((int)damage);
        }

        yield return new WaitForSeconds(attackCooldown);
        animator.SetBool("IsShoot", false);
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
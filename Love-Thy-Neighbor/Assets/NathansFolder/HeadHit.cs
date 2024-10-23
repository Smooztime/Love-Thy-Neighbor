using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadHit : MonoBehaviour
{
    EnemyHealth enemyHealth;
    [SerializeField] float headShotMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = transform.parent.GetComponent<EnemyHealth>();
    }
    public void HeadWasHit(float gunDamage)
    {
        enemyHealth.TakeDamage(gunDamage * headShotMultiplier);
    }
}

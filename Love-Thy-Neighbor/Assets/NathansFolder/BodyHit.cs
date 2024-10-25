using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyHit : MonoBehaviour
{
    EnemyHealth enemyHealth;
    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = transform.parent.GetComponent<EnemyHealth>();
    }
    public void BodyWasHit(float gunDamage)
    {
        enemyHealth.TakeDamage(gunDamage); 
    }
}

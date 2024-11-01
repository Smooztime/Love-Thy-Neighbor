using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyHit : MonoBehaviour
{
    GameObject enemyHealth;
    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = transform.parent.gameObject;
    }
    public void BodyWasHit(float gunDamage)
    {
        if(enemyHealth.TryGetComponent<EnemyHealth>(out EnemyHealth eH))
        {
            eH.TakeDamage(gunDamage);
        }
        else if(enemyHealth.TryGetComponent<PastaManHealth>(out PastaManHealth pH)){
            pH.TakeDamage(gunDamage);
        }
        else if (enemyHealth.TryGetComponent<SuperSoakerHealth>(out SuperSoakerHealth sH))
        {
            sH.TakeDamage(gunDamage);
        }

        Debug.Log(gunDamage);
    }
}

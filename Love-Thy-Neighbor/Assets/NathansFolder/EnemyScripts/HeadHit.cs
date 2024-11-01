using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadHit : MonoBehaviour
{
    GameObject enemyHealth;
    [SerializeField] float headShotMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = transform.parent.gameObject;
    }
    public void HeadWasHit(float gunDamage)
    {
        if (enemyHealth.TryGetComponent<EnemyHealth>(out EnemyHealth eH))
        {
            eH.TakeDamage(gunDamage * headShotMultiplier);
        }
        else if (enemyHealth.TryGetComponent<PastaManHealth>(out PastaManHealth pH)){
            pH.TakeDamage(gunDamage *  headShotMultiplier);
        }
        else if (enemyHealth.TryGetComponent<SuperSoakerHealth>(out SuperSoakerHealth sH))
        {
            sH.TakeDamage(gunDamage * headShotMultiplier);
        }

        Debug.Log(gunDamage);
    }
}

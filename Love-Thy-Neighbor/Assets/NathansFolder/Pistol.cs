using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
   public float inAccuracy = 0;
    float accuracyRecoverySpeed;
    float pistolDamage;
    float attackSpeed;
    float maxInaccuracy;
    [SerializeField]
    GameObject bulletMark;
    [SerializeField] PistolData pistolData;
    bool canShoot = true;
    // Start is called before the first frame update
    void Start()
    {
        pistolDamage = pistolData.damage;
        attackSpeed = pistolData.attackSpeed;
        accuracyRecoverySpeed = pistolData.accuracyRecoverySpeed;
        maxInaccuracy = pistolData.maxInaccuracy;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(inAccuracy > 0)
        {
            inAccuracy -= Time.deltaTime * accuracyRecoverySpeed;
        }
        if(inAccuracy < 0)
        {
            inAccuracy = 0;
        }
    }
    IEnumerator ShotCoolDown()
    {
        yield return new WaitForSeconds(attackSpeed);
        canShoot = true;
    }
    public void Shoot()
    {
        if(canShoot)
        {
            float yBloom = inAccuracy;
            float xBloom = Random.Range(-inAccuracy, inAccuracy);
            inAccuracy += pistolData.perShotRecoil;
            inAccuracy = Mathf.Clamp(inAccuracy, 0, maxInaccuracy);
            RaycastHit hit;
            
            
            if(inAccuracy > pistolData.perShotRecoil)
            {
                yBloom += Random.Range(-pistolData.perShotRecoil, pistolData.perShotRecoil);
            }
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f + xBloom, 0.5f + yBloom, 0));
            Debug.DrawRay(ray.origin, ray.direction * 10);

            if (Physics.Raycast(ray, out hit))
            {
                Instantiate(bulletMark, hit.point, Quaternion.identity);
                if (hit.collider.gameObject.TryGetComponent(out HeadHit head))
                {
                    head.HeadWasHit(pistolDamage);
                }
                if (hit.collider.gameObject.TryGetComponent(out BodyHit body))
                {
                    body.BodyWasHit(pistolDamage);
                }

            }
            StartCoroutine(ShotCoolDown());
            canShoot = false;
        }
        
        
    }
}

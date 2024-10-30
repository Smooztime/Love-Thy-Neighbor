using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] FlowerManData FMData;
    [SerializeField] int moneyForKill;
    float health;
    // Start is called before the first frame update
    void Start()
    {
        health = FMData.health;    
    }

    // Update is called once per frame
    void Update()
    {
        if(health < 0)
        {
            GameObject.Find("UpgradeMenu").GetComponent<FinaceHandler>().PlayerMoney+=moneyForKill;
            Destroy(gameObject);
        }
    }
    public void TakeDamage(float damage)
    {
        Debug.Log(damage);
        health -= damage;
    }
}

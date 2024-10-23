using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] FlowerManData FMData;
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
            Destroy(gameObject);
        }
    }
    public void TakeDamage(float damage)
    {
        Debug.Log(damage);
        health -= damage;
    }
}

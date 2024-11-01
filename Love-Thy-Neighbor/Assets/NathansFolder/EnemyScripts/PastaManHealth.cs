using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastaManHealth : MonoBehaviour
{
    [SerializeField] PastaManData PMData;
    [SerializeField] int moneyForKill;
    [SerializeField] GameObject partEnemySystem;
    float health;
    // Start is called before the first frame update
     void Start()
    {
        health = PMData.health;
    }

    // Update is called once per frame
    void Update()
    {
        if (health < 0)
        {
            Instantiate(partEnemySystem, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
            GameObject.Find("UpgradeMenu").GetComponent<FinaceHandler>().PlayerMoney += moneyForKill;
            Destroy(gameObject);
        }
    }
    public void TakeDamage(float damage)
    {
        Debug.Log(damage);
        health -= damage;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public Vector3 targetPosition;
    [SerializeField] float speed;
    FreezeManager freezeManager;
    public int Damage;
    // Start is called before the first frame update
    void Start()
    {
        freezeManager = GameObject.Find("FreezeManager").GetComponent<FreezeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed * freezeManager.FrozenTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PlayerCapsule")
        {
            other.GetComponent<PlayerHealth>().TakeDamage(Damage);
        }
        
    }
}

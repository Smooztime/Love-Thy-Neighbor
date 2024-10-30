using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHoleScript : MonoBehaviour
{
    float bulletHoleLifeTime = 5;
    // Start is called before the first frame update
    IEnumerator ClearAfterSeconds()
    {
        yield return new WaitForSeconds(bulletHoleLifeTime);
        Destroy(gameObject);
    }
    void Start()
    {
        StartCoroutine(ClearAfterSeconds());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

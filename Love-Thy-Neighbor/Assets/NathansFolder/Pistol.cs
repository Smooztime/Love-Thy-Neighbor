using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    float inAccuracy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Shoot()
    {
        RaycastHit hit;
        inAccuracy = Random.Range(-0.03f, 0.03f);
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f + inAccuracy, 0.5f + inAccuracy, 0));
        Physics.Raycast(ray, out hit);
        Debug.DrawRay(ray.origin,ray.direction * 10);
        Debug.Log(hit.point);
    }
}

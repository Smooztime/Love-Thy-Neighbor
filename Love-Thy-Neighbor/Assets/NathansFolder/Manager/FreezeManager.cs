using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeManager : MonoBehaviour
{
    public bool TimeIsFrozen = false;
    public int FrozenTime { get; private set; } = 1;
    //for ranged enemy
    public GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeIsFrozen)
        {
            FrozenTime = 0;
        }
        else
        {
            FrozenTime = 1;
        }
    }
}

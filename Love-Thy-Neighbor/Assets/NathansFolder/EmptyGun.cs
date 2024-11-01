using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyGun : MonoBehaviour
{
    [SerializeField] AudioSource source;
    public void PlaySound()
    {
        source.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    [SerializeField] AudioSource source;
    public void PlaySound()
    {
        source.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    [SerializeField] AudioSource source;

    public void playSound(AudioClip ac)
    {
        source.PlayOneShot(ac);
    }
}

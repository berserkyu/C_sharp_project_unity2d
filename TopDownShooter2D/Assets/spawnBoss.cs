using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnBoss : MonoBehaviour
{
    [SerializeField] private GameObject boss;
    [SerializeField] private KeyDoor door;
    [SerializeField] private AudioSource sound;
    [SerializeField] private AudioClip INsound;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("player in collides");
        if (other.gameObject.CompareTag("Player"))
        {
            door.CloseDoor();
            boss.SetActive(true);
            sound.PlayOneShot(INsound);
            Destroy(this);
        }
    }
}

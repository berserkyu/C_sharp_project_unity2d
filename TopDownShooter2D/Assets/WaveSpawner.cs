using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyWave;
    [SerializeField] private KeyDoor door;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //πÿ√≈∑≈π∑
            door.CloseDoor();
            enemyWave.SetActive(true);
            Destroy(gameObject);
        }
    }
}

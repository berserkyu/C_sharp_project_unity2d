using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyWave : MonoBehaviour
{
    [SerializeField] GameObject key;
    [SerializeField] private KeyDoor door;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 0)
        {
            door.OpenDoor();
            key.SetActive(true);
        }
    }
}

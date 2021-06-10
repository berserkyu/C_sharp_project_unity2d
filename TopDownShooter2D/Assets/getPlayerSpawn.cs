using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getPlayerSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform player;

    void Start()
    {
        spawnPlayer();
    }

    void spawnPlayer()
    {
        player.position = transform.position;
    }

}

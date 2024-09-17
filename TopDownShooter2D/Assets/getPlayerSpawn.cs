using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getPlayerSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spawnPlayer();
    }

    void spawnPlayer()
    {
        player.position = transform.position;
    }

}

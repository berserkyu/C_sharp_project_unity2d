using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnThings : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform spanwPt;
    [SerializeField] private GameObject Player;
    private GameObject player;
    void Start()
    {
       // player = Instantiate(Player, spanwPt.position, spanwPt.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

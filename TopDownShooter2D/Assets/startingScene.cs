using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startingScene : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject player, enemy;

    void Start()
    {
        transform.position = player.transform.position;
        player.SetActive(false);
        enemy.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Dodge"))
        {
            player.SetActive(true);
            enemy.SetActive(true);
            gameObject.SetActive(false);
        }   
    }
}

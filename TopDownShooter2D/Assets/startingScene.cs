using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startingScene : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform cameraTrans;
    [SerializeField] private GameObject player, enemy;

    void Start()
    {
        transform.position = new Vector3(cameraTrans.position.x-1, cameraTrans.position.y-1, 0);
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

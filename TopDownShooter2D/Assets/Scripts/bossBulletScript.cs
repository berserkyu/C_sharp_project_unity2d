using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossBulletScript : MonoBehaviour
{
    private int dmg = 20;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            collision.gameObject.transform.GetChild(0).gameObject.GetComponent<PlayerBehaviour>()?.damage(dmg);
        }
        Destroy(gameObject);
    }
}

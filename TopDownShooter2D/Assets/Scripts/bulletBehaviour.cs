using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletBehaviour : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D obj)
    {
        Debug.Log("Bullet hit"+obj.collider.name);
        Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletBehaviour : MonoBehaviour
{
    private int dmg = 5;
    private gunBehaviour gun;

    public void setGunBehaviour(gunBehaviour gb)
    {
        gun = gb as gunBehaviour;
    }

    void OnCollisionEnter2D(Collision2D obj)
    { 
        obj.gameObject.GetComponent<enemyBattle>()?.damage(dmg); 
        gun.createWeaponTracer(transform.position);
        Destroy(gameObject);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletBehaviour : MonoBehaviour
{
    private int dmg = 5;
    private gunBehaviour gun;

    void Awake()
    {
        gameObject.SetActive(false);   
    }

    public void setGunBehaviour(gunBehaviour gb)
    {
        gun = gb as gunBehaviour;
    }

    void OnCollisionEnter2D(Collision2D obj)
    {
        
        //do damage
        obj.gameObject.GetComponent<enemyBattle>()?.damage(dmg);
        obj.gameObject.GetComponent<bossBattle>()?.damage(dmg);
        //tell gun to create weapon tracer from fire point to conctact point
        Vector2 pt = obj.GetContact(0).point;
        gun.createWeaponTracer(new Vector3(pt.x,pt.y,0));
        //disable the bullet instance
        gameObject.SetActive(false);

    }
}

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
        Vector2 pt = obj.GetContact(0).point;
        obj.gameObject.GetComponent<bossBattle>()?.damage(dmg);
        obj.gameObject.GetComponent<enemyBattle>()?.damage(dmg);
        gun.createWeaponTracer(new Vector3(pt.x,pt.y,0));
        gameObject.SetActive(false);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBattle : MonoBehaviour
{
    [SerializeField] private int maxHp;
    [SerializeField] private Transform health;
    [SerializeField] private AudioSource sound;
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private Animator anim;
    [SerializeField] private int enemyType;
    [SerializeField] private GameObject damageZone;
    public GameObject bloodEffect;
    private Vector3 initScale;
    private HealthSystem hpSys;
   
    // Start is called before the first frame update
    void Start()
    {
        hpSys = new HealthSystem(maxHp);
        initScale = health.localScale;
    }
    void die()
    {
        if (enemyType == 0)
        {
            anim.Play("ShootEnemyDieDown");
        }
        else
        {
            anim.Play("EnemyDieDown");
        }
        if (gameObject.GetComponent<EnemyFollowPlayer>() != null) gameObject.GetComponent<EnemyFollowPlayer>().enabled = false;
        if (gameObject.GetComponent<EnemyShootFollowPlayer>() != null) gameObject.GetComponent<EnemyShootFollowPlayer>().enabled = false;
    }
    public void damage(int val)
    {
        sound.PlayOneShot(damageSound);
        hpSys.Damage(val);
        object p = Instantiate(bloodEffect, transform.position, Quaternion.identity);//ÅçÑª
        health.localScale = new Vector3(hpSys.GetHealthPercent()*initScale.x,initScale.y ,initScale.z);
        if (hpSys.GetHealth() == 0)
        {
            die();
        }
    }
    public void doneDying()
    {
        Destroy(gameObject);
    }
    public void spawnDamageZone(int i)
    {
        damageZone.transform.position = transform.position;
        damageZone.SetActive(true);
        damageZone.GetComponent<damageZone>().rotate(i);
    }
    public void disableDamageZone()
    {
        damageZone.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

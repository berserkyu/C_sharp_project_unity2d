using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootFollowPlayer : MonoBehaviour
{
    public float speed;
    public float lineOfSite;
    public float shoootingRange;
    public float fireRate = 2f;
    private float nextFireTime;
    public GameObject bullet;
    public GameObject bulletParent;
    private Transform player;
    public HealthBar healthBar;
    public HealthSystem healthSystem;
    private int damageVal;
    [SerializeField] private AudioSource sound;
    [SerializeField] private AudioClip attackSound;

    void Start()
    {
        damageVal = 10;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        HealthSystem healthSystem = new HealthSystem(100);
        healthBar.Setup(healthSystem);
    }

    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSite && distanceFromPlayer > shoootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }
        else if (distanceFromPlayer <= shoootingRange && nextFireTime < Time.time) 
        {
            Instantiate(bullet,bulletParent.transform.position, Quaternion.identity);
            sound.PlayOneShot(attackSound);
            nextFireTime = Time.time + fireRate;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, shoootingRange);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject gb = collision.gameObject.transform.GetChild(0).gameObject;
            if (gb == null) return;
            PlayerBehaviour pb = gb.GetComponent<PlayerBehaviour>();
            if (pb != null) pb.damage(damageVal);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    public float speed;
    public float lineOfSite;
    private Transform player;
    public HealthBar healthBar;
    public HealthSystem healthSystem;
    private int damageVal;
    

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
        if (distanceFromPlayer < lineOfSite) 
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
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
